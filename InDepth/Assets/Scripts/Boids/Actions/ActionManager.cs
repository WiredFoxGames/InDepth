using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidGroup/Action/Manager")]
public class ActionManager : BoidAction
{
    public BoidAction[] actionList;
    public float[] weightList;

    public override Vector3 CalculateMovement(BoidUser boid, List<Transform> context, BoidGroup group)
    {
        //
        if (weightList.Length != actionList.Length)
        {
            return Vector3.zero;
        }


        Vector3 movement = Vector3.zero;
        for (int i = 0; i < actionList.Length; i++)
        {
            Vector3 moveToward = actionList[i].CalculateMovement(boid, context, group) * weightList[i];

            if (moveToward != Vector3.zero)
            {
                if (moveToward.sqrMagnitude > weightList[i] * weightList[i])
                {
                    moveToward.Normalize();
                    moveToward *= weightList[i];
                }
                movement += moveToward;
            }
        }
        return movement;
    }
}
