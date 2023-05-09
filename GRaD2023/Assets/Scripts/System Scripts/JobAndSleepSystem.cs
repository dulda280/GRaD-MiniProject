using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobAndSleepSystem : MonoBehaviour
{

    public EventHandler eventHandler;

    public int motelSleepPrice = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Motel"))
        {
            StartCoroutine(eventHandler.ShowEvent($"Press 'E' to sleep here for {motelSleepPrice}$", 3));
        }
    }
}
