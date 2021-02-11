using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldedges : MonoBehaviour
{
    Vector3 startPos;
    Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("startpos1").transform.position;
        camPos = GameObject.Find("CamPos").transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transform entering Portal: " + collision.transform.name);
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<Transform>().transform.position = startPos;
            GameObject.Find("MainCam").GetComponent<Transform>().transform.position = camPos;
        }

    }
}
