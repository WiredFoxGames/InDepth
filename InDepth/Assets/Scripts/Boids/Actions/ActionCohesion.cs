using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidGroup/Action/Cohesion")]
public class ActionCohesion : BoidAction
{
    Vector3 currentVelocity;
    [Range(0.001f, 1f)]
    public float boidSmoothing = 0.5f;

    public override Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group)
    {
        // If context is empty, method will not make any changes to the boid
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        // Makes a vector with all of the possition of nearby boids and then divides it by the number of
        // boids in the contest, getting this way the middle position of them all.
        Vector3 cohesionVec = Vector3.zero;
        foreach (Transform pos in context)
        {
            cohesionVec += pos.position;
        }
        cohesionVec /= context.Count;
        cohesionVec -= boid.transform.position;
        cohesionVec = Vector3.SmoothDamp(boid.transform.right, cohesionVec, ref currentVelocity, boidSmoothing);
        return cohesionVec;
    }
}
