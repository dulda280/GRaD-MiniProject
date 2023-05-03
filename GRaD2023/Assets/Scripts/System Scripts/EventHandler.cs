using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    public TextMeshProUGUI eventText;
    public GameObject eventBG;
    public IEnumerator ShowEvent(string eventMessage, float delay)
    {
        eventText.text = eventMessage;
        ShowElement(eventBG, true);
        yield return new WaitForSeconds(delay);
        ShowElement(eventBG, false);
    }

    /*
    public void ShowEvent(string eventMessage)
    {
        eventIsBeingShown = true;
        eventText.text = eventMessage;
        ShowElement(eventBG, true);
        
    }
    */

    public void ShowElement(GameObject gameObject, bool state)
    {
        gameObject.SetActive(state);
    }
    
}
