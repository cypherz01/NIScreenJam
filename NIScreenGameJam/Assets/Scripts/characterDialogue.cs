using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterDialogue : MonoBehaviour
{

    public GameObject PressEnter;
    public Dialogue dialogue;

    bool atboard;

    // Start is called before the first frame update
    void Start()
    {
        PressEnter.SetActive(false);
        atboard = false;

    }

    // Update is called once per frame
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
