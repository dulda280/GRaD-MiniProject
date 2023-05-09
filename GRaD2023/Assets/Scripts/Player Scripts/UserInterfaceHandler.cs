using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.UI;

public class UserInterfaceHandler : MonoBehaviour
{
    public GameObject supermarketShopPanel;
    public GameObject pharmacyShopPanel;
    public GameObject drugShopPanel;
    public GameObject escapeMenu;
    public Button quitToMenuButton;
    public Button continueButton;

    private bool escapeMenuActive = false;

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
        UpdatePlayerUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escapeMenuActive)
            {
                escapeMenuActive = true;
                escapeMenu.SetActive(true);
                Time.timeScale = 0;
            } else
            {
                escapeMenuActive = false;
                escapeMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
       
    }

    public void ShowShopInterface(string shopType, bool state)
    {
        if (shopType == "FoodShop")
        {
            supermarketShopPanel.SetActive(state);
        }

        if (shopType == "Pharmacy")
        {
            pharmacyShopPanel.SetActive(state);
        }

        if (shopType == "Druggie")
        {
            drugShopPanel.SetActive(state);
        }
    }


    public void UpdatePlayerUI()
    {
        
        physicalHealth.text = Mathf.Ceil(player.health).ToString();
        mentalHealth.text = Mathf.Ceil(player.mental).ToString();
        hungerAndThirst.text = Mathf.Ceil(player.hungerAndThirst).ToString();
        money.text = player.money.ToString();
    }

    public void OnClickContinue()
    {
        escapeMenuActive = false;
        escapeMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuitToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
