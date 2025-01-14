// LaserSword for Unity
// (c) 2016 Digital Ruby, LLC
// Refactored for efficiency

using Oculus.Interaction;
using UnityEngine;
using VolumetricLines;

namespace DigitalRuby.LaserSword
{
    public class LaserSwordScript : MonoBehaviour
    {
        [Header("Blade Config")]
        [Tooltip("Blade color")]
        public Color BladeColor = Color.cyan;

        [Range(0.0f, 30.0f)]
        [Tooltip("Blade intensity")]
        public float BladeIntensity = 1.0f;

        [Header("Audio")]
        [Tooltip("Sound to play when the laser sword turns on")]
        public AudioClip StartSound;

        [Tooltip("Sound to play when the laser sword turns off")]
        public AudioClip StopSound;

        [Tooltip("Sound to play when the laser sword stays on")]
        public AudioClip ConstantSound;

        [Header("Other")]
        [Tooltip("How long it takes to turn the laser sword on and off")]
        [Range(0.1f, 3.0f)]
        public float ActivationTime = 1.0f;

        [Header("Assignments")]
        [Tooltip("Root game object.")]
        public GameObject Root;

        [Tooltip("Hilt game object.")]
        public GameObject Hilt;

        [Tooltip("Blade sword renderer.")]
        public MeshRenderer BladeSwordRenderer;

        [Tooltip("Blade sword mesh.")]
        public MeshFilter BladeSwordMesh;

        [Tooltip("Light game object.")]
        public Light Light;

        [Tooltip("Audio source.")]
        public AudioSource AudioSource;

        [Tooltip("Audio source for looping.")]
        public AudioSource AudioSourceLoop;

        [Tooltip("Blade Laser Script")]
        public VolumetricLineBehavior BladeLaserScript;

        [Tooltip("Blade start")]
        public Transform BladeStart;

        [Tooltip("Blade end")]
        public Transform BladeEnd;

        [Tooltip("DistanceHandGrabGameObject")]
        public GameObject OVRDistanceHandGrabGameObject;

        public float initialBladeLightIntensity = 100f;

        public OVRInput.Button activationButton = OVRInput.Button.Two; // Button to activate/deactivate (X Button)
        private bool isGrabbed = false;
        private bool isActivated = false;
        private ObjectGrabbedEventSender grabbedEventSender;

        private float bladeHeight;
        private float bladeTime;
        private int state; // 0 = off, 1 = on, 2 = turning off, 3 = turning on
        private Transform temporaryBladeStart;
        private float bladeDir; // 1 = up, -1 = down
        private Material bladeMaterial;
        private float initialBladeScaleY, initialBladeLaserLineLength;
        private Rigidbody rb;

        private void Awake()
        {
            // Get the DistanceGrabInteractable component
            grabbedEventSender = OVRDistanceHandGrabGameObject.GetComponent<ObjectGrabbedEventSender>();

            if (grabbedEventSender != null)
            {
                // Subscribe to grab (select) and release (unselect) events
                grabbedEventSender.OnObjectGrabbed += OnGrabbed;
                grabbedEventSender.OnObjectReleased += OnReleased;
            }

            rb = GetComponent<Rigidbody>();

            initialBladeScaleY = BladeSwordMesh.transform.localScale.y;
            // Cache the material for performance
            bladeMaterial = BladeSwordRenderer.material;
            initialBladeLaserLineLength = BladeLaserScript.EndPos.y;

            bladeHeight = Vector3.Distance(BladeStart.position, BladeEnd.position);

            if (Camera.main != null && Camera.main.depthTextureMode == DepthTextureMode.None)
            {
                Camera.main.depthTextureMode = DepthTextureMode.Depth;
            }

            InitializeBlade();
        }


        private void InitializeBlade()
        {
            // Blade is initially off; set scale to zero but don't modify the actual positions
            UpdateBladeScale(0f);

            Light.color = BladeColor;
            bladeMaterial.color = BladeColor;
            bladeMaterial.SetColor("_Color", BladeColor);
            BladeLaserScript.LineColor = BladeColor;
            // UpdateLaserLength(0f);

        }

        private void OnDestroy()
        {
            /*
            if (distanceGrabInteractable != null)
            {
                // Unsubscribe from events
                distanceGrabInteractable.WhenSelectInteractor -= OnGrabbed;
                distanceGrabInteractable.WhenUnselectInteractor -= OnReleased;
            }
            */
        }

        private void OnGrabbed(GameObject source)
        {
            isGrabbed = true;
        }

        private void OnReleased(GameObject source)
        {
            
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
            }
            

            isGrabbed = false;
        }

        private void Update()
        {
            // Check if the handle is grabbed and the activation button is pressed
            if (isGrabbed && OVRInput.GetDown(activationButton))
            {
                SetActive(!isActivated);
                Debug.Log("activated: " + !isActivated);
            }

            if (state == 2 || state == 3)
            {
                UpdateBladeState();
            }

            if (state == 1) // When fully on, set light to max
            {
                Light.intensity = initialBladeLightIntensity;
            }
        }

        private void UpdateBladeState()
        {
            bladeTime += Time.deltaTime;
            float percent = Mathf.Clamp01(bladeTime / ActivationTime);

            // Scale Y from 0 to the initial value during activation or vice versa during deactivation
            float currentScaleY = state == 3 ? percent * initialBladeScaleY : (1.0f - percent) * initialBladeScaleY;
            Light.intensity = initialBladeLightIntensity * percent;

            float currentLaserLineLength = state == 3 ? percent * initialBladeLaserLineLength : (1.0f - percent) * initialBladeLaserLineLength;
            // UpdateLaserLength(currentLaserLineLength);

            /*
            BladeSwordRenderer.transform.localScale = new Vector3(
                BladeSwordRenderer.transform.localScale.x,
                currentScaleY,
                BladeSwordRenderer.transform.localScale.z
            );

            // Activate or deactivate visuals based on scale
            BladeSwordRenderer.gameObject.SetActive(currentScaleY > 0.01f);

            */
            UpdateBladeScale(currentScaleY);



            if (bladeTime >= ActivationTime)
            {
                state = state == 3 ? 1 : 0; // Set final state (1 = on, 0 = off)
                bladeTime = 0f;

                if (temporaryBladeStart != null)
                {
                    Destroy(temporaryBladeStart.gameObject);
                }
            }
        }

        private void UpdateBladeScale(float percent)
        {
            Vector3 endPosition = BladeStart.position + (Root.transform.up * bladeDir * percent * bladeHeight);
            BladeEnd.position = endPosition;

            BladeSwordMesh.transform.localScale = new Vector3(BladeSwordMesh.transform.localScale.x, percent, BladeSwordMesh.transform.localScale.z);
            BladeSwordMesh.gameObject.SetActive(percent > 0.01f);
        }

        private void UpdateLaserLength(float newLength)
        {
            BladeLaserScript.EndPos = new Vector3(0, newLength, 0);
        }

        public bool TurnOn(bool value)
        {
            if (state == 2 || state == 3 || (state == 1 && value) || (state == 0 && !value))
            {
                isActivated = value;
                return false; // Invalid state change
            }

            bladeDir = value ? 1f : -1f;
            state = value ? 3 : 2;
            bladeTime = 0f;

            // Create or destroy the temporary blade start
            if (temporaryBladeStart == null)
            {
                temporaryBladeStart = new GameObject("TemporaryBladeStart").transform;
                temporaryBladeStart.parent = Root.transform;
                temporaryBladeStart.position = BladeEnd.position;
            }

            // Play appropriate sound
            AudioSource.PlayOneShot(value ? StartSound : StopSound);
            if (value)
            {
                AudioSourceLoop.clip = ConstantSound;
                AudioSourceLoop.Play();
            }
            else
            {
                AudioSourceLoop.Stop();
            }

            isActivated = true;
            return true;
        }

        public void Activate() => TurnOn(true);

        public void Deactivate() => TurnOn(false);

        public void SetActive(bool active)
        {
            if (active) Activate();
            else Deactivate();
        }
    }
}