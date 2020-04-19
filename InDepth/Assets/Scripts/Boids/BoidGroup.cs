using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGroup : MonoBehaviour
{
    // Object variables
    public BoidUser boidObject;
    List<BoidUser> boidGroup = new List<BoidUser>();
    public BoidAction boidAction;

    // Movement Variables
    [Range(1f, 100f)]
    public float accelSpeed = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    // Grop properties
    [Range(5, 400)]
    public int groupSize = 20;
    const float groupDensity = 0.2f;
    [Range(1f, 10f)]
    public float nearObjRadius = 2f;
    [Range(0f, 1f)]
    public float separationRadius = 0.5f;

    // Simplified operations
    float squaredMaxSpeed;
    float squaredNearObjRadius;
    float squaredSeparationRadius;
    public float SquaredSeparationRadius => squaredSeparationRadius;


    // Start is called before the first frame update
    void Start()
    {
        // Squaring is a complex operation, by manually doing the operations ourselves we can improve performance
        squaredMaxSpeed = maxSpeed * maxSpeed;
        squaredNearObjRadius = nearObjRadius * nearObjRadius;
        squaredSeparationRadius = squaredNearObjRadius * squaredNearObjRadius;

        for (int i = 0; i < groupSize; i++)
        {
            Vector3 randomGroupSize = Random.insideUnitSphere * groupSize * groupDensity;
            Quaternion randomRotation = Quaternion.Euler(Vector3.right * Random.Range(0f, 360f));
            BoidUser boid = Instantiate(boidObject, randomGroupSize, randomRotation, transform);
            boidGroup.Add(boid);

        }
        Debug.Log(boidGroup.Count + " boids generated");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BoidUser boid in boidGroup)
        {
            List<Transform> context = GetNearbyObjects(boid);

            // Near objects radius tester.
            boid.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.cyan, Color.red, context.Count / 5f);

            // Decides which acction it should take
            Vector3 movementVec = boidAction.CalculateMovement(boid, context, this);
            movementVec *= accelSpeed;
            if (movementVec.sqrMagnitude > squaredMaxSpeed)
            {
                movementVec = movementVec.normalized * maxSpeed;
            }
            boid.MoveBoid(movementVec);
        }
    }

    /// <summary>
    /// Returns objects in an area close to the currently updating boid.
    /// </summary>
    /// <param name="boid"></param>
    /// <returns></returns>
    List<Transform> GetNearbyObjects(BoidUser boid)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(boid.transform.position, nearObjRadius);

        foreach (Collider c in contextColliders)
        {
            // If the collider is not our own boid, adds the position of the colider to the list.
            if (c != boid.Collision)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
