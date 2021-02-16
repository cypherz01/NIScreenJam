﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    GameObject Player;
    GameObject Enemy;
    AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        audiosrc = GameObject.Find("AudioManager_hit").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Ground") || (collision.collider.tag == "Enemy"))
        {
            Player.GetComponent<Move2d>().isGrounded = true;
            GameObject.Find("Player").GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        if(collision.collider.tag == "Enemy")
        {
            Enemy = collision.gameObject;
            Enemy.GetComponent<Move2dEnemy>().health--;
            Enemy.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            audiosrc.Play();


            if (Enemy.GetComponent<Move2dEnemy>().health == 0)
            {
                Destroy(Enemy);
            }

            Player.GetComponent<Move2d>().Jump();

 

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Ground")|| (collision.collider.tag == "Enemy" +
            ""))
        {
            Player.GetComponent<Move2d>().isGrounded = false;
        }
    }
}
