using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UserInterfaceHandler : MonoBehaviour
{
    public GameObject shopPanel;

    public TextMeshProUGUI physicalHealth;
    public TextMeshProUGUI mentalHealth;
    public TextMeshProUGUI hungerAndThirst;
    public TextMeshProUGUI money;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        physicalHealth.text = player.health.ToString();
        mentalHealth.text = player.mental.ToString();
        hungerAndThirst.text = player.hungerAndThirst.ToString();
        money.text = player.money.ToString();
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
        physicalHealth.text = player.health.ToString();
        mentalHealth.text = player.mental.ToString();
        hungerAndThirst.text = player.hungerAndThirst.ToString();
        money.text = player.money.ToString();
    }
}
