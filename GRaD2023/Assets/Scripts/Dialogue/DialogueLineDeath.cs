using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace DialogueSystemSpace{
    public class DialogueLineDeath : DialogueSystem
    {
        private TextMeshProUGUI textHolder;
        [Header ("Text design")]
        [SerializeField] public string[] input;

        private int dialogueCounter = 0;

        private Color textColor = Color.cyan;

        [Header ("Sound")]
        [SerializeField] private AudioClip sound;

        [Header ("References")]
        public GameObject nextButton;
    
        private void Awake(){
            textHolder = GetComponent<TextMeshProUGUI>();
            textHolder.text = "";
        }

        private void Start(){
            StartCoroutine(WriteText(input[dialogueCounter], textHolder, textColor, sound));
        }

        private void Update(){
            if (textHolder.text == input[dialogueCounter])
            {
                nextButton.SetActive(true);
            }
            if(dialogueCounter == 1){
                
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
            if(dialogueCounter > 1){
                dialogueCounter = 1;
            }
        }

        public void NextDialogue(){
            dialogueCounter += 1;
            textHolder.text = "";
            StartCoroutine(WriteText(input[dialogueCounter], textHolder, textColor, sound));
            nextButton.SetActive(false);
            Debug.Log("called");
            

        }
    }
}