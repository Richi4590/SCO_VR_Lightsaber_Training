using System.Collections.Generic;
using UnityEngine;

public class TrainingDrone : MonoBehaviour
{
    public Transform user; // The user (e.g., VR player or camera)
    public GameObject projectilePrefab; // The projectile to be fired
    public Transform projectileSpawnPoint; // Where the projectile spawns
    public List<AudioClip> laserShotSFX;

    [Header("Main Settings")]
    public bool enabled = true;
    public bool stayStationary = false;
    public bool orbitMode = true; // If true, the drone orbits; if false, moves within 180� arc
    public float startFirstShootingAfterNSeconds = 2f;

    [Header("Shooting Settings")]
    public float minShootInterval = 0.1f; // Time between shots
    public float maxShootInterval = 2f; // Time between shots
    public float minProjectileSpeed = 5f; // Speed of the projectile
    public float maxProjectileSpeed = 12f; // Speed of the projectile
    public float minLaserYDirectionOffset = -0.2f;
    public float maxLaserYDirectionOffset = 0.1f;

    [Header("Stationary Settings")]
    public float minStationaryTime = 1f; // Time to stay stationary after reaching a target
    public float maxStationaryTime = 3f; // Time to stay stationary after reaching a target

    [Header("Flying Settings")]
    public float minDistanceToTriggerTarget = 0.2f;
    public float moveForce = 10f; // Speed of the drone's movement
    public float distanceFromPlayer = 5f; // Distance to maintain from the user
    public float droneHeightOffset = 1;
    public float verticalRange = 2f; // Maximum up/down movement range
    public float directionSwitchInterval = 2.5f; // Time interval between direction switches

    [Header("Orbit Settings")]
    public float minOrbitAngleDistance = 5f; // Minimum angular distance between orbit targets
    public float maxOrbitAngleDistance = 45f; // Maximum angular distance between orbit targets

    [Header("Arc Settings")]
    public float arcWidth = 180f; // Width of the movement arc when not orbiting

    [Header("Audio Settings")]
    public AudioSource laserShotAudioSource;
    public AudioSource engineAudioSource; // AudioSource for the drone's engine sound
    public float pitchMin = 0.8f; // Minimum pitch
    public float pitchMax = 2f; // Maximum pitch

    private float shootTimer;
    private float stationaryTimer = 0f;
    private bool gracePeriodOver = false;
    private float initialShootingTimer = 0f;
    private Vector3 randomTargetPosition;
    private Rigidbody rb;
    private float currentOrbitAngle = 180f; // Keeps track of the current orbit angle
    private bool reachedTarget = false;

    private float timeSinceLastSwitch = 0f; // Keeps track of time since the last direction switch
    private bool clockwise = true; // Keeps track of the current orbit direction

    private float initialMoveForce;
    private float initialMinShootInterval;
    private float initialMaxShootInterval;
    private float initialPitch;

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

    private void FixedUpdate()
    {
        if (enabled)
        {
            if (!stayStationary)
                MoveDrone();
        }

    }

    private void Update()
    {
        if (enabled)
        {

            // Update time since last direction switch
            shootTimer -= Time.deltaTime;
            timeSinceLastSwitch += Time.deltaTime;

            if (reachedTarget)
                stationaryTimer -= Time.deltaTime;

            //Debug.Log(stationaryTimer);

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

    public void EnableDrone()
    {
        InitDrone();
        this.enabled = true;
    }

    public void ShouldOrbit(bool shouldOrbit)
    {
        this.orbitMode = shouldOrbit;
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
            ApplyForceTowards(randomTargetPosition); // Then apply force towards the target after rotating
        }
    }

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

    private void RotateTowards(Vector3 targetPosition)
    {
        // Calculate direction to target
        Vector3 direction = targetPosition - transform.position;

        // Calculate the rotation step
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target position using slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * moveForce);
    }

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

        // Ensure the angle wraps around 360�
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

    /*
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
        Vector3 velocity = projectileSpawnPoint.forward * Random.Range(minProjectileSpeed, maxProjectileSpeed);
        
        float randomUpDownVelocityOffset = Random.Range(-1.0f, 0f);
        velocity.y = randomUpDownVelocityOffset;

        projectile.GetComponent<Projectile>().ShootProjectile(this.gameObject, user.gameObject, velocity);

        PlayRandomLaserSound();
    }
    */

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

    private void OnDrawGizmos()
    {
        if (randomTargetPosition != null)
        {
            // Draw a sphere at the target position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(randomTargetPosition, 0.01f); // Draw a small sphere at the target position

            // Draw a line from the drone to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, randomTargetPosition);
        }

        if (user == null) return;

        Gizmos.color = Color.red;
        Vector3 flatUserForward = new Vector3(user.forward.x, 0f, user.forward.z);
        Gizmos.DrawLine(user.transform.position, user.transform.position + flatUserForward);
    }

}