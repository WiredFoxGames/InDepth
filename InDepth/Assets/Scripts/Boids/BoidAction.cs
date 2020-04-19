using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidAction : ScriptableObject
{
    public abstract Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group);
}
