using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Player Variables
    private int physicalHealth = 100;
    private int mentalHealth = 100;
    private int hungerAndThirst = 100;
    private int money = 0;
    private float movementSpeed = 5f;
    public Rigidbody2D rigidBody;
    Vector2 movement;

    // Player Interaction Variables
    private float gatheringTime = 2f;
    private bool gathering = false;
    private bool canGatherTrash = false;
    private float time = 0.0f;

    // Script References
    public UserInterfaceHandler userInterface;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (canGatherTrash)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (time >= gatheringTime)
                {
                    money += 5;
                    gatheringTime = 2f;
                    gathering = false;
                    canGatherTrash = false;
                    userInterface.UpdatePlayerUI();
                }
                else
                {
                    gatheringTime -= Time.deltaTime;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void Gathering()
    {
        gathering = true;
    }

    private void ConsumeFood()
    {

    }


    public void SetPlayerAttributes(int variable, int newVariableValue, string type)
    {
        if (type == "set")
        {
            variable = newVariableValue;
        } else if (type == "add")
        {
            variable += newVariableValue;
        } else if (type == "sub")
        {
            variable -= newVariableValue;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Merchant"))
        {
            Debug.Log("Can trade with merchant");
            userInterface.ShowShopInterface(true);
        }

        if (collision.gameObject.CompareTag("TrashCan"))
        {
            canGatherTrash = true;
            Debug.Log("Can look for trash");
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
    public int GetPlayerPhysicalHealth() { return physicalHealth; }
    public int GetPlayerMentalHealth() { return mentalHealth; }
    public int GetPlayerHungerAndThirst() { return hungerAndThirst; }
    public int GetPlayerMoney() { return money; }
}
