using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace DialogueSystemSpace{

    public class DialogueSystem : MonoBehaviour
    {
        public bool finished {get; private set;}
        protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, Color textColor, AudioClip sound){
            finished = false;
            textHolder.color = textColor;
            for (int i = 0; i < input.Length; i++){
                textHolder.text += input[i];
                DialogueSound.instance.PlaySound(sound);
                yield return new WaitForSeconds(0.05f);
            }

            finished = true;
        }
    }
}
