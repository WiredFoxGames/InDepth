using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UniJSON;
using UnityEngine;
using UnityEngine.UI;


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
    private int rockAmount;
    public GameObject ui_popup;
    public TextMeshProUGUI notResource;
    private float popupTime = 1.2f;
    public float timer = 0.0f;


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
        LoadGame();
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
            ShowPopup();
            notResource.SetText("Progress saved!");
        }

        // All about camera moves
        h_mouse = mHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);

        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);
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

        //Timer for popup
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            HidePopup();
        }
    }

    //Get the position of the player.
    public Vector3 GetPosition()
    {
        return transform.position;
    }


    public void CraftItem(Item.ItemType itemType)
    {
        Item rockDos = new Item();
        Item crystalDos = new Item();
        Item ironDos = new Item();
        Item pearlDos = new Item();
        Item meatDos = new Item();
        int rockRestante = 0;
        int crystalRestante = 0;
        int ironRestante = 0;
        int pearlRestante = 0;
        int meatRestante = 0;

        bool canCraft = false;
        if (itemType == Item.ItemType.LaCanicadora)
        {
            foreach (Item rock in inventory.GetItemList())
            {
                if (rock.itemType == Item.ItemType.Rock && rock.amount >= 30)
                {
                    rockRestante = rock.amount - 30;
                    foreach (Item crystal in inventory.GetItemList())
                    {
                        if (crystal.itemType == Item.ItemType.Crystal && crystal.amount >= 10)
                        {
                            crystalRestante = crystal.amount - 10;
                            foreach (Item iron in inventory.GetItemList())
                            {
                                if (iron.itemType == Item.ItemType.Iron && iron.amount >= 15)
                                {
                                    ironRestante = iron.amount - 15;
                                    foreach (Item pearl in inventory.GetItemList())
                                    {
                                        if (pearl.itemType == Item.ItemType.Pearl && pearl.amount >= 1)
                                        {
                                            pearlRestante = pearl.amount - 1;
                                            canCraft = true;
                                            rockDos = rock;
                                            crystalDos = crystal;
                                            ironDos = iron;
                                            pearlDos = pearl;
                                            ShowPopup();
                                            notResource.SetText("You crafted: LaCanicadora");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (canCraft)
            {
                inventory.AddItem(new Item {itemType = Item.ItemType.LaCanicadora, amount = 1});
                inventory.RemoveItem(rockDos);
                inventory.RemoveItem(crystalDos);
                inventory.RemoveItem(ironDos);
                inventory.RemoveItem(pearlDos);
                inventory.AddItem(new Item {itemType = Item.ItemType.Rock, amount = rockRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Crystal, amount = crystalRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Iron, amount = ironRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Pearl, amount = pearlRestante});
            }
            else
            {
                ShowPopup();
                notResource.SetText("Not enough resources.");
            }
        }

        if (itemType == Item.ItemType.DoubleCannon)
        {
            foreach (Item rock in inventory.GetItemList())
            {
                if (rock.itemType == Item.ItemType.Rock && rock.amount >= 20)
                {
                    rockRestante = rock.amount - 20;
                    foreach (Item crystal in inventory.GetItemList())
                    {
                        if (crystal.itemType == Item.ItemType.Crystal && crystal.amount >= 5)
                        {
                            crystalRestante = crystal.amount - 5;
                            foreach (Item iron in inventory.GetItemList())
                            {
                                if (iron.itemType == Item.ItemType.Iron && iron.amount >= 10)
                                {
                                    ironRestante = iron.amount - 10;
                                    foreach (Item pearl in inventory.GetItemList())
                                    {
                                        if (pearl.itemType == Item.ItemType.Pearl && pearl.amount >= 0)
                                        {
                                            pearlRestante = pearl.amount - 0;
                                            canCraft = true;
                                            rockDos = rock;
                                            crystalDos = crystal;
                                            ironDos = iron;
                                            pearlDos = pearl;
                                            ShowPopup();
                                            notResource.SetText("You crafted: DoubleCannon");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (canCraft)
            {
                inventory.AddItem(new Item {itemType = Item.ItemType.DoubleCannon, amount = 1});
                inventory.RemoveItem(rockDos);
                inventory.RemoveItem(crystalDos);
                inventory.RemoveItem(ironDos);
                inventory.RemoveItem(pearlDos);
                inventory.AddItem(new Item {itemType = Item.ItemType.Rock, amount = rockRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Crystal, amount = crystalRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Iron, amount = ironRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Pearl, amount = pearlRestante});
            }
            else
            {
                ShowPopup();
                notResource.SetText("Not enough resources.");
            }
        }

        if (itemType == Item.ItemType.ElSuspenso)
        {
            foreach (Item rock in inventory.GetItemList())
            {
                if (rock.itemType == Item.ItemType.Rock && rock.amount >= 40)
                {
                    rockRestante = rock.amount - 40;
                    foreach (Item crystal in inventory.GetItemList())
                    {
                        if (crystal.itemType == Item.ItemType.Crystal && crystal.amount >= 30)
                        {
                            crystalRestante = crystal.amount - 30;
                            foreach (Item iron in inventory.GetItemList())
                            {
                                if (iron.itemType == Item.ItemType.Iron && iron.amount >= 30)
                                {
                                    ironRestante = iron.amount - 30;
                                    foreach (Item pearl in inventory.GetItemList())
                                    {
                                        if (pearl.itemType == Item.ItemType.Pearl && pearl.amount >= 0)
                                        {
                                            pearlRestante = pearl.amount - 0;
                                            canCraft = true;
                                            rockDos = rock;
                                            crystalDos = crystal;
                                            ironDos = iron;
                                            pearlDos = pearl;
                                            ShowPopup();
                                            notResource.SetText("You crafted: ElSuspenso");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (canCraft)
            {
                inventory.AddItem(new Item {itemType = Item.ItemType.ElSuspenso, amount = 1});
                inventory.RemoveItem(rockDos);
                inventory.RemoveItem(crystalDos);
                inventory.RemoveItem(ironDos);
                inventory.RemoveItem(pearlDos);
                inventory.AddItem(new Item {itemType = Item.ItemType.Rock, amount = rockRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Crystal, amount = crystalRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Iron, amount = ironRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Pearl, amount = pearlRestante});
            }
            else
            {
                ShowPopup();
                notResource.SetText("Not enough resources.");
            }
        }

        if (itemType == Item.ItemType.Health)
        {
            foreach (Item rock in inventory.GetItemList())
            {
                if (rock.itemType == Item.ItemType.Rock && rock.amount >= 0)
                {
                    rockRestante = rock.amount - 0;
                    foreach (Item crystal in inventory.GetItemList())
                    {
                        if (crystal.itemType == Item.ItemType.Crystal && crystal.amount >= 5)
                        {
                            crystalRestante = crystal.amount - 5;
                            foreach (Item iron in inventory.GetItemList())
                            {
                                if (iron.itemType == Item.ItemType.Iron && iron.amount >= 5)
                                {
                                    ironRestante = iron.amount - 5;
                                    foreach (Item meat in inventory.GetItemList())
                                    {
                                        if (meat.itemType == Item.ItemType.Meat && meat.amount >= 15)
                                        {
                                            meatRestante = meat.amount - 15;
                                            canCraft = true;
                                            rockDos = rock;
                                            crystalDos = crystal;
                                            ironDos = iron;
                                            meatDos = meat;
                                            ShowPopup();
                                            notResource.SetText("You crafted: Health");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (canCraft)
            {
                inventory.AddItem(new Item {itemType = Item.ItemType.Health, amount = 1});
                inventory.RemoveItem(rockDos);
                inventory.RemoveItem(crystalDos);
                inventory.RemoveItem(ironDos);
                inventory.RemoveItem(meatDos);
                inventory.AddItem(new Item {itemType = Item.ItemType.Rock, amount = rockRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Crystal, amount = crystalRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Iron, amount = ironRestante});
                inventory.AddItem(new Item {itemType = Item.ItemType.Meat, amount = meatRestante});
            }
            else
            {
                ShowPopup();
                notResource.SetText("Not enough resources.");
            }
        }
    }

    void ShowPopup()
    {
        ui_popup.SetActive(true);
        timer = popupTime;
    }

    void HidePopup()
    {
        ui_popup.SetActive(false);
    }

    public bool TryCraftItem(int spendRockAmount)
    {
        return false;
    }

    public void SaveGame()
    {
        string docupath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dict = new JsonFormatter();
        dict.BeginMap();
        foreach (Item item in inventory.GetItemList())
        {
            dict.Key(item.itemType.ToString());
            dict.Value(item.amount);
        }

        dict.EndMap();

        string json = dict.ToString();

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docupath, "indepth.save")))
        {
            outputFile.WriteLine(json);
        }
        
    }

    public void LoadGame()
    {
        string savegame;
        string docupath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (File.Exists(Path.Combine(docupath, "indepth.save")))
        {
            using (StreamReader inputFile = new StreamReader(Path.Combine(docupath, "indepth.save")))
            {
            
                savegame = inputFile.ReadLine();
                var values = JsonConvert.DeserializeObject<Dictionary<Item.ItemType, int>>(savegame);
                foreach (var item in values)
                {
                    inventory.AddItem(new Item {itemType = item.Key, amount = item.Value});
                }
            }
        }
        else
        {
            using (FileStream fs = File.Create(Path.Combine(docupath, "indepth.save")))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("{\"Rock\":0,\"Crystal\":0,\"Iron\":0,\"Pearl\":0,\"Meat\":0}");
                fs.Write(info,0,info.Length);
            }
            
        }
        
    }
}