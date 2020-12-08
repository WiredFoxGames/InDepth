using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPcontroller : MonoBehaviour, ICrafter
{
    private CharacterController characterController;

    [SerializeField] private UI_Inventory uiInvetory; // Call ui_inventory class
    public float walkSpeed = 6.0f; // Set walkspeed for the character
    public float runSpeed = 10.0f; // Set runspeed for the character
    public float jumpSpeed = 8.0f; // Set jumpseepd for the character
    public float gravity = 20.0f; // Set the gravity for the character
    private Vector3 move = Vector3.zero; // Vector to camera
    public Camera cam; // Camera as a gameobject
    public float mHorizontal = 3.0f; // Set the horizontal move of the mouse
    public float mVertical = 2.0F; // Set the vertical move of the mouse
    public float minRotation = -65.0f; // Set the minimal rotation of camera
    public float maxRotation = 60.0f; // Set the maximum rotation of camera
    private float h_mouse; // Horizontal mouse
    private float v_mouse; // Vertical mouse
    private Inventory inventory; // Call class inventory
    
    //Call everytime 
    private void Awake()
    {
        //Create the object inventory.    
        inventory = new Inventory();
        //Set the UI_Inventory
        uiInvetory.SetInventory(inventory);
        //Set the Inventory in the player
        uiInvetory.SetPlayer(this);
    }
    //Call at the start of the game.
    void Start()
    {
        //Get the component CharacterController from inspector
        characterController = GetComponent<CharacterController>();
    }

    //When player collide with an item of the world
    private void OnTriggerEnter(Collider other)
    {
        // Create the object with the component from inspector
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        // If object exist, then make a condition
        if (itemWorld != null)
        {
            //If the inventory its more than 27 then nothings happens.
            if (uiInvetory.isFull)
            {
                Debug.Log("Esta lleno");
            }
            //If the inventory its not full, then get the item and destroy the one in the ground
            else
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }

        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // All about camera moves
        h_mouse = mHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);
        
        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0,0);
        transform.Rotate(0, h_mouse, 0);
        //If the player its colliding with the floor
        if (characterController.isGrounded)
        {
            //Set the move with the camera
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //If the player pulse shift, increase the speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = transform.TransformDirection(move) * runSpeed;
            }
            //If not, then the speed its normal
            else
            {
                move = transform.TransformDirection(move) * walkSpeed;
            }
            //If player use Space, then make a jump
            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
        }
        
        //Set the gravity with deltatime to get a good smooth jump
        move.y -= gravity * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);
    }
    
    //Get the position of the player.
    public Vector3 GetPosition() {
        return transform.position;
    }

    public void CraftItem(Item.ItemType itemType)
    {
        Debug.Log("Crafteando item" + itemType);
    }
}
