using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI bigEventText;

    public GameObject eventBG;
    public GameObject bigEventBG;
    public IEnumerator ShowEvent(string eventMessage, float delay)
    {
        eventText.text = eventMessage;
        ShowElement(eventBG, true);
        yield return new WaitForSeconds(delay);
        ShowElement(eventBG, false);
    }

    public IEnumerator ShowBigEvent(string eventMessage, float delay){
        bigEventText.text = eventMessage;
        
        ShowBigElement(bigEventBG, true);
        yield return new WaitForSeconds(delay);
        
        ShowBigElement(bigEventBG, false);
    }

    /*
    public void ShowEvent(string eventMessage)
    {
        eventIsBeingShown = true;
        eventText.text = eventMessage;
        ShowElement(eventBG, true);
        
    }
    */

    public void ShowBigElement(GameObject gameObject, bool state){
        gameObject.SetActive(state);

    }

    public void ShowElement(GameObject gameObject, bool state)
    {
        gameObject.SetActive(state);
    }
    
}
