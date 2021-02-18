using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoard : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject NoticeboardUI;
    public Dialogue dialogue;

    bool atboard;

    private void Start()
    {
        NoticeboardUI.SetActive(false);
        atboard = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

            }
        }

        if(Input.GetKeyDown(KeyCode.X) && atboard)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    public void Resume()
    {
        NoticeboardUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        NoticeboardUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        atboard = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        atboard = false;
    }
}
