using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCheck : MonoBehaviour
{
    public GameObject Player;
    public bool isblocked;
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isblocked = true;
            Debug.Log("wall");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isblocked = false;
        Debug.Log("left wall");
    }
}
