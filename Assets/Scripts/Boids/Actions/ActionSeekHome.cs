using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidGroup/Action/Seek home")]
public class ActionSeekHome : BoidAction
{
    public Vector3 center;
    public float radius = 50f;
    Vector3 currentVelocity;
    [Range(0.001f, 1f)]
    public float boidSmoothing = 0.5f;

    public override Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group)
    {
        center = group.transform.position;
        Vector3 moveHome = center - boid.transform.position;
        float t = moveHome.magnitude / radius;
       if (t < 0.9f)
        {
            return Vector3.zero;
        }

        moveHome = Vector3.SmoothDamp(boid.transform.forward, moveHome, ref currentVelocity, boidSmoothing);
        return moveHome;
    }
}
