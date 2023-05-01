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
        if (gathering)
        {
            if (time >= gatheringTime)
            {
                money += 5;
                gatheringTime = 2f;
                gathering = false;
            } else
            {
                gatheringTime -= Time.deltaTime;
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrashCan"))
        {
            Debug.Log("Can look for trash");
            money += 10;
            userInterface.UpdatePlayerUI();
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
