using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldedges : MonoBehaviour
{
    Vector3 newPos;
    Vector3 newCamPos;

    public PanelManager PanelManager;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transform entering Portal: " + collision.transform.name);
        if (collision.tag == "Player" && gameObject.tag == "Right")
        {
            if (PanelManager.i % 2 == 0) PanelManager.i += 1;
            PanelManager.i += 1;
            PanelManager.y += 1;
            newPos = PanelManager.goRight("pos").transform.position;
            newCamPos = PanelManager.goRight("cam").transform.position;
            collision.GetComponentInParent<Transform>().transform.position = newPos;
            GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
        }
        else if (collision.tag == "Player" && gameObject.tag == "Left")
        {
            if (PanelManager.i % 2 == 1) PanelManager.i -= 1;
            PanelManager.i -= 1;
            PanelManager.y -= 1;
            newPos = PanelManager.goLeft("pos").transform.position;
            newCamPos = PanelManager.goLeft("cam").transform.position;
            collision.GetComponentInParent<Transform>().transform.position = newPos;
            GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;

        }

    }
}
