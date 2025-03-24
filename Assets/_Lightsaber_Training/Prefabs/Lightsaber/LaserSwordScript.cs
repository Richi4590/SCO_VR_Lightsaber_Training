// LaserSword for Unity
// (c) 2016 Digital Ruby, LLC

// (c) 2025 Richardo Serban
// Heavily Refactored for efficiency

using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserSwordScript : MonoBehaviour
{
    [Header("Blade Config")]
    [Tooltip("Blade color")]
    public bool randomBladeColor = false;
    public Color BladeColor = Color.cyan;

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

    public float initialBladeLightIntensity = 1f;

    public OVRInput.Button activationButton = OVRInput.Button.Two; // Button to activate/deactivate (X Button)
    public bool turnOn = false;

    private bool _turnOn;
    private bool isGrabbed = false;
    private bool isActivated = false;

    private float initialPitchRelfection;
    private float initialPitchSwordHum;

    private ObjectGrabbedEventSender grabbedEventSender;

    private float bladeHeight;
    private float bladeTime;
    private int state; // 0 = off, 1 = on, 2 = turning off, 3 = turning on
    private Transform temporaryBladeStart;
    private float bladeDir; // 1 = up, -1 = down
    private Material bladeMaterial;
    private float initialBladeScaleY, initialBladeLaserLineLength;
    private Rigidbody rb;
    private Vector3 initialBladeLocalPosition;

    //Only used during the Editor to react to turnOn changes for debugging purposes!
    private void OnValidate()
    {
        if (_turnOn != turnOn)
        {
            _turnOn = turnOn;

            if (turnOn && !isActivated)
            {
                GameManager.Instance().StartGame();
                Activate();
            }

            if (!turnOn && isActivated)
            {
                Deactivate();
            }
        }
    }

    private void Awake()
    {
        _turnOn = turnOn;

        // Get the DistanceGrabInteractable component
        grabbedEventSender = OVRDistanceHandGrabGameObject.GetComponent<ObjectGrabbedEventSender>();

        if (grabbedEventSender != null)
        {
            // Subscribe to grab (select) and release (unselect) events
            grabbedEventSender.OnObjectGrabbed += OnGrabbed;
            grabbedEventSender.OnObjectReleased += OnReleased;
        }

        initialBladeLocalPosition = BladeSwordMesh.transform.localPosition;

        rb = GetComponent<Rigidbody>();

        initialPitchRelfection = AudioSource.pitch;
        initialPitchSwordHum = AudioSourceLoop.pitch;   

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

        SetBladeColor(BladeColor);
        // UpdateLaserLength(0f);

    }

    private void SetBladeColor(Color bladedColor)
    {
        Light.color = bladedColor;
        bladeMaterial.color = bladedColor;
        bladeMaterial.SetColor("_Color", bladedColor);
        BladeLaserScript.LineColor = bladedColor;
    }

    private void OnDestroy()
    {
        if (grabbedEventSender != null)
        {
            // Subscribe to grab (select) and release (unselect) events
            grabbedEventSender.OnObjectGrabbed -= OnGrabbed;
            grabbedEventSender.OnObjectReleased -= OnReleased;
        }
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
            Debug.Log("activated: " + isActivated);
        }

        if (state == 2 || state == 3)
        {
            UpdateBladeState();
        }

        if (BladeSwordMesh.transform.localPosition != initialBladeLocalPosition)
            BladeSwordMesh.transform.localPosition = initialBladeLocalPosition;

        if (BladeSwordMesh.transform.localRotation.eulerAngles != Vector3.zero)
            BladeSwordMesh.transform.localRotation = Quaternion.Euler(Vector3.zero);

        AudioSource.pitch = initialPitchRelfection * Time.timeScale;
        AudioSourceLoop.pitch = initialPitchSwordHum * Time.timeScale;
    }

    private void UpdateBladeState()
    {
        bladeTime += Time.deltaTime;
        float percent = Mathf.Clamp01(bladeTime / ActivationTime);

        // Scale Y from 0 to the initial value during activation or vice versa during deactivation
        float currentScaleY = state == 3 ? percent * initialBladeScaleY : (1.0f - percent) * initialBladeScaleY;
        Light.intensity = state == 3 ? initialBladeLightIntensity * percent : ((1.0f - percent) * initialBladeLightIntensity);

        UpdateBladeScale(currentScaleY);


        if (bladeTime >= ActivationTime)
        {
            state = state == 3 ? 1 : 0; // Set final state (1 = on, 0 = off)

            if (state == 1) // When fully on, set light to max
                Light.intensity = initialBladeLightIntensity;
            else
                Light.intensity = 0;

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
        isActivated = value;

        if (state == 2 || state == 3 || (state == 1 && value) || (state == 0 && !value))
        {
            state = value ? 3 : 2; 
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
            if (randomBladeColor)
            {                                                                                                                              //Orange
                List<Color> bladeColors = new List<Color>() { Color.cyan, Color.blue, Color.red, Color.green, Color.magenta, Color.yellow, new Color(0.8f, 0.5f, 0f)};
                Color randomColor = bladeColors[Random.Range(0, bladeColors.Count)];
                SetBladeColor(randomColor);
            }
            else
            {
                SetBladeColor(BladeColor);
            }

            AudioSourceLoop.clip = ConstantSound;
            AudioSourceLoop.Play();
        }
        else
        {
            AudioSourceLoop.Stop();
        }

        return value;
    }

    public void Activate() => TurnOn(true);

    public void Deactivate() => TurnOn(false);

    public void SetActive(bool active)
    {
        if (active) Activate();
        else Deactivate();
    }
}
