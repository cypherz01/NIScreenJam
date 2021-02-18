using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private GameObject triggeringNpc;
    private bool triggering;
    public DialogueTrigger Diag;

    void Start()
    {

    }

    void Update()
    {
        if (triggering)
        {
            Debug.Log("Within Range");
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Pressed the Interact Button");
                Diag.TriggerDialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            triggering = false;
            triggeringNpc = null;
        }
    }
}