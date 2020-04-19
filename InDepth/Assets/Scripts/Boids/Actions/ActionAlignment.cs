using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidGroup/Action/Alignment")]
public class ActionAlignment : BoidAction
{
    Vector3 currentVelocity;
    [Range(0.001f, 1f)]
    public float boidSmoothing = 0.5f;

    public override Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group)
    {
        // If context is empty, method will return a vector that's the same as current facing direction
        if (context.Count == 0)
        {
            return boid.transform.right;
        }

        // Makes a vector with all of the heading positions of nearby boids and then divides it by the number of
        // boids in the contest, getting this way the average forward direction of them all.
        Vector3 alignmentVec = Vector3.zero;
        foreach (Transform pos in context)
        {
            alignmentVec += pos.transform.right;
        }
        alignmentVec /= context.Count;
        alignmentVec = Vector3.SmoothDamp(boid.transform.right, alignmentVec, ref currentVelocity, boidSmoothing);

        return alignmentVec;
    }
}
