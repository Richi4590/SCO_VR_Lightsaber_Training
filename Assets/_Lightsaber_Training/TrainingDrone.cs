using UnityEngine;

public class TrainingDrone : MonoBehaviour
{
    public Transform user; // The user (e.g., VR player or camera)
    public GameObject projectilePrefab; // The projectile to be fired
    public Transform projectileSpawnPoint; // Where the projectile spawns
    public float orbitDistance = 5f; // Distance to maintain from the user
    public float moveSpeed = 2f; // Speed of the drone's movement
    public float minShootInterval = 0.1f; // Time between shots
    public float maxShootInterval = 2f; // Time between shots
    public float projectileSpeed = 10f; // Speed of the projectile
    public bool orbitMode = true; // If true, the drone orbits; if false, moves within 180° arc
    public float arcWidth = 180f; // Width of the movement arc when not orbiting
    public float verticalRange = 2f; // Maximum up/down movement range
    public float maxStationaryTime = 2f; // Time to stay stationary after reaching a target
    public bool stayStationary = false;

    private float shootTimer;
    private float stationaryTimer = 0f;
    private Vector3 randomTargetPosition;

    private void Start()
    {
        shootTimer = Random.Range(minShootInterval, maxShootInterval); // Initialize the shoot timer
        ChooseRandomTarget(); // Pick the initial random target
    }

    private void Update()
    {
        if (user == null)
        {
            Debug.LogError("User is not assigned to the drone!");
            return;
        }

        if (!stayStationary)
            MoveDrone();
        
        HandleShooting();
    }

    private void MoveDrone()
    {
        if (orbitMode)
        {
            // Orbit around the user
            OrbitAroundUser();
        }
        else
        {
            // Move randomly within a 180° arc with vertical offset
            MoveRandomlyWithVerticalOffset();
        }
    }

    private void OrbitAroundUser()
    {
        float orbitAngle = Time.time * moveSpeed; // Angle changes over time
        float radians = Mathf.Deg2Rad * orbitAngle;
        Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * orbitDistance;
        transform.position = user.position + offset;
        transform.LookAt(user);
    }

    private void MoveRandomlyWithVerticalOffset()
    {
        if (stationaryTimer > 0f)
        {
            // Stay stationary for the specified time
            stationaryTimer -= Time.deltaTime;
            return;
        }

        // Move toward the random target
        transform.position = Vector3.MoveTowards(transform.position, randomTargetPosition, moveSpeed * Time.deltaTime);

        // If the drone reaches the target, stay stationary and pick a new target
        if (Vector3.Distance(transform.position, randomTargetPosition) < 0.1f)
        {
            stationaryTimer = Random.Range(0.1f, maxStationaryTime); // Start the stationary timer
            ChooseRandomTarget(); // Pick a new random position
        }

        // Always face the user
        transform.LookAt(user);
    }

    private void ChooseRandomTarget()
    {
        // Generate a random horizontal angle within the arc
        float randomAngle = Random.Range(-arcWidth / 2, arcWidth / 2);

        // Generate a random vertical offset
        float verticalOffset = Random.Range(-verticalRange, verticalRange);

        // Calculate the direction based on the random angle
        Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * user.forward;

        // Set the target position with vertical offset at the desired distance
        randomTargetPosition = user.position + direction.normalized * orbitDistance;
        randomTargetPosition.y += verticalOffset; // Apply vertical offset
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = Random.Range(minShootInterval, maxShootInterval); // Reset the shoot timer
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError("ProjectilePrefab or ProjectileSpawnPoint is not assigned!");
            return;
        }

        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        Vector3 velocity = projectileSpawnPoint.forward * projectileSpeed;
        projectile.GetComponent<Projectile>().ShootProjectile(this.gameObject, user.gameObject, velocity);

        //Play Sound...

    }
}