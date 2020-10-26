using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidGroup/Action/Separation")]
public class ActionSeparation : BoidAction
{
    public override Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group)
    {
        // If context is empty, method will not make any changes to the boid
        if (context.Count == 0)
        {
            return Vector3.zero;
        }


        // Makes a vector with all of the possition of nearby boids and then divides it by the number of
        // boids in the contest, getting this way the middle position of them all.
        Vector3 separationVec = Vector3.zero;
        int closeObjCount = 0;
        foreach (Transform pos in context)
        {
            if (Vector3.SqrMagnitude(pos.position - boid.transform.position) < group.SquaredSeparationRadius){
                closeObjCount++;
                separationVec += boid.transform.position - pos.position;
            }   
        }
        if (closeObjCount > 0)
        {
            separationVec /= closeObjCount;
        }
        return separationVec;
    }
}
