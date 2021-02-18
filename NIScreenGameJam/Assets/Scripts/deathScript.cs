using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathScript : MonoBehaviour
{
    GameObject respawn;
    GameObject cam;
    GameObject cam_respawn;
    GameObject Panelmanager;
    new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.Find("startpos_0");
        cam = GameObject.Find("MainCam");
        cam_respawn = GameObject.Find("CamPos_0");
        Panelmanager = GameObject.Find("position_manager");
        audio = GameObject.Find("AudioManager_Death").GetComponent<AudioSource>(); 
        
    }

    // Update is called once per fram

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f,0f);
            collision.GetComponent<Move2d>().canmove = false;
            collision.GetComponent<Rigidbody2D>().position = respawn.transform.position;
            cam.transform.position = cam_respawn.transform.position;
            Panelmanager.GetComponent<PanelManager>().i = 0;
            Panelmanager.GetComponent<PanelManager>().y = 0;
            audio.Play();
        }
    }
}
