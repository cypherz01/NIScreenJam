using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoard : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject NoticeboardUI;

    private void Start()
    {
        NoticeboardUI.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
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
}
