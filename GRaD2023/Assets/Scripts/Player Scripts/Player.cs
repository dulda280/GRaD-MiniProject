using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Player Variables
    private int _physicalHealth = 10;
    private int _mentalHealth = 10;
    private int _hungerAndThirst = 10;
    private int _money = 15;
    private float movementSpeed = 5f;
    public Rigidbody2D rigidBody;
    Vector2 movement;

    public Animator animator;

    // Script References
    public Inventory inventory;
    public InventoryUI inventoryUI;
    public UserInterfaceHandler userInterface;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (inventory.CheckForConsumables())
            {
                hungerAndThirst += 5;
                userInterface.UpdatePlayerUI();
                inventoryUI.UpdateUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            money += 10;
            userInterface.UpdatePlayerUI();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void PlayerMovement()
    {
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Merchant"))
        {
            Debug.Log("Can trade with merchant");
            userInterface.ShowShopInterface(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Merchant"))
        {
            userInterface.ShowShopInterface(false);
        }
    }


    // Getters and Setters
    public int health { get { return _physicalHealth; } set { _physicalHealth = value; }}
    public int mental { get { return _mentalHealth; } set { _mentalHealth = value; }}
    public int hungerAndThirst { get { return _hungerAndThirst; } set { _hungerAndThirst = value; }}
    public int money { get { return _money; } set { _money = value; }}
}
