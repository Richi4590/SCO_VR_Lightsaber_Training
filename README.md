This is a small simple XR Lightsaber Parrying Mini-Game I made during my Master's Degree of Interactive Media in 2025.

- Laser's can only be parried and not reflected back to something!
- You have three lives when pressing a colored button

Button color overviews:
- Red: 360° drone movement, normal drone shooting and travel speed
- Yellow: Drone tries to move only in front of your headset vision in a cone view, normal drone shooting and travel speed
- Orange: 360° drone movement, very fast shooting and travel speed
  
- You can change environment by pressing the white button on the pedestal!
- Requires an Oculus Quest 3 for XR!
- Quest 2 was not tested but should work in VR!

At the bottom of this README you can see two gameplay videos of the game!

# Oculus Setup and Unity Integration Guide

## Download the Oculus Software

Download the Oculus software from the official site:  
https://www.meta.com/at/en/quest/setup/

<img src="https://github.com/user-attachments/assets/dffa027b-4d9a-49f1-9621-79c8f989a312" width="500">

1. If you don't have an account, create one; otherwise, log in.
2. Connect your headset to your PC via Wi-Fi or cable.
3. Perform the connection test and switch connections if the speed is too low.

---

## Create a Developer Account

Visit https://developer.oculus.com and create a developer account.  
Complete the verification process by either:
- Adding a credit card, or
- Enabling two-factor authentication.

---

## Enable Developer Features in Oculus App

1. Restart the Oculus app and enable **Runtime Features for Developers**.
2. Enable **Passthrough via Oculus Link**.
3. Enable **Spacial Data via MetaQuest-Link**
4. Ensure **Developer Mode** and **USB Debugging** are enabled on the headset.

---

# Unity Setup for Oculus Development

### Unity Version
Please download and use Unity **2023.2.20f**.

---

## Download Required Meta XR Assets from Unity Asset Store

### Meta XR SDKs
1. **Meta XR Utility Kit**:  
   https://assetstore.unity.com/packages/tools/integration/meta-mr-utility-kit-272450

2. **Meta XR Core SDK**:  
   https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169

3. **Meta XR Interaction SDK**:  
   https://assetstore.unity.com/packages/p/meta-xr-interaction-sdk-265014

4. **Meta XR Interaction SDK Essentials**:  
   https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-essentials-264559

5. **Meta XR Platform SDK**:  
   https://assetstore.unity.com/packages/tools/integration/meta-xr-platform-sdk-262366

6. **Meta XR Simulator**:  
   https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-266732

<img src="https://github.com/user-attachments/assets/1f1050e0-3444-43ec-af6c-e4e9a8ffd171" width="500">   

## Download Required Assets from Unity Asset Store

### Sounds, SFX, Models Assets
1. **Free Laser Weapons**:  
   https://assetstore.unity.com/packages/audio/sound-fx/weapons/free-laser-weapons-214929

2. **SciFi Enemies and Vehicles**:  
   https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159

3. **Sci-Fi Styled Modular Pack**:  
   https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913

4. **Sherbb's Particle Collection**:  
   https://assetstore.unity.com/packages/vfx/particles/sherbb-s-particle-collection-170798

5. **Volumetric Lines**:  
   https://assetstore.unity.com/packages/tools/particles-effects/volumetric-lines-29160

6. **Lightsaber Base Asset**:  
   https://github.com/jjxtra/UnityLaserSword

---

## Unity Project Setup

### Create a New Unity Project
1. Open **Unity Hub** and create a new project.
2. Select the **3D (URP) Core** template, name your project, and create it.

<img src="https://github.com/user-attachments/assets/9de03a5c-d98b-44c6-81b9-e51b2f121961" width="500"> 

---

### Import Required Packages
1. Open **Window > Package Manager**.
2. Select the **My Assets** tab and import:
   - Meta XR Utility Kit 
   - Meta XR Core SDK
   - Meta XR Interaction SDK
   - Meta XR Interaction SDK Essentials
   - Meta XR Platform SDK
   - Meta XR Simulator
   - Free Laser Weapons
   - SciFi Enemies and Vehicles
   - Sci-Fi Styled Modular Pack
   - Sherbb's Particle Collection
   - Volumetric Lines

## IMPORTANT
If you have weird visual bugs with the Unity Editor itself, try to downgrade Versions of Metas XR Packages.

In my case I had everything updated to the latest (71.0.0 at the time) except "Meta XR Interaction SDK" and "Meta XR Interaction SDK Essentials", which where version 69.0.2, and 69.0.1 respectively.

After those packages were downgraded, Unity functioned properly again!

<img src="https://github.com/user-attachments/assets/ab217c4f-036a-4dce-b91d-420eefcb246c" width="500"> 

### TIP

You can use the easy "Add to My Assets" Button on the respective packages Website to make importing it easier!

<img src="https://github.com/user-attachments/assets/e6784739-192d-41cf-b7cf-a9d85e828ec4" width="500"> 

## Unity Project Settings

### XR Settings
1. Go to **Edit > Project Settings > XR Plug-in Management**.
2. Click **Install XR Plugin Management**.
3. Enable **Oculus** for PC and Android.
4. Add your headset under **Target Devices** in the Android Tab (in my case Ouest 2 and 3).

### Performance Settings

1. Go to **Edit > Project Settings > Quality**.
2. Click under **Levels** the **Balanced** profile and mark both PC and Android for it as default.
3. Go to **Edit > Project Settings > Graphics**.
4. Make sure that the "URP-Balanced (Universal Render Pipeline Asset) is set as the Default Render Pipeline!"

With this you get a nice balance (as the name suggests) between performance and visual fidelity!

<img src="https://github.com/user-attachments/assets/33fd153e-e753-4b77-a569-2d7e06e14e3e" width="500"> 

## Unity Build Settings

1. Open **File > Build Settings**.
2. Switch the Build Platform to **Android**.

<img src="https://github.com/user-attachments/assets/a0888522-4af1-4d72-a745-5af209afc9ff" width="500"> 

## Unity VR Scene Setup
Create a new scene and name it **Lightsaber_Training_VR**. This is where the lightsaber training in VR is done!

### Add the OVR Camera Rig
1. Delete the default camera from the scene.
2. Search and add the **OVRPlayerController** prefab to the scene (from the Meta XR SDK Core Package).  

Click the **OVRCameraRig** child object where the OVR Manager script component is attached to.

#### OVR Manager Settings
 1. Enable your headset in **Target Device**.
 2. Set **Tracking Origin Type** to **Floor Level**.
 3. Adjust the **General Tab Settings**:
    - **Hand Tracking Support**: Set to **Controllers Only**.
    - **Passthrough Support**: Set to **Required** or **Supported**.
    - **Scene Support**: Set to **Required** or **Supported**.
    - **Boundary Visibility Support**: Set to **Required** or **Supported**.
    - **Insight Passthrough and Guardian Boundary**: Set to **Enable Passthrough**.

<img src="https://github.com/user-attachments/assets/4528037c-80d3-4af9-88cb-952415694079" width="500"> 

#### Adding Building Blocks
 1. On the upper left side there should be a Button called **Meta XR Tools**
 2. Click on it and then **Building Blocks**
 3. Add the following:
    1. Passthrough
    2. Controller Tracking
    3. Distance Grab
    4. Grab Interactor
 4. Search the **ControllerPokeInteractor** prefab and add it as a child object to the Left and Right Controllers **ControllerInteractors** object respectively. Make sure to assign their components correctly! 

Your should now have 3x child objects for the left and right controllers **ControllerInteractors** object!

Each of these child objects manage their respective featue like distance and width of selecting objects from afar (**DistanceGrabInteractor**)

After the **Distance Grab** building block has been added, an additional building block called **[BuildingBlock] Cube** should have appeared as well in the scene!

Its child object is important later, so that the Lightsaber can be grabbed and moved towards a controller!

After the **Passthrough Building Block** has been added to the scene, a new object called **[BuildingBlock] Passthrough** should have appeard in the scene graph! Click on it and set its **Placement** to **Underlay**.

Add an empty child object to the **OVRCameraRig**, and name it **PlayerTarget**, this will be the point where **Enemies** will try to,
aim and shoot at the user! In a later section of this cookbook, a script will be attached to this object!

<img src="https://github.com/user-attachments/assets/02bee659-d679-43cf-8251-79d0168d12a6" width="500"> 

#### Set Camera Background
1. Navigate to **OVRCameraRig > TrackingSpace > CenterEyeAnchor > Environment**.
2. Set **Background Type** to **Solid Color**.
3. Adjust **Background** color to full transparency (0, 0, 0, 0).
4. Enable **Post Processing**

Congratulations! You should have a working XR Setup with Grabbing, Distance Grabbing, Poke and Passthrough interactions!

### Adding an Event System Object
1. If not already present: Add an empty game object to the scene and name it "EventSystem"
2. Add an **EventSystem** and **Standalone Input Module** components to it!

### Adding a Post Processing Object
1. Make sure you enable **Post Processing** on the **CenterEyeAnchor**!
2. Add an empty game object and call it **Post Processing**
3. Add a **Volume** component to it
4. Set it to **Global** mode with a **Weight** of 1 and **Priority** of 0
5. Create a new **Volume Profile**
6. Add a **Bloom** override
7. Adjust to your liking!

### IMPORTANT
When enabling **Post Processing** on the **CenterEyeAnchor**, you will loose the ability to see via **Passthrough**! So beware!

---

### Fix Errors
1. Go to on the upper left side there should be a Button called **Meta XR Tools**
2. Click **Project Setup Tool**
3. On the checklist for both **PC** and **Android**, click **Fix All** and **Apply All**.
4. Beware on any automatic adjustments that re-enabe hand interactions! Its probably best to set those errors to "Ignore", 
   so they wont appear again!

<img src="https://github.com/user-attachments/assets/fea5bbb0-a192-493f-b79b-d8561ed17af2" width="500"> 

# Game Implementation: Lightsaber Training

### Creating the Game Manager
To make the lightsaber trainging more engaging, we will create a simple **Game Manager** that keeps track of the amount of
**Lifes**, **Projectiles Parried** and the **Drone Activation Status** during a game. This script will be created as a **Singleton**, 
meaning that only one per scene is allowed! This is helpful, as we can access the **Game Manager** from any script at any given time 
and we can subscribe to its **Events** such as **OnGameStart**, **OnGameStop**, **OnDroneActivation**, **OnParriedShot**, **OnLifesLeft**.


### Game Manager Script
```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // The object "GameMusic" should have an AudioSource attached to it
    public AudioSource backgroundMusicAudioSource; 
    public AudioClip normalAmbienceMusic;
    public AudioClip parryingAmbienceMusic;
    public int initialLifes = 3;
    public bool startGame = false;

    public event Action OnGameStart;
    public event Action OnGameStop;

    public event Action<bool> OnDroneActivation;
    public event Action<int> OnParriedShot;
    public event Action<int> OnLifeLost;

    private static GameManager instance;
    private int lifes = 3;
    private int parriedShots = 0;
    private bool gameStarted = false;

   // rest of the code below
}
```

#### `Instance()`
```csharp
   public static GameManager Instance()
   {
      return instance;
   }
```
- Returns the singleton instance of the GameManager.

---

#### `Awake()`
```csharp

   void Awake()
   {
      if (instance != null)
         Destroy(this);

      instance = this;
   }
```

- Sets up the singleton instance of GameManager.
- Destroys duplicate instances to maintain a single active instance.

---

#### `Start()`
```csharp
   void Start()
   {
      OnGameStart += () => _OnGameStart();
      OnGameStop += () => _OnGameStop();
      lifes = initialLifes;
   }
```

- Subscribes _OnGameStart() and _OnGameStop() methods to the respective events.
- Initializes lifes to the value of initialLifes.

#### `Update()`
```csharp
   void Update()
   {
      if (startGame && !gameStarted)
      {
         startGame = false;
         StartGame();
      }
   }
```

- Monitors the startGame flag.
- Starts the game if startGame is set to true and the game hasn't already started.

---

#### `StartGame(), StopGame() and GameStarted()`
```csharp
   public void StartGame()
   {
      OnGameStart.Invoke();
   }

   public void StopGame()
   {
      OnGameStop.Invoke();
   }

   public bool GameStarted() => gameStarted;
```

- StartGame: Invokes the OnGameStart event to notify subscribers that the game has started.
- StopGame: Invokes the OnGameStop event to notify subscribers that the game has stopped.
- GameStarted: Returns a boolean about if the game is currently running or not.

---

#### `ProjectileParried()`
```csharp
   public void ProjectileParried()
   {
      parriedShots++;
      OnParriedShot.Invoke(parriedShots);
   }
```

- Increments the count of parried shots.
- Invokes the OnParriedShot event, passing the updated count.

--- 

#### `PlayerHit()`
```csharp
   public void PlayerHit()
   {
      lifes--;
      OnLifeLost.Invoke(lifes);

      if (lifes <= 0) 
         StopGame();
   }
```

- Reduces the player's lifes by 1.
- Invokes the OnLifeLost event with the updated lives count.
- Stops the game if the player has no lives remaining.

---


#### `_OnGameStart()`
```csharp
   private void _OnGameStart()
   {
      gameStarted = true;
      backgroundMusicAudioSource.Stop();
      backgroundMusicAudioSource.PlayOneShot(parryingAmbienceMusic);
      parriedShots = 0;
      lifes = initialLifes;

      OnDroneActivation.Invoke(true);
      OnParriedShot.Invoke(parriedShots);
      OnLifeLost.Invoke(lifes);
   }
```

- Logic executed when the game starts:
  - Sets gameStarted to true.
    - Plays the parrying ambience music.
    - Resets parriedShots and lifes.
    - Activates drones and notifies listeners of the initial game state

---

#### `_OnGameStop()`
```csharp
   private void _OnGameStop()
   {
      gameStarted = false;
      backgroundMusicAudioSource.Stop();
      backgroundMusicAudioSource.PlayOneShot(normalAmbienceMusic);
      OnDroneActivation.Invoke(false);
   }
```

- Logic executed when the game stops:
  - Sets gameStarted to false.
  - Plays the normal ambience music.
  - Deactivates drones.

In the end, the script with all its assignments should look like this:

<img src="https://github.com/user-attachments/assets/b7972921-2868-4f00-9451-6b463374b578" width="500"> 

### Creating the Player Hit Event script
In order to track if a player was hit by an object (eg. **LaserProjectile**), we need to create a small script and assign it to the
**OVRPlayerController**, where a **CharacterController** is assigned.

The code in the script itself is very small and understandable:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEvent : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Laser")
            GameManager.Instance().PlayerHit();
    
    }
}
```

We call our **GameManager's** *PlayerHit()* function whenever an object with the tag **Laser** collides with the player. 

#### Creating the Game Music Object
In the scene, create an empty gameobject called **GameMusic** and attach an **AudioSource** component to it. This will be our
**AudioSource** that plays background music during and after a game round. Add the initial music, into the **audio Resource** field and 
adjust the settings to your liking!

## Creating a Laser Projectile
Under **Assets > VolumetricLines > BuiltInRenderPipeline > Prefabs** you should have a bunch of Prefabs representing different 
kind of lasers. Drag the **SingleLine-LightSaber** prefab into the scene and create a new prefab called **Projectile**. This will be
our starting point for the projectile!

### TIP
It may happen that the Laser looks wrong or purple, because it was initially designed for the Built-in Rendering Pipeline.
If thats the case then, go to the **Materials** folder and select every single Material. Then go to **Edit > Rendering > Materials > Convert Selected Built-in Materials to URP**.
Now the selected materials, should look correct!

Add the following components:
 - A **Rigidbody**
 - A **Capsule Collider**

Make sure the **Rigidbody** has the following settings:

<img src="https://github.com/user-attachments/assets/5aa8636d-6a36-46b1-960b-0c21e97644e0" width="410"> 
<img src="https://github.com/user-attachments/assets/9fa66a9e-9c8f-44e8-b5f2-f0b00184adae" width="500"> 

Alter the existing **Volumetric Line Behaviour** component so it has these settings as a base:

<img src="https://github.com/user-attachments/assets/502aebf4-12ea-4e17-9a7f-1209218da55f" width="500"> 

Add an empty child object to the **Projectile** prefab and name it **Light**. Add a **Light** component to it and set it to
**Realtime**. Adjust the settings to your liking but for parrity's sake here are the used settings:

<img src="https://github.com/user-attachments/assets/f13ce808-55fe-460f-89a2-8c0c6384c8c9" width="500"> 

## Creating the Projectile Script

We will now create a script for projectiles that exhibit heat-seeking behavior, can reflect off surfaces, 
and produce visual/audio effects upon collision.

The `Projectile` script handles:
1. **Shooting a projectile** with specified velocity.
2. **Reflection mechanics**, allowing the projectile to bounce off objects like a lightsaber.
3. **Collision effects**, such as sparks, decals, and sound effects.
4. **Optional** **Heat-seeking behavior**, where the projectile adjusts its trajectory to follow a target.
---

```csharp
   public class Projectile : MonoBehaviour
   {
      public GameObject shooter = null; //  The object that fired the projectile (e.g., a player or enemy)
      public GameObject target = null; // The object the projectile should follow (if heat-seeking is enabled).

      public bool heatSeeking = false; // Toggles the heat-seeking behavior. When `true`, the projectile adjusts its trajectory toward the target.
      public bool reflected = false; // Indicates whether the projectile has been reflected (e.g., by a lightsaber).
      public bool debugRotation = false; //  Enables debugging by allowing manual adjustment of the projectile's rotation during runtime.

      public Vector3 rotationOffset = new Vector3(90, 0, 0); //  A rotation offset applied to the projectile's orientation for customization
      public float reflectionForceMultiplier = 1.0f; // Multiplier for reflected laser speed
      public float heatSeekingStrength = 1.0f; // Determines how strongly the projectile follows the target. Higher values make the trajectory sharper.

      public List<GameObject> sparksPrefabs; // A list of spark effects to instantiate upon collision.
      public List<GameObject> bulletHoleDecals; // A list of decals (e.g., bullet holes) to apply to surfaces on impact.
      public List<AudioClip> laserDeflectionSFX; // A list of sound effects for when the projectile is deflected.

      private Rigidbody rb;
      private Collider coll;
      private Vector3 currentVelocity = Vector3.zero; // Stores the projectile's current velocity for heat-seeking and reflection calculations.
      private bool applyFinalVelocity = false; // A flag to apply the reflected velocity in the next physics update.
   
      // upcoming code below
   }
```
---

#### `Awake()`
```csharp
private void Awake()
{
    rb = GetComponent<Rigidbody>();
    coll = GetComponent<Collider>();
}
```

--- 
#### `Update()`
```csharp
private void Update()
{
    if (debugRotation)
    {
        ChangeTarget(target);
    }

    if (heatSeeking && target != null)
    {
        // Adjust velocity to slightly move towards the target
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

        Vector3 newVelocity = Vector3.Lerp(
            rb.velocity.normalized,
            directionToTarget,
            Time.deltaTime * heatSeekingStrength
        ).normalized * currentVelocity.magnitude;

        rb.velocity = newVelocity;

        Quaternion targetRotation = Quaternion.LookRotation(newVelocity);
        
        // Offset rotation of the projectile
        Quaternion offsetRotation = Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z); 
        targetRotation *= offsetRotation;

        // Smoothly interpolate to the new rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                                             Time.deltaTime * heatSeekingStrength);
    }
}
```

#### Updates the projectile's behavior every frame.
 - Debug Mode:
   - If debugRotation is enabled, the projectile continuously updates its target. This is useful for testing and debugging rotation behavior.

  - Heat-Seeking Logic:
  - Direction to Target: Calculates the direction vector toward the target.
    - Velocity Adjustment: Gradually adjusts the projectile's velocity to move toward the target using Vector3.Lerp. The strength of adjustment is controlled by heatSeekingStrength.
    - Rotation Update: Smoothly aligns the projectile's rotation with the new velocity direction using Quaternion.Slerp.

--- 

#### `FixedUpdate()`
```csharp
private void FixedUpdate()
{
    if (applyFinalVelocity)
    {
        rb.velocity = currentVelocity;
        applyFinalVelocity = false;
    }
}
```
#### Purpose: 
  - Ensures the Rigidbody consistently uses the stored velocity for physics updates.

--- 
#### `ShootProjectile()`
```csharp
public void ShootProjectile(GameObject shooter, GameObject target, Vector3 velocity)
{
    this.shooter = shooter;
    this.target = target;
    ChangeTarget(target);

    transform.rotation = Quaternion.LookRotation(velocity.normalized);
    transform.rotation *= Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);

    currentVelocity = velocity;
    rb.velocity = currentVelocity;

    Destroy(this.gameObject, 10f);
    gameObject.SetActive(true);
}
```
#### Parameters:
 - shooter: The object firing the projectile.
 - target: The initial target for heat-seeking.
 - velocity: The initial velocity of the projectile.

Key Points:
Rotation and Velocity:

The projectile's rotation aligns with the velocity direction.
A rotational offset is applied.
Lifetime Management:

The projectile is destroyed after 10 seconds to optimize memory usage.

--- 
#### `ChangeTarget()`
```csharp
public void ChangeTarget(GameObject newTarget)
{
    target = newTarget;
}
```
- Updates the projectile's target (useful for retargeting during gameplay).

--- 
#### `SetReflected()`
```csharp
public void SetReflected(Vector3 newVelocity, bool reflected)
{
    currentVelocity = newVelocity;
    applyFinalVelocity = true;
    this.reflected = reflected;
}
```
- Marks the projectile as reflected and assigns a new velocity.

--- 
#### `OnCollisionEnter()`
```csharp
private void OnCollisionEnter(Collision collision)
{
   if (collision.collider.transform.tag != "Lightsaber")
   {
      if (collision.collider.transform.tag != "Player")
      {
            // Get the contact point and normal of the collision
            ContactPoint contact = collision.GetContact(0);

            InstantiateSpark(contact);
            InstantiateDecal(contact);
      }
      Destroy(this.gameObject);
      return;
   }

   //Lightsaber Blade Hit!
   Reflect(collision);
}
```
- Handles collision logic:
   - Instantiates effects for non-lightsaber collisions.
   - Calls Reflect() when hitting a lightsaber.

--- 
#### `Reflect()`
```csharp
    public void Reflect(Collision bladeCollision)
    {
        LayerMask layersToIgnore = LayerMask.GetMask("Laser", "Player");
        rb.excludeLayers = layersToIgnore;
        coll.excludeLayers = layersToIgnore;

        GameManager.Instance().ProjectileParried();
        PlayRandomDeflectionSound(bladeCollision.collider.GetComponent<AudioSource>());
        InstantiateSpark(bladeCollision.GetContact(0));

        // Prevent angular motion
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Capture the velocity before Unity modifies it
        Vector3 incomingVelocity = currentVelocity;
        float incomingSpeed = incomingVelocity.magnitude; // Preserve the speed

        // Get the contact point and normal
        ContactPoint contact = bladeCollision.GetContact(0);
        Vector3 hitNormal = contact.normal;

        // Calculate the reflection direction
        Vector3 reflectedDirection = Vector3.Reflect(incomingVelocity.normalized, hitNormal);

        // Align the projectile's rotation with the reflected velocity
        Quaternion targetRotation = Quaternion.LookRotation(reflectedDirection);
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 0);

        // Calculate the new velocity while preserving speed
        Vector3 velocityAfterReflection = reflectedDirection * incomingSpeed * reflectionForceMultiplier;

        // Mark the laser as reflected
        SetReflected(velocityAfterReflection, true);
        // Debugging: Visualize the reflection
        Debug.DrawRay(contact.point, reflectedDirection, Color.blue, 2.0f);
        
    }
```
- Reflects the projectile using physics:
   - Calculates the reflection vector with Vector3.Reflect.
   - Adjusts the rotation and velocity based on the reflection.

--- 
#### `PlayRandomDeflectionSound()`
```csharp
private void PlayRandomDeflectionSound(AudioSource deflectionAudioSource)
{
    deflectionAudioSource.pitch = Time.timeScale * Random.Range(0.9f, 1.2f);
    deflectionAudioSource.PlayOneShot(laserDeflectionSFX[Random.Range(0, laserDeflectionSFX.Count)]);
}
```
- Plays a random sound effect when the projectile is reflected.

--- 
#### `InstantiateSpark()`
```csharp
private void InstantiateSpark(ContactPoint contact)
{
    GameObject sparkSFX = Instantiate(
        sparksPrefabs[Random.Range(0, sparksPrefabs.Count)], 
        contact.point, 
        Quaternion.LookRotation(contact.normal)
    );

    float longestWaitingTime = 0; // Tracks the longest particle duration
    int timesSmaller = 10;       // Scale factor for sparks
    sparkSFX.transform.localScale = sparkSFX.transform.localScale / timesSmaller;

    // Get the main particle system's duration
    longestWaitingTime = sparkSFX.GetComponent<ParticleSystem>().main.duration;

    // Adjust scale and duration for child particle systems
    for (int i = 0; i < sparkSFX.transform.childCount; i++)
    {
        GameObject child = sparkSFX.transform.GetChild(i).gameObject;
        child.transform.localScale = sparkSFX.transform.localScale / timesSmaller;

        ParticleSystem childParticle = child.GetComponent<ParticleSystem>();
        if (childParticle != null && childParticle.main.duration > longestWaitingTime)
            longestWaitingTime = childParticle.main.duration;
    }

    Destroy(sparkSFX, longestWaitingTime); // Destroy spark after its effects finish
}
```
1. Instantiate Spark Effect
   - Selects a random spark prefab from the sparksPrefabs list.
   - Instantiates it at the collision point (contact.point), oriented to face outward using contact.normal.

2. Scaling
   - The sparks' size is reduced by dividing the original scale by timesSmaller.

3. Particle Duration
   - Determines the duration of the main and child particle systems to ensure the spark exists long enough for all effects to complete.

4. Destroy the Spark
   - The spark effect is destroyed after its longest particle duration to optimize memory usage.

--- 
#### `InstantiateDecal()`
```csharp
private void InstantiateDecal(ContactPoint contact)
{
    GameObject bulletDecal = Instantiate(
        bulletHoleDecals[Random.Range(0, bulletHoleDecals.Count)], 
        contact.point + (Vector3.up / 100f), 
        Quaternion.LookRotation(contact.normal)
    );

    Destroy(bulletDecal, 10); // Automatically remove decal after 10 seconds
}
```
1. Instantiate Decal
   - Selects a random bullet hole decal from the bulletHoleDecals list.
   - Instantiates it slightly above the collision point (contact.point + Vector3.up / 100f) to prevent clipping and **Z-Fighting** 
     with the surface.
   - Aligns the decal with the surface's normal using Quaternion.LookRotation(contact.normal).

2. Destroy the Decal 
   - The decal is destroyed after 10 seconds to prevent memory buildup and clutter in the scene.

## -- CAUTION: UNITY PHYSICS SHENANIGANS--
This was one of my hardest to find bugs during development!

Sometimes lasers that were parried by the lightsaber had almost no velocity!!!

Unity's collision detection tries to slow down the projectile when colliding with something, 
as it normally should. However, as we want this projectile to hit a **Lightsaber** blade and 
reflect off of it, with either the same or faster speed the projectile was shot from following additionas and fix 
was necessary:
   - Keep applying the **currentVelocity** value to the rigidbody in `FixedUpdate()` to 
     ensure a persistent speed!
   - In the `Reflect()` function, instead of using:
   ```csharp
        Vector3 incomingVelocity = rb.velocity;
   ```
   - you should use:
   ```csharp
        Vector3 incomingVelocity = currentVelocity;
   ```

With with rb.velocity, Unity first applies an almost complete stop to the rigidbody
and only then the **Reflect** function is called to parry the laser!

---

With everything in place the complete **Projectile** should look like this:

<img src="https://github.com/user-attachments/assets/60f3debe-3015-4d88-b610-26b5de307d7d" width="500"> 

## Creating a Lightsaber
 ###  Major Adjustments
 ####  Prefab
 Inside the cloned repository of the lightsaber asset, copy the folder under **Assets > LaserSword** into your assets folder.
 Open the folder and go to the **Prefabs** folder and drag the single prefab **LaserSwordPrefab** into the scene.
 
 We need to make an assortment of different adjustments to it such as: 
 
 1. Drag the following objects to the **LightsaberSwordHilt** to make them its child objects:
      - **LaserSwordLight**
      - **LaserSwordAudioSource**
      - **LaserSwordAudioSourceLoop**
      - **BladeStart**
      - **BladeEnd**
 2. Delete the **LaserSwordBladeGlow** child object, as the shader is not usable in URP and even if it were, it would not look as good as the shader used for the laser projectile
 3. The following component adjustments to **LightsaberSwordHilt** are necessary:
    - Copy the **LightsaberSwordScript** from the root object **LaserSwordPrefab** to the **LaserSwordHilt**.
    - **Mesh Collider** (already present), make it **Convex**
    - Add a **Rigid Body** component, with the following settings:
         - **Mass**: Set to **1**.
         - **Drag**: Set to **0**.
         - **Angular Drag**:  Set to **0.1**.
         - **Use Gravity**: Set to **True**
         - **Is Kinematic**: Set to **True**.
         - **Interpolate**: **Interpolate**, so the object moves smoothly between physics updates
         - **Collision Detection**: **Continous Dynamic**, so the projectiles accurately collide with it
 4. Click and drag the **LightsaberSwordHilt** back into the project window to make a prefab out of it. 
    This will act as the new root of the new lightsaber object with all of the other objects as its children.
 5. Just like the creation of  the **Laser** projectile, under **Assets > VolumetricLines > BuiltInRenderPipeline > Prefabs** you should have a bunch of 
    Prefabs representing different kinds of lasers. Drag the **SingleLine-LightSaber** prefab into the scene and make it as the child object 
    of **LaserSwordBlade**. Be sure to make completely new prefab of the laser prefab to not accidentally mess with the original version!
 6. **LaserSwordBlade** will act as the **invisible** collider that reacts to the laser projectiles
 7. Adapt the **SingleLine-LightSaber** so its aproximately the length and width of the collider under **LaserSwordBlade**
    - Be sure to disable **Do Not Overwrite Tempalte Material Properties**
 8. **LaserSwordBlade** requires the following components:
    -  **Mesh Collider** (already present)
    -  **Rigid Body**, with the following settings:
          - **Mass**: Set to **0 or 0.1**.
          - **Drag**: Set to **0**.
          - **Angular Drag**:  Set to **0**.
          - **Use Gravity**: Set to **False**
          - **Is Kinematic**: Set to **True**.
          - **Interpolate**: **Interpolate**, so the object moves smoothly between physics updates
          - **Collision Detection**: **Continous Dynamic**, so the projectiles accurately collide with it
 9. Add two empty game objects to the **LightsaberHilt** and name them **GrabPoint** and **GrabPoint2**
 10. Position them so that the first **GrabPoint** is within the **LightsaberHilt** itself but on the upper part, while the second one is on the lower part of the hilt.
 11. For each of the two **GrabPoint** objects, assign a **GrabFreeTransformer** component to it and set the following:
       - **Constraints are Relative**: Set to **True**
       - Everything **Constrain** checkbox for **Position** and **Rotation** to **False**
 12. Just like in section **Adding Building Blocks**, you will require to add a **Distance Grab** building block with the
     **Pull Interactable To Hand** mode selected into the scene.
 13. An additional building block called **[BuildingBlock] Cube** should have appeared.
 14. Take its child object **[BuildingBlock] DistanceHandGrab** and make it as a child to the **LightsaberHilt** object.
 15. On the **[BuildingBlock] DistanceHandGrab**, make sure that the required rigidbody fields are assigned to the hilt's rigidbody and
     that the **Reticle Data Mesh** component has the **Mesh Filter** of the **LightsaberHilt** assigned to it!
 16. For the **Grabbable** component make sure to do the following:
      - Under *Optionals*, assign to the **One Grab Transformer** the **GrabPoint's Grab Free Transformer** component and the same for the **Two Grab Transformer**
      with the **GrabPoint2's Grab Free Transformer**
      - **Transfer On Second Selected** to **False**
      - Set the **Max Grab Points** to **2**
      - **Kinematic While Selected** to **True**
      - **Throw When Unselected** to **True**
 17. Assign to the **[BuildingBlock] DistanceHandGrab** the **ObjectGrabbedEventSender** that will be created and discussed in the next step.

The object hierarchy of the lightsaber prefab should look like this:

<img src="https://github.com/user-attachments/assets/08caca15-7d76-440f-aab4-936445c6ae2d" width="500"> 

---
 ###  Creating an **ObjectGrabbedEventSender** Script
 In order to know when an object has been grabbed or not, it was necessary to create a brand new script called
 **ObjectGrabbedEventSender** and assign it to the previously mentioned **[BuildingBlock] DistanceHandGrab** object.

```csharp
using Oculus.Interaction;
using System;
using UnityEngine;

public class ObjectGrabbedEventSender : MonoBehaviour
{
    public event Action<GameObject> OnObjectGrabbed;
    public event Action<GameObject> OnObjectMoved;
    public event Action<GameObject> OnObjectReleased;

    private Grabbable _grabbable;

    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();

        if (_grabbable == null)
        {
            Debug.LogError("Grabbable is missing! This script requires a Grabbable component.");
        }
    }

    private void OnEnable()
    {
        // Subscribe to Grabbable events
        _grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    private void OnDisable()
    {
        // Unsubscribe from Grabbable events
        _grabbable.WhenPointerEventRaised -= HandlePointerEvent;
    }

    private void HandlePointerEvent(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                OnObjectGrabbed?.Invoke(gameObject);
                break;
            case PointerEventType.Move:
                OnObjectMoved?.Invoke(gameObject);
                break;
            case PointerEventType.Unselect:
                OnObjectReleased?.Invoke(gameObject);
                break;
        }
    }
}
```
The script itself is pretty straight forward, three events are triggered when the object your aiming at or near with your controller
is being grabbed, moved or ungrabbed! Scripts that need to know if the object they are attached to are grabbed or not need to subribe to them!
This can be used for any object and for many different use cases! 

In our case, the upcoming **LaserSwordScript** subscribes to it, to know if the Lightsaber is held or not!

---
###  **LaserSwordScript** Code
 A LOT of changes were made to this code! So much so that it would be too much to do a side by side comparison of it!
   
```csharp
// LaserSword for Unity
// (c) 2016 Digital Ruby, LLC
// Refactored for efficiency

using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

namespace DigitalRuby.LaserSword
{
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

        // Button to activate/deactivate (X Button)
        public OVRInput.Button activationButton = OVRInput.Button.Two; 
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

            BladeSwordMesh.transform.localScale = new Vector3(BladeSwordMesh.transform.localScale.x, 
                                                              percent, 
                                                              BladeSwordMesh.transform.localScale.z);
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
                {                                                                                                                             
                    List<Color> bladeColors = new List<Color>() { Color.cyan,
                                                                  Color.blue, 
                                                                  Color.red, 
                                                                  Color.green, 
                                                                  Color.magenta, 
                                                                  Color.yellow, 
                                                                  new Color(0.8f, 0.5f, 0f)};  //Orange
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
}

```
These is a summary of the changes compared to the initial version of the script:

#### New things: 

1. Blade Color Customization:
   - Random Blade Color:
     - Introduced a randomBladeColor boolean that enables random blade colors when activated. It selects a color from a predefined list (e.g., Color.blue, Color.red).
     - This replaces static blade color settings in the initial version.

2. Audio System Enhancements:
   - Pitch and Looping Sounds:
   - Introduced dynamic pitch adjustments for audio based on Time.timeScale.
   - Added specific start and stop sounds for blade activation, plus a looping sound for when the blade is active.

3. Grab and Release Event Handling:
   - ObjectGrabbedEventSender: A new event listener component was added to handle when the sword is grabbed or released, which in turn determines if the blade can be activated.
   - Grab-based Blade State Management: Added logic to allow the sword to only toggle its state when grabbed, using button press interactions to activate/deactivate.

4. Performance Optimizations:
   - Caching Materials and Components: The script now caches commonly used components (like the blade material, light component, etc.) in Awake() to optimize performance, removing the need for repeated lookups.

5. Blade Initialization:
   - InitializeBlade(): A function added to initialize the blade with the default state, including scaling and initial color.

6. Light Intensity Transitions:
   - Blade light intensity is smoothly adjusted based on its state, ensuring more gradual transitions during activation and deactivation.

7.  Blade Position and Rotation Fixes:
   - Added logic to ensure that the blade's mesh position and rotation are reset and handled correctly.

#### Removed things:
1. Unused Variables and Components:
   - LaserSwordProfileScript: Removed the reference to LaserSwordProfileScript, which was previously responsible for handling 
     audio, color, and glow settings. In the new version, these are managed directly in the script.
   - BladeGlowRenderer: Removed the glow effect logic. This was replaced with simpler material-based adjustments for the blade.
   - swordBlock and glowBlock: Removed the Material Property Block system previously used for controlling the blade’s glow and light intensity.

2. Legacy Blade Activation Logic:
   - Removed complex blade activation logic that previously involved using LaserSwordProfileScript to modify the glow and intensity over time.
   - The initial version had more intricate transitions and glow effects, which were replaced with smooth scaling and intensity changes in the new version.

3. Obsolete Functions:
   - CheckState(): This function used to check the state of the blade was replaced by the more efficient UpdateBladeState().
   - UpdateLaserLength(): The old script had logic for updating the laser's length based on some parameters, which is no longer 
     necessary in the current version.

This is how the final **LaserSword** script looks like with all of its assignments:

<img src="https://github.com/user-attachments/assets/c1ac00c5-4eda-4cad-ac6d-be73d898cafb" width="500">

---

### Adjusting Collider / Rigidbody Exclusions
It is important to adjust certain colliders and rigidbodies to exclude other objects that have a specific **Layer** set.

The following changes are required to be made to the objects that have a **Collider** and **Rigidbody** to them:

### Tags and Layers
 - **LightsaberHilt** and **LaserSwordBlade** need to have a "Lightsaber" **Tag** and **Layer** name attached to it
 - **OVRPlayerController** needs to have a "Player" **Tag** and **Layer** name attached to it
 - The **Laser Prefab** we created earlier needs to have a "Laser" **Tag** and **Layer** name attached to it

## Creating An Enemy (Drone)
We need an enemy that shoots instances of our **Laser Prefab** at us. This can be a robot, a static turret or in our case a **Drone**.
 ### Prefab
 Within the Assets folder go to **PopupAsylum > PA_SciFiCombatants > _Imported3D > Characters**. You should see a prefab **PA_Drone** inside it and
 a bug-like drone for the ground. 
 
 1. Drag the **PA_Drone** into the scene
 2. Create an empty gameobject and call it **PA_Training_Drone**
 3. Make **PA_Drone** its child object
 4. Create two child objects and assign them to **PA_Training_Drone**
 5. Name them **HoverSoundAudioSourceLoop** and **LaserShotAudioSourceSingle**
 6. Add to each of them an **AudioSource** component
 7. Adjust the audio settings to your liking! 
      - Be sure to set **HoverSoundAudioSourceLoop's Loop** setting to **True**
 8. Create an empty child object to **PA_Drone** and move it so that its positioned at the tip of its turret mesh
      - This will server as our position to where **Laser Projectiles** will be shot from.
 9. Add a **Point Light** object to the **PA_Drone** and adjust the color, size and intensity to your liking, so it is easier to spot in dark areas!
10. In the root of the object **PA_Training_Drone** we need to attach the following components to it: 
    -  **Rigid Body**, with the following settings:
          - **Mass**: Set to **1**.
          - **Drag**: Set to **2**.
          - **Angular Drag**:  Set to **0.05**.
          - **Use Gravity**: Set to **False**
          - **Is Kinematic**: Set to **False**.
          - **Interpolate**: **Interpolate**, so the object moves smoothly between physics updates
          - **Collision Detection**: **Continous**
   - Create and attach the **Training Drone** script which will be created and discussed now:

 ### Creating the **Training Drone** Script
 We need to create a script called **Training Drone**. This is where all the logic regarding shooting and flying is going to happen.
 We first need to establish our parameters, which are the following: 
   ```csharp
   using System.Collections.Generic;
   using UnityEngine;

   public class TrainingDrone : MonoBehaviour
   {
      [Tooltip("Reference to the VR player or camera.")]
      public Transform user; 

      [Tooltip("Prefab for the projectile the drone will fire.")]
      public GameObject projectilePrefab;

      [Tooltip("Transform from where the projectile will be fired.")]
      public Transform projectileSpawnPoint;

      [Tooltip("Audio clips for laser sound effects.")]
      public List<AudioClip> laserShotSFX;

      [Header("Main Settings")]
      [Tooltip("Enable or disable the drone.")]
      public bool enabled = true;

      [Tooltip("If true, the drone remains stationary.")]
      public bool stayStationary = false;

      [Tooltip("If true, the drone orbits the player. If false, it moves in an arc.")]
      public bool orbitMode = true;

      [Tooltip("Time in seconds before the drone starts shooting for the first time.")]
      public float startFirstShootingAfterNSeconds = 2f;

      [Header("Shooting Settings")]
      [Tooltip("Minimum interval between consecutive shots.")]
      public float minShootInterval = 0.1f;

      [Tooltip("Maximum interval between consecutive shots.")]
      public float maxShootInterval = 2f;

      [Tooltip("Minimum speed of the fired projectile.")]
      public float minProjectileSpeed = 5f;

      [Tooltip("Maximum speed of the fired projectile.")]
      public float maxProjectileSpeed = 12f;

      [Tooltip("Minimum Y-offset for randomizing projectile firing direction.")]
      public float minLaserYDirectionOffset = -0.2f;

      [Tooltip("Maximum Y-offset for randomizing projectile firing direction.")]
      public float maxLaserYDirectionOffset = 0.1f;

      [Header("Stationary Settings")]
      [Tooltip("Minimum time the drone stays stationary after reaching a target.")]
      public float minStationaryTime = 1f;

      [Tooltip("Maximum time the drone stays stationary after reaching a target.")]
      public float maxStationaryTime = 3f;

      [Header("Flying Settings")]
      [Tooltip("Minimum distance to consider the drone has reached a target.")]
      public float minDistanceToTriggerTarget = 0.2f;

      [Tooltip("Force applied to move the drone towards its target.")]
      public float moveForce = 10f;

      [Tooltip("Distance maintained between the drone and the player.")]
      public float distanceFromPlayer = 5f;

      [Tooltip("Vertical offset for the drone’s position relative to the player.")]
      public float droneHeightOffset = 1;

      [Tooltip("Range for up and down movement.")]
      public float verticalRange = 2f;

      [Tooltip("Time interval after which the drone may switch movement direction.")]
      public float directionSwitchInterval = 2.5f;

      [Header("Orbit Settings")]
      [Tooltip("Minimum angular distance between orbit targets.")]
      public float minOrbitAngleDistance = 5f;

      [Tooltip("Maximum angular distance between orbit targets.")]
      public float maxOrbitAngleDistance = 45f;

      [Header("Arc Settings")]
      [Tooltip("Width of the arc for movement when not orbiting.")]
      public float arcWidth = 180f;

      [Header("Audio Settings")]
      [Tooltip("Audio source for laser firing sound effects.")]
      public AudioSource laserShotAudioSource;

      [Tooltip("Audio source for engine sound.")]
      public AudioSource engineAudioSource;

      [Tooltip("Minimum pitch for engine audio.")]
      public float pitchMin = 0.8f;

      [Tooltip("Maximum pitch for engine audio.")]
      public float pitchMax = 2f;

      // Private Variables
      private float shootTimer; // Timer controlling when to shoot next
      private float stationaryTimer = 0f; // Timer for stationary state duration
      private bool gracePeriodOver = false; // Indicates if the initial shooting grace period is over
      private float initialShootingTimer = 0f; // Tracks time for the first shot delay
      private Vector3 randomTargetPosition; // Randomly chosen target position
      private Rigidbody rb; // Rigidbody for movement physics
      private float currentOrbitAngle = 180f; // Angle used for orbit movement
      private bool reachedTarget = false; // Flag indicating if the drone reached its target

      private float timeSinceLastSwitch = 0f; // Timer to track direction switching for orbiting
      private bool clockwise = true; // Determines orbit direction (clockwise or counter-clockwise)

      private float initialMoveForce; // Original move force value (used for resetting)
      private float initialMinShootInterval; // Original minimum shoot interval
      private float initialMaxShootInterval; // Original maximum shoot interval
      private float initialPitch; // Initial audio pitch for laser sounds

      // upcoming code below
   }
   ```
---

   #### `Start()`
   ```csharp
   private void Start()
   {
      initialMoveForce = moveForce;
      initialMinShootInterval = minShootInterval;
      initialMaxShootInterval = maxShootInterval;

      InitDrone();
      ChooseRandomTarget(); // Pick the initial random target

      // Ensure the Rigidbody exists
      rb = GetComponent<Rigidbody>();
      rb.useGravity = false;

      initialPitch = laserShotAudioSource.pitch;

      GameManager.Instance().OnGameStart += () => EnableDrone();
      GameManager.Instance().OnGameStop += () => StopDrone();

      if (user == null)
      {
         Debug.LogError("User is not assigned to the drone!");
         return;
      }
   }
   ```

   - Explanation:
     - Initialization: Saves initial movement and shooting values for resetting later.
     - RigidBody Setup: Ensures the Rigidbody is present and disables gravity for hovering behavior.
     - Drone Setup: Calls InitDrone() to initialize internal variables and sets the first target position with ChooseRandomTarget().
     - Game Events: Subscribes to game start/stop events for enabling/disabling the drone.
---

   #### `FixedUpdate()`
   ```csharp
   private void FixedUpdate()
   {
      if (enabled)
      {
         if (!stayStationary)
               MoveDrone();
      }
   }
   ```

   - Explanation:
      - Movement Logic: Calls MoveDrone() if the drone is enabled and not stationary.
      - Ensures movement physics updates are performed consistently.

---

   #### `Update()`
   ```csharp
   private void Update()
   {
      if (enabled)
      {
         // Update time since last direction switch
         shootTimer -= Time.deltaTime;
         timeSinceLastSwitch += Time.deltaTime;

         if (reachedTarget)
               stationaryTimer -= Time.deltaTime;

         if (!gracePeriodOver)
         {
               initialShootingTimer += Time.deltaTime;

               if (initialShootingTimer > startFirstShootingAfterNSeconds)
                  gracePeriodOver = true;
         }

         transform.LookAt(user); // Always face the user

         HandleShooting();
         AdjustEnginePitch();
      }

      laserShotAudioSource.pitch = Time.timeScale * initialPitch;
   }
   ```

   - Explanation:
     - Timers: Updates multiple timers (e.g., shooting, direction switching, stationary).
     - Grace Period: Monitors time until the drone starts shooting after activation.
     - User Tracking: Ensures the drone faces the user at all times using transform.LookAt(user).
     - Shooting & Audio: Calls HandleShooting() to fire projectiles and AdjustEnginePitch() to manage engine sound dynamics.

---

   #### `EnableDrone(), StopDrone(), EnableDisableDrone() and InitDrone()`
   ```csharp
   public void EnableDrone()
   {
      InitDrone();
      this.enabled = true;
   }

   public void StopDrone()
   {
      this.enabled = false;
   }

   public void EnableDisableDrone()
   {
      this.enabled = !this.enabled;

      if (this.enabled)
         EnableDrone();
      else
         StopDrone();
   }

   public void SetToChaoticDroneMode() 
   {
      moveForce = initialMoveForce * 2;
      minShootInterval = initialMinShootInterval * 0.25f;
      maxShootInterval = initialMaxShootInterval * 0.25f;
   }

   private void InitDrone()
   {
      moveForce = initialMoveForce;
      minShootInterval = initialMinShootInterval;
      maxShootInterval = initialMaxShootInterval;

      shootTimer = Random.Range(minShootInterval, maxShootInterval);
      stationaryTimer = 0;
      gracePeriodOver = false;
      initialShootingTimer = 0;
   }

   ```
   
   - Explanation:
     - EnableDrone: Enables the drone's functionality.
       - Initialization: Resets drone variables using InitDrone(). 
     - StopDrone: Disables the drone, halting all movement and shooting.
     - EnableDisableDrone: Simple function to toggle start and stop of the drone
     - SetToChaoticDroneMode: Amplifies the movement force and the makes the min and max drone shoot interval smaller (shoots faster)

---

   #### `MoveDrone()`
   ```csharp
   private void MoveDrone()
   {
      if (orbitMode)
      {
         // Orbit around the user (this will use an arc to move around the user)
         OrbitAroundUser();
      }
      else
      {
         // Move within a defined arc in front of the user
         MoveInArc();
      }
   }
   ```
   
   - Explanation:
     - Mode-Based Movement:
       - Calls OrbitAroundUser() if orbitMode is enabled.
       - Calls MoveInArc() for arc-based movement when orbitMode is disabled.

---

   #### `OrbitAroundUser()`
   ```csharp
   private void OrbitAroundUser()
   {
      RotateTowards(randomTargetPosition); // Rotate towards the target position first

      if (Vector3.Distance(transform.position, randomTargetPosition) < minDistanceToTriggerTarget)
      {
         reachedTarget = true;

         if (stationaryTimer <= 0f)
         {
               // If the drone is close enough to the target, choose a new target
               stationaryTimer = Random.Range(minStationaryTime, maxStationaryTime);
               ChooseRandomTarget(); // Choose a new random target after stationary time
         }
      }
      else
      {
         ApplyForceTowards(randomTargetPosition); // Then apply force towards the target after rotating
      }
   }
   ```
   - Explanation:
     - Orbit Movement:
       - Rotates the drone toward the target position using RotateTowards().
       - Checks if the target is reached and, if so, picks a new target after a stationary interval. 
       - Applies force toward the target with ApplyForceTowards() if the target isn't reached.

---

   #### `MoveInArc()`
   ```csharp
   private void MoveInArc()
   {
      RotateTowards(randomTargetPosition); // Rotate towards the target position first

      if (Vector3.Distance(transform.position, randomTargetPosition) < minDistanceToTriggerTarget)
      {
         reachedTarget = true;

         if (stationaryTimer <= 0f)
         {
               // If the drone is close enough to the target, choose a new target
               stationaryTimer = Random.Range(minStationaryTime, maxStationaryTime);
               ChooseRandomTarget(); // Choose a new random target after stationary time
         }
      }
      else
      {
         ApplyForceTowards(randomTargetPosition); // Apply force towards the target after rotating
      }
   }

   ```
   - Explanation:
     - Target Rotation: The drone first rotates toward the randomTargetPosition using RotateTowards() to ensure it faces the intended direction before moving.
     - Target Proximity Check:
       - If the drone is within the specified minDistanceToTriggerTarget of its target, it's considered to have "reached" the target, setting reachedTarget to true.
       - A stationary timer is initialized to a random value between minStationaryTime and maxStationaryTime, causing the drone to pause briefly.
       - After the stationary interval expires, a new target is chosen using ChooseRandomTarget().
     - Movement Towards Target:
       - If the drone hasn't reached the target, it moves closer by applying force in the target's direction with ApplyForceTowards().

   #### Key Difference from Orbit Mode:
   While both MoveInArc() and OrbitAroundUser() involve moving toward a target, MoveInArc() is specific to movement constrained 
   to a predefined arc in front of the user, providing a more predictable and controlled movement pattern compared to the continuous 
   orbiting behavior of OrbitAroundUser().

---

   #### `ApplyForceTowards()`
   ```csharp
   private void ApplyForceTowards(Vector3 targetPosition)
   {
      // Calculate the direction to the target
      Vector3 direction = targetPosition - transform.position;

      // Get the distance to the target
      float distance = direction.magnitude;

      // If very close to the target, stop applying force (or you can add more logic here to stop movement)
      if (distance < minDistanceToTriggerTarget)
      {
         return;
      }

      // Normalize the direction vector
      Vector3 normalizedDirection = direction.normalized;

      // Apply force towards the target position
      rb.AddForce(normalizedDirection * moveForce, ForceMode.Acceleration); // Use ForceMode.Force for gradual movement
   }
   ```
   - Explanation:
     - This function ensures that the drone moves toward a specified target position by applying a force.
   - Steps:
     - Calculate the direction vector pointing from the drone's current position to the targetPosition.
     - If the distance to the target is less than minDistanceToTriggerTarget, the function stops further movement.
     - Normalize the direction vector to ensure consistent movement speed regardless of the distance.
     - Apply a force in the normalized direction using the moveForce value.

---

   #### `RotateTowards()`
   ```csharp
   private void RotateTowards(Vector3 targetPosition)
   {
      // Calculate direction to target
      Vector3 direction = targetPosition - transform.position;

      // Calculate the rotation step
      Quaternion targetRotation = Quaternion.LookRotation(direction);

      // Smoothly rotate towards the target position using slerp
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * moveForce);
   }
   ```
   - Explanation:
     - Rotates the drone smoothly to face the targetPosition using Spherical Linear Interpolation (Slerp).
       - Steps:
         - Compute the direction vector from the drone's position to the target.
         - Calculate the target rotation using Quaternion.LookRotation(direction).
         - Smoothly interpolate the drone's current rotation to the target rotation over time, factoring in moveForce for speed control.

 ---

   #### `ChooseRandomTarget()`
   ```csharp
   private void ChooseRandomTarget()
   {
      if (orbitMode)
      {
         // Orbit mode: pick a target position that forms an orbit around the user
         ChooseOrbitTarget();
      }
      else
      {
         // Arc mode: pick a random position within an arc in front of the user
         ChooseArcTarget();
      }

      reachedTarget = false;
   }
   ```
   - Explanation:
     - Determines whether to select a target for orbiting or arcing based on the orbitMode flag.
     - If orbitMode is true, it calls ChooseOrbitTarget(); otherwise, it calls ChooseArcTarget().
     - Resets reachedTarget to false so the drone begins moving toward the new target.

---

   #### `ChooseOrbitTarget()`
   ```csharp
   private void ChooseOrbitTarget()
   {
      // Randomly switch direction at the given interval
      if (timeSinceLastSwitch >= directionSwitchInterval)
      {
         clockwise = Random.value > 0.5f; // Randomly switch direction
         timeSinceLastSwitch = 0f; // Reset the time counter
      }

      // Get a random angular distance between min and max
      float angleDistance = Random.Range(minOrbitAngleDistance, maxOrbitAngleDistance);

      // Update the current orbit angle by adding the random angular distance
      if (clockwise)
      {
         currentOrbitAngle += angleDistance; // Rotate clockwise
      }
      else
      {
         currentOrbitAngle -= angleDistance; // Rotate counter-clockwise
      }

      // Ensure the angle wraps around 360°
      if (currentOrbitAngle >= 360f)
      {
         currentOrbitAngle -= 360f;
      }
      else if (currentOrbitAngle < 0f)
      {
         currentOrbitAngle += 360f;
      }

      // Convert the angle to radians
      float radians = Mathf.Deg2Rad * currentOrbitAngle;

      // Generate the orbit position based on the new angle
      Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * distanceFromPlayer;

      // Apply vertical offset
      float verticalOffset = Random.Range(-verticalRange, verticalRange);
      offset.y = droneHeightOffset + verticalOffset;

      // Set the new target position
      randomTargetPosition = user.position + offset;
   }
   ```
   - Explanation:
     - Calculates a new target position for the drone to orbit around the user in a circular path.
       - Steps:
         - Randomly switches the orbit direction (clockwise/counterclockwise) at regular intervals.
         - Updates the current orbit angle by adding or subtracting a random angular distance.
         - Ensures the angle stays within a 360° range by wrapping around if necessary.
         - Converts the angle to radians and computes the orbit offset.
         - Adds a random vertical offset within the positive and negative of verticalRange and adds it to the orbit offset
         - Adds to the orbit offset the static height offset from the height of the player

---

   #### `ChooseArcTarget()`
   ```csharp
   private void ChooseArcTarget()
   {
      // Generate a random horizontal angle within the arc for non-orbit mode
      float randomAngle = Random.Range(-arcWidth / 2, arcWidth / 2); // Random angle within the arc
      float verticalOffset = Random.Range(-verticalRange, verticalRange); // Random vertical offset within the range

      Debug.Log("Vertical Offset: " + verticalOffset + ", randomAngle: " + randomAngle);

      // Ignore the y rotation of the forward vector, so the drone doesnt fly out of the building
      Vector3 flatUserForward = new Vector3(user.forward.x, 0f, user.forward.z);

      // Calculate the direction based on the random angle
      Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * flatUserForward;

      // Set the target position with vertical offset at the desired distance
      randomTargetPosition = (user.position + new Vector3(0, droneHeightOffset + verticalOffset, 0)) + (direction.normalized * distanceFromPlayer);
   }
   ```

   - Explanation:
     - Computes a random target position within an arc in front of the user.
       - Steps:
         - Generates a random angle within the arcWidth range and a random vertical offset.
         - Adjusts the forward direction of the user to ignore the y component (to ensure a flat arc).
         - Calculates the direction vector by rotating the user's forward vector by the random angle.
         - Combines the direction vector and vertical offset with the user's position to calculate the new target.

---

   #### `HandleShooting()`
   ```csharp
   private void HandleShooting()
   {
      if (gracePeriodOver)
      {
         if (shootTimer <= 0f)
         {
               ShootProjectile();
               shootTimer = Random.Range(minShootInterval, maxShootInterval); // Reset the shoot timer
         }
      }
   }
   ```
   - Explanation:
     - Shooting Logic:
       - Monitors the grace period before the drone begins shooting.
       - Fires a projectile by calling ShootProjectile() when the shoot timer expires.
       - Resets the shooting timer using random intervals.

---

   #### `ShootProjectile()`
   ```csharp
   private void ShootProjectile()
   {
      if (projectilePrefab == null || projectileSpawnPoint == null)
      {
         Debug.LogError("ProjectilePrefab or ProjectileSpawnPoint is not assigned!");
         return;
      }

      // Create the projectile
      GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
      projectile.SetActive(false);

      // Calculate velocity towards the target
      Vector3 directionToTarget = (user.transform.position - projectileSpawnPoint.position).normalized;

      // Randomize the Y component of the direction
      float randomHeightAdjustment = Random.Range(minLaserYDirectionOffset, maxLaserYDirectionOffset);
      directionToTarget.y += randomHeightAdjustment;

      // Normalize the modified direction and scale by speed
      Vector3 velocity = directionToTarget.normalized * Random.Range(minProjectileSpeed, maxProjectileSpeed);

      projectile.GetComponent<Projectile>().ShootProjectile(this.gameObject, user.gameObject, velocity);

      PlayRandomLaserSound();
   }
   ```
   - Explanation:
     - Projectile Creation:
       - Instantiates a projectile at the given spawn point transform
       - Creates the direction from the projectile spawnpoint to the target
       - Adjusts the projectile's vertical trajectory for unpredictability.
       - Normalizes the direction and creates the final randomized velocity towards the user
       - Shoots the projectile and plays a random laser sound effect.

---

   #### `AdjustEnginePitch() and PlayRandomLaserSound()`
   ```csharp
   private void PlayRandomLaserSound()
   {
        laserShotAudioSource.PlayOneShot(laserShotSFX[Random.Range(0, laserShotSFX.Count)]);
   }

   private void AdjustEnginePitch()
   {
      if (engineAudioSource == null)
         return;

      // Get the current velocity magnitude
      float velocityMagnitude = rb.velocity.magnitude;

      // Map velocity to pitch range
      float pitch = Mathf.Lerp(pitchMin, pitchMax, (velocityMagnitude / moveForce) * Time.timeScale);
      engineAudioSource.pitch = pitch;
   }

   ```
   - Explanation:
     - Plays a random sound from the laserShotAudioSource variable
     - Dynamic Audio Adjustment:
       - Calculates the drone's speed and adjusts the engine pitch accordingly.
       - Provides audio feedback to reflect the drone's motion state dynamically.

---

   #### `OnDrawGizmos()`
   ```csharp
   private void OnDrawGizmos()
   {
      if (randomTargetPosition != null)
      {
         // Draw a sphere at the target position
         Gizmos.color = Color.red;

         // Draw a small sphere at the target position
         Gizmos.DrawSphere(randomTargetPosition, 0.01f); 

         // Draw a line from the drone to the target
         Gizmos.color = Color.blue;
         Gizmos.DrawLine(transform.position, randomTargetPosition);
      }

      if (user == null) return;

      Gizmos.color = Color.red;
      Vector3 flatUserForward = new Vector3(user.forward.x, 0f, user.forward.z);
      Gizmos.DrawLine(user.transform.position, user.transform.position + flatUserForward);
   }
   ```
   - Explanation:
     - Used for debugging in the Unity Editor to visualize key elements such as the target position and drone's line of sight.
       - Steps:
         - Draws a red sphere at the randomTargetPosition and a blue line connecting the drone to the target.
         - Draws a red line representing the user's forward direction.

If everything was implemented and assigned correctly, you should have a working drone that has two flight modes, 
shoots lasers at different speeds and intervals and modulates its flying sound based on its velocity!

The final **TrainingDrone** script and all of its assignments should look like this:

<img src="https://github.com/user-attachments/assets/fcbb8cc4-80d7-4550-a8ae-0b13f732931a" width="300">
<img src="https://github.com/user-attachments/assets/06c4e007-9a27-499d-a792-0b53af2137f2" width="450">

### Creating Object Position Lock-On Script
We need to create a small utility script that will be necessary for improved drone targeting. Create and attach a new script called
**ObjectPositionLockOn** onto the **OVRCameraController's** *PlayerTarget* object. 

Inside it we will code the following simple script:

   ```csharp
using UnityEngine;

public class ObjectPositionLockOn : MonoBehaviour
{
    public Transform objectToLockOn;
    public bool lockOnX = false;
    public bool lockOnY = false;
    public bool lockOnZ = false;

    public bool lockRotationX = false;
    public bool lockRotationY = false;
    public bool lockRotationZ = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (lockOnX)
            currentPosition.x = objectToLockOn.position.x;

        if (lockOnY)
            currentPosition.y = objectToLockOn.position.y;

        if (lockOnZ)
            currentPosition.z = objectToLockOn.position.z;

        transform.position = currentPosition;

        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (lockRotationX)
            currentRotation.x = objectToLockOn.rotation.eulerAngles.x;

        if (lockRotationY)
            currentRotation.y = objectToLockOn.rotation.eulerAngles.y;

        if (lockRotationZ)
            currentRotation.z = objectToLockOn.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

 ```

The Update() method continuously adjusts the object's position and rotation based on the specified locking options.

This is a very handy and generic script useful to dynamically and easily restrict an objects movement based the movement of a different one!

- For the VR scene: 
  - We need to set the **Lock On Y** checkbox to **True** and assign the **CenterEyeAnchor** to the **Object To Lock On** field of the script.
  - This will ensure that the object on the X- and Z-axis doesnt move around, with the exception of the Y-axis to which it alters its height
   based on the user's headset height.

It should look like this:

<img src="https://github.com/user-attachments/assets/c569a72c-6d1c-4dde-85d8-37f3342158e9" width="500">

## Creating Miscellenous Objects
 ### Space Station Environment
  The packages **Sci-Fi Styled Modular Pack** has an amazing prebuilt space station on a dessert planet.
  This or a different environment of your liking can be used for the VR scene for your lightsaber training.

  Visual example of the dessert planet scene:

<img src="https://github.com/user-attachments/assets/aed38165-ea42-4344-bfdd-2be16f257548" width="800">

  #### Console
   A simple console mesh object is necessary, where we will display the **Drone Activation Status**, **Number of Parried Lasers Projectiles** and
   the **Number of Lifes Left**. The mesh used in the VR and AR scene was a *Console* mesh object, from the **Sci-Fi Styled Modular Pack** package.
   
   You can position and scale it up or down however you like!

   **UI**
   **Buttons**

  The console house four different buttons:
   - A red one, starting a round of lightsaber training with the drone being in orbit mode
   - A yellow one, that starts a game with the drone being in arc mode
   - An orange one, that starts a game with the drone being in orbit mode and more difficult settings applied to it
   - A white one, that depending on the current environment, switches from VR to the AR scene or vice versa.

   1. As a pushable and pokeable button with the VR controllers, the prefab **BigRedButton** from the **Meta XR Interaction SDK Essentials**
      Sample Assets was used.
   2. Position, rotate and scale them on the console so that they can be well interacted with.
   3. Create a new material called **CustomButtonColor** and replace the material from the **Button** found under **Button > Visuals > ButtonVisual > Button**
   4. For every **Button**: Change the field **Normal Color** to the color of your choice (red, yellow, orange, white). 
      The buttons will initially appear white because of the material itself but its collor will be overwritten during runtime.
   5. For every **Button** object that has the **InteractableUnityEventWrapper** attached to it:
      - Add events for the **When Select()** entry.
      - Example:
        - The red button has two event entries attached, namely **GameManager.StartGame()** and **TrainingDrone.ShouldOrbit()** and set to **True**
        - The yellow button has two event entries attached, namely **GameManager.StartGame()** and **TrainingDrone.ShouldOrbit()** set to **False**
        - The orange button has two event entries attached, namely **GameManager.StartGame()** and **TrainingDrone.SetToChaoticDroneMode()** 
        - The white button has one event entry attached, namely **SceneSwitcher.LoadScene** with the name of the respective opposite scene as its parameter

   **SceneSwitcher** is a very straightforward script that has only one function defined inside it:
   ```csharp
   using UnityEngine;
   using UnityEngine.SceneManagement;

   public class SceneSwitcher : MonoBehaviour
   {
      public void LoadScene(string sceneName)
      {
         if (sceneName != "")
               SceneManager.LoadScene(sceneName);
         else
               Debug.LogWarning("No scene name present for the SceneSwitcher!");
      }
   }
   ```
   
   **Console Moving Up and Down or Disappear**

   During gameplay, the console could yield as an obstacle for the **Laser Projectiles**. A solution would be to create a script
   and subscribe to the **GameManager's** **OnGameStart** and **OnGameEnd** events and temporarily, en- or disable the console object during gameplay.

   A fancier way would be to move the console up and down lineary.

   Make sure to try out different solutions however it fits you best!
 
   The final **Console** object should look similiar like this:

<img src="https://github.com/user-attachments/assets/31bc53d1-2e1c-4db4-bd7c-cf1782894f40" width="300">

## Unity XR Scene Setup
Create a new scene and name it **Lightsaber_Training_AR**. This is where the lightsaber training in AR is done!

Copy the following objects that are already setup from the VR scene:

 1. **GameManager**
 2. **GameMusic**
 3. **ConsoleRootIndoors**
 4. **OVRPlayerController**
 5. **LightsaberHilt**

Since moving in AR with an *Analog Stick* is for most people very disorienting, because of the positional shifts of VR
objects in the scene, we need to alter the way **OVRPlayerController** is built.

First create a new original prefab of the **OVRPlayerController** child object **OVRCameraRig**, as it has everything already setup.

Delete the **OVRPlayerController**, since stationary movement, using the **OVRCameraRig** alone, is necessary!

### Adjusting the PlayerTarget Object
Because the **OVRPlayerController** was deleted, we currently do not have a collider present, which is necessary for triggering **OnLifeLost** events during gameplay.

Attach a simple *Capsule Collider* to the **PlayerTarget** object, and keep it the same settings you used for the VR scene.
Additionally, to trigger an **OnLifeLost** event, when a **Laser Projectile** hits the user, we need to also attach our **PlayerHitEvent** script to it as well! 

#### Reconfiguring the ObjectPositionLockOn script

We need to reconfigure the **ObjectPositionLockOn** script that is attached onto the **OVRCameraController's** *PlayerTarget* object again.

This time set the following: 
   - Set the **Lock On X**, **Lock On Y**, **Lock On Z**, **Lock Rotation Y** checkboxes to **True**
   - If necessary, reassign the **CenterEyeAnchor** again 
     to the **Object To Lock On** field of the script

Because we do not control the camera with the **Analog Stick** anymore, the only movement that the camera is receiving is by physically moving in space with it.
This is the reason a direct lock-on of the **PlayerTarget's** position to the **CenterEyeAnchor** is necessary,  as without it, the **Drone** would shoot towards 
a position where the user would not be located at, which would ruin the games core mechanic. 

Now when the user is moving, the **PlayerTarget** object, that houses the **ObjectPositionLockOn**, **Capsule Collider** and **PlayerHitEvent** scripts move with
the headset itself, resulting in accurate laser dodges and hit events for when a user got git with a **Laser Projectile**.

### Adding Light Reflections To The AR Scene
To enhance the gameplay in AR, we will add the ability to cast light from the **Lightsaber**, **Laser Projectiles** and **Drone** onto the digitally scanned room 
surfaces generated by the **Meta Quest 3** itself.

To do this, we need to create and instance three objects into the scene:
   - EffectMeshGlobalMesh: A simple object, that has the **EffectMesh** script from the **Meta MR Utility Kit** attached to it! (used for all rooms)
   - EffectMesh: Another simple object, that has the **EffectMesh** script from the **Meta MR Utility Kit** attached to it! (used for specific rooms)
   - MRUK: A prefab from the **Meta MR Utility Kit**, that has an **MRUK** script attached to it

For this game, we can have both, the **EffectMeshGlobalMesh** and **EffectMesh** use the same script settings which are the following:

  - **Spawn On Start**: Set to **All Rooms**.
  - **Mesh Material**: Needs the **TransparentSceneAnchor** material attached to it
  - **Colliders**: Set to **False**
  - **Cut Holes**: Set to **Nothing**
  - **Cast Shadows**: Set to **False**
  - **Hide Mesh**: Set to **False**
  - **Texture Coordinate Modes**: Everything set to **METRIC**
  - **Labels**: **Everything**

For the **MRUK** object, everything should be already setup automatically, but be sure to set **EnableWorldLock** to **False**

If everything has been setup correctly, you should have a working AR scene with dynamic light casting onto the current room!

## Audio and Assets Links
These are the links to some of the audio and assets used for sound effects, music and textures for the bullet decals:
   - https://www.pngegg.com/en/png-dkhrn
   - https://www.pngegg.com/en/png-dkkoa
   - https://www.soundsnap.com/laser_noise_03
   - https://pixabay.com/sound-effects/epic-game-music-by-kris-klavenes-3-mins-49771/
   - https://pixabay.com/sound-effects/space-rumble-29970/
   - https://pixabay.com/sound-effects/drone-flying-67483/

The rest was used from the respective packages, that we imported in the beginning of this cookbook!

The packages with audio and environmental assets were: 
   - **Laser Weapons Sound Pack** (Laser Sounds)
   - **PopupAsylum** (Drone Model)
   - **Sci-Fi Styled Modular Pack** (VR Room)
   - **Sherbbs Particle Collection** (Sparks Particles)
   - **VolumetricLines** (Laser Projectile and Lightsaber)

# Overall Results
With everything implemented and assigned correctly you should have a VR and AR scene which is where you can perform your lightsaber training respectively and switch between them by the push of a button on the digital console!


https://github.com/user-attachments/assets/abed177e-e63f-4dbd-beac-cbf7c6cedd5b

https://github.com/user-attachments/assets/742a0e1d-a8fe-4cbb-bb53-aae97a3aea62

