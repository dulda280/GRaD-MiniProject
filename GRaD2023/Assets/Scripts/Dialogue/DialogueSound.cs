using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    public static DialogueSound instance{get; private set;}

    private AudioSource source;
    private void Awake(){
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound){
        source.PlayOneShot(sound);
    }
}
