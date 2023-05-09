using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Video.VideoPlayer;

public class Player : MonoBehaviour
{
    // Player Variables
    float _physicalHealth = 10;
    float _mentalHealth = 10;
    private float _hungerAndThirst = 40;
    private int _money = 5;
    private float movementSpeed = 5f;
    public Rigidbody2D rigidBody;
    Vector2 movement;

    public Animator animator;

    // Script References
    public TimeScript timeScript;
    public EventHandler eventHandler;
    public Inventory inventory;
    public InventoryUI inventoryUI;
    public UserInterfaceHandler userInterface;

    // Motel Sleep System
    public int motelSleepPrice = 10;
    private bool canSleepAtMotel = false;

    // Job System
    private string jobDeliveryDriver = "Delivery Driver";
    private int jobDeliveryDriverSalary = 15;
    private float jobDeliveryDriverWorkHours = 4;
    private bool canWorkAtJobDelivery = false;

    private string jobOfficeWorker = "Office Worker";
    private int jobOfficeWorkerSalary = 20;
    private int jobOfficeWorkerWorkHours = 7;
    private bool canWorkAtJobOffice = false;

    private string jobCashier = "Cashier";
    private int jobCashierSalary = 18;
    private int jobCashierWorkHours = 6;
    private bool canWorkAtjobCashier = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerInputChecker();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void PlayerInputChecker()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            money += 10;
            userInterface.UpdatePlayerUI();
        }

        if (canSleepAtMotel)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SleepAtMotel();
                canSleepAtMotel = false;
                Debug.Log("");
            }
        }

        if (canWorkAtjobCashier)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WorkJob(jobCashier);
                canWorkAtjobCashier = false;
                Debug.Log("CASHIER");
            }
        }

        if (canWorkAtJobDelivery)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WorkJob(jobDeliveryDriver);
                canWorkAtJobDelivery = false;
                Debug.Log("DELIVERY");
            }
        }

        if (canWorkAtJobOffice)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WorkJob(jobOfficeWorker);
                canWorkAtJobOffice = false;
                Debug.Log("OFFICE");
            }
        }
    }

    private void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void SleepAtMotel()
    {
        if (money >= motelSleepPrice)
        {
            money -= motelSleepPrice;
            timeScript.ResetTime();
            StartCoroutine(eventHandler.ShowEvent($"I feel well rested", 1));
        } else
        {
            StartCoroutine(eventHandler.ShowEvent($"I can't afford to stay at this Motel, price is {motelSleepPrice}$ and you have {money}$", 4));
        }
    }

    private void WorkJob(string jobType)
    {
        if (jobType == jobDeliveryDriver)
        {
            var earnedMoney = (jobDeliveryDriverSalary * jobDeliveryDriverWorkHours);
            money += Mathf.FloorToInt(earnedMoney);
            StartCoroutine(eventHandler.ShowEvent($"I earned {earnedMoney}$ for a total of {jobDeliveryDriverWorkHours} hrs worked", 4));
            // Pass time by time script
            workTimeProgression(jobDeliveryDriverWorkHours, timeScript);
        }

        if (jobType == jobOfficeWorker)
        {
            var earnedMoney = (jobOfficeWorkerSalary * jobOfficeWorkerWorkHours);
            money += Mathf.FloorToInt(earnedMoney);
            StartCoroutine(eventHandler.ShowEvent($"I earned {earnedMoney}$ for a total of {jobOfficeWorkerWorkHours} hrs worked", 4));
            // Pass time by time script
            workTimeProgression(jobOfficeWorkerWorkHours, timeScript);
        }

        if (jobType == jobCashier)
        {
            var earnedMoney = (jobCashierSalary * jobCashierWorkHours);
            money += Mathf.FloorToInt(earnedMoney);
            StartCoroutine(eventHandler.ShowEvent($"I earned {earnedMoney}$ for a total of {jobCashierWorkHours} hrs worked", 4));
            // Pass time by time script
            workTimeProgression(jobCashierWorkHours, timeScript);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodShop"))
        {
            userInterface.ShowShopInterface("FoodShop", true);
        }

        if (collision.gameObject.CompareTag("Pharmacy"))
        {
            userInterface.ShowShopInterface("Pharmacy",true);
        }

        if (collision.gameObject.CompareTag("Motel"))
        {
            StartCoroutine(eventHandler.ShowEvent($"Press 'E' to sleep here for {motelSleepPrice}$", 3));
            canSleepAtMotel = true;
        }

        if (collision.gameObject.CompareTag("Supermarket"))
        {
            StartCoroutine(eventHandler.ShowEvent($"Press 'E' to work here for {jobCashierSalary}$ for a duration of {jobCashierWorkHours} Hrs", 4));
            canWorkAtjobCashier = true;
        }

        if (collision.gameObject.CompareTag("DeliveryJob"))
        {
            StartCoroutine(eventHandler.ShowEvent($"Press 'E' to work here for {jobDeliveryDriverSalary}$ for a duration of {jobDeliveryDriverWorkHours} Hrs", 4));
            canWorkAtJobDelivery = true;
        }

        if (collision.gameObject.CompareTag("Office"))
        {
            StartCoroutine(eventHandler.ShowEvent($"Press 'E' to work here for {jobOfficeWorkerSalary}$ for a duration of {jobOfficeWorkerWorkHours} Hrs", 4));
            canWorkAtJobOffice = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodShop"))
        {
            userInterface.ShowShopInterface("FoodShop",false);
        }

        if (collision.gameObject.CompareTag("Pharmacy"))
        {
            userInterface.ShowShopInterface("Pharmacy", false);
        }

        if (collision.gameObject.CompareTag("Motel"))
        {
            canSleepAtMotel = false;
        }

        if (collision.gameObject.CompareTag("Supermarket"))
        {
            canWorkAtjobCashier = false;
        }

        if (collision.gameObject.CompareTag("DeliveryJob"))
        {
            canWorkAtJobDelivery = false;
        }

        if (collision.gameObject.CompareTag("Office"))
        {
            canWorkAtJobOffice = false;
        }
    }

    private void workTimeProgression(float timeWorked, TimeScript timeObj){
        var tempTime = timeObj.timeVal;
        timeObj.timeVal = tempTime + (timeWorked*60f);
        timeObj.intensityMod = timeObj.intensityMod + (timeWorked/20f);
    }

    // Getters and Setters
    public float health { get { return _physicalHealth; } set { _physicalHealth = value; }}
    public float mental { get { return _mentalHealth; } set { _mentalHealth = value; }}
    public float hungerAndThirst { get { return _hungerAndThirst; } set { _hungerAndThirst = value; }}
    public int money { get { return _money; } set { _money = value; }}
}
