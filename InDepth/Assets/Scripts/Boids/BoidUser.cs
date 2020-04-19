using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object must have a collider component, this line prevents nullpointer exceptions
[RequireComponent(typeof(SphereCollider))]
public class BoidUser : MonoBehaviour
{
    SphereCollider collision;
    public SphereCollider Collision { get { return collision; } }

    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<SphereCollider>();
    }

    public void MoveBoid(Vector3 movement)
    {
        transform.right = movement;
        transform.position += movement * Time.deltaTime;
        //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, movement, 10f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
