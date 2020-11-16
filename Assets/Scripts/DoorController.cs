
using UnityEngine;
//This class controll the animation of doors.
public class DoorController : MonoBehaviour
{
    //Variables.
    private Animator _doorAnim;

    //When another object its collide with the trigger
    //Make the condition of the animation = true.
    private void OnTriggerEnter(Collider other)
    {
        //condition of the animation.
        _doorAnim.SetBool("character_nearby", true);
    }

    //When the object its non collide with the trigger
    //Make the condition of the animation = false.
    private void OnTriggerExit(Collider other)
    {
        //condition of the animation.
        _doorAnim.SetBool("character_nearby", false);
    }

    private void Start()
    {
        //Get the animator component.
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

}
