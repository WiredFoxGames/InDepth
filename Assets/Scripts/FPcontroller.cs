using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPcontroller : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private UI_Inventory uiInvetory;
    
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 move = Vector3.zero;
    public Camera cam;
    public float mHorizontal = 3.0f;
    public float mVertical = 2.0F;
    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    private float h_mouse;
    private float v_mouse;
    private Inventory inventory;
    
    private void Awake()
    {
        inventory = new Inventory();
        uiInvetory.SetInventory(inventory);
        ItemWorld.SpawnItemWorld(new Vector3(1, 0, 14), new Item {itemType = Item.ItemType.Crystal, amount = 1});
        ItemWorld.SpawnItemWorld(new Vector3(-4, 0, 14), new Item {itemType = Item.ItemType.Rock, amount = 1});
        ItemWorld.SpawnItemWorld(new Vector3(-4, 0, 9), new Item {itemType = Item.ItemType.Meat, amount = 1});
    }
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
        
    }

    // Update is called once per frame
    void Update()
    {

        h_mouse = mHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);
        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0,0);
        transform.Rotate(0, h_mouse, 0);
        
        if (characterController.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = transform.TransformDirection(move) * runSpeed;
            }
            else
            {
                move = transform.TransformDirection(move) * walkSpeed;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
        }

        move.y -= gravity * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);
    }
}
