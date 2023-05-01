using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UserInterfaceHandler : MonoBehaviour
{
    public GameObject shopPanel;

    public TextMeshProUGUI physicalHealth;
    private string physicalHealthString = "Physical Health: ";

    public TextMeshProUGUI mentalHealth;
    private string mentalHealthString = "Mental Health: ";

    public TextMeshProUGUI hungerAndThirst;
    private string hungerAndThirstString = "Hunger & Thirst: ";

    public TextMeshProUGUI money;
    private string moneyString = "Money: ";

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        physicalHealth.text = physicalHealthString + player.GetPlayerPhysicalHealth().ToString();
        mentalHealth.text = mentalHealthString + player.GetPlayerMentalHealth().ToString();
        hungerAndThirst.text = hungerAndThirstString + player.GetPlayerHungerAndThirst().ToString();
        money.text = moneyString + player.GetPlayerMoney().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowShopInterface(bool state)
    {
        shopPanel.SetActive(state);
    }

    public void UpdatePlayerUI()
    {
        physicalHealth.text = physicalHealthString + player.GetPlayerPhysicalHealth().ToString();
        mentalHealth.text = mentalHealthString + player.GetPlayerMentalHealth().ToString();
        hungerAndThirst.text = hungerAndThirstString + player.GetPlayerHungerAndThirst().ToString();
        money.text = moneyString + player.GetPlayerMoney().ToString();
    }
}
