using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boughtItemSound;
    public AudioClip consumeItemSound;
    public AudioClip jobPayoutSound;

    public void BoughtItemSFX()
    {
        audioSource.clip = boughtItemSound;
        audioSource.Play();
    }

    public void ConsumeItemSFX() 
    {
        audioSource.clip = consumeItemSound;
        audioSource.Play();
    }

    public void JobPayoutSFX()
    {
        audioSource.clip = jobPayoutSound;
        audioSource.Play();
    }
}
