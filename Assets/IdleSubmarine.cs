using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSubmarine : MonoBehaviour
{
    public Transform propeller;
    public float propellerSpeedFac = 700;
    public Material propSpinMat;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        propeller.Rotate(Vector3.forward * Time.deltaTime * propellerSpeedFac * 50, Space.Self);
        propSpinMat.color =
            new Color(propSpinMat.color.r, propSpinMat.color.g, propSpinMat.color.b, 50 * .3f);
    }
}
