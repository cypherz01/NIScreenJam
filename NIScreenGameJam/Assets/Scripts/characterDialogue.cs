using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterDialogue : MonoBehaviour
{

    public GameObject PressEnter;
    public Dialogue dialogue;
    bool atboard;

    void Start()
    {
        PressEnter.SetActive(false);
        atboard = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && atboard)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            atboard = true;
            PressEnter.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            atboard = false;
            PressEnter.SetActive(false);
        }
    }
}
