using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPC : MonoBehaviour
{
    public string npcName;
    public bool isStationary;

    public Player player;

    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueTitle;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerIsWithinRange;
    public bool playerIsInteracting;

    public Animator animator;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTitle.text = npcName;
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!isStationary)
        {
            NPCPathMovement();
        }
        */
        if (Input.GetKeyDown(KeyCode.E) && playerIsWithinRange && !playerIsInteracting)
        {
            playerIsInteracting = true;
            if (dialoguePanel.activeInHierarchy && !playerIsInteracting)
            {
                ResetText();
            } else
            {
                dialogueTitle.text = npcName;
                dialoguePanel.SetActive(true);
                StartCoroutine(TypeDialogue());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void ResetText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator TypeDialogue()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1) 
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeDialogue());
        } else
        {
            player.OnDialogEnded();
            ResetText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsWithinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsWithinRange = false;
            playerIsInteracting = false;
            ResetText();
        }
    }
}
