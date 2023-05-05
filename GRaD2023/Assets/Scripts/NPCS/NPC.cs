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
    public float movementSpeed = 1f;

    public GameObject[] pathToWalk;
    public int currentWaypoint;
    public bool hasReachedDestination;

    private bool walkingLeft;
    private bool walkingRight;
    private bool walkingUp;
    private bool walkingDown;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTitle.text = npcName;
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStationary)
        {
            NPCPathMovement();
        }
        if (Input.GetKeyDown(KeyCode.E) && playerIsWithinRange)
        {
            playerIsInteracting = true;
            if (dialoguePanel.activeInHierarchy)
            {
                ResetText();
            } else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(TypeDialogue());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NPCPathMovement()
    {
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Vertical", transform.position.y);
        if (transform.position.x > 0.1)
        {
            walkingRight = true;
            animator.SetBool("WalkingRight", true); 
        } else if (transform.position.x < 0.1)
        {
            walkingLeft = true;
            animator.SetBool("WalkingLeft", true);
        }

        if (transform.position.y > 0.1)
        {
            walkingUp = true;
            animator.SetBool("WalkingUp", true);
        } else if (transform.position.y < -0.94)
        {
            walkingDown = true;
            animator.SetBool("WalkingDown", true);
        }
        float step = movementSpeed * Time.deltaTime;
        if (!hasReachedDestination && !playerIsInteracting)
        {
            transform.position = Vector2.MoveTowards(transform.position, pathToWalk[currentWaypoint].transform.position, step);
            // If we have reached the destination
            
            if (Vector2.Distance(transform.position, pathToWalk[currentWaypoint].transform.position) < 0.1f)
            {
                hasReachedDestination = true;
                ResetMovementVariables();
                currentWaypoint++;
                if (currentWaypoint >= 4)
                {
                    currentWaypoint = 0;
                }
                transform.position = Vector2.MoveTowards(transform.position, pathToWalk[currentWaypoint].transform.position, step);
                hasReachedDestination = false;
            }
        }
    }

    public void ResetMovementVariables()
    {
        walkingLeft = false;
        walkingRight = false;
        walkingUp = false;
        walkingDown = false;
        animator.SetBool("WalkingLeft", false);
        animator.SetBool("WalkingRight", false);
        animator.SetBool("WalkingUp", false);
        animator.SetBool("WalkingDown", false);
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
