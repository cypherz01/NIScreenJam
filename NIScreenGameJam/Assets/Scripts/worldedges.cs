using UnityEngine;

public class worldedges : MonoBehaviour
{
    public PanelManager PanelManager;

    Vector3 newPos;
    Vector3 newCamPos;
    GameObject player;
    int count;
    AudioSource audioboss;
    AudioSource audioback;

    private void Start()
    {
        count = 0;
        newCamPos = PanelManager.goRight("cam").transform.position;
        GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
        audioboss = GameObject.Find("BackgroundMusic_boss").GetComponent<AudioSource>();
        audioback = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if(count <= 50) count++;
        if (this.GetComponent<Collider2D>().enabled == false && count >= 50)
        {
            Debug.Log("Setting true");
            this.GetComponent<Collider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transform entering Portal: " + collision.transform.name);
        if (collision.tag == "Player" && gameObject.tag == "Right")
        {
            if (PanelManager.i % 2 == 0) PanelManager.i += 1;
            PanelManager.i += 1;
            PanelManager.y += 1;

            if (PanelManager.y == 5)
            {
                audioboss.Play();
                audioback.volume = 0;
            }

            if(PanelManager.y > 5)
            {
                newCamPos = PanelManager.goRight("cam").transform.position;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
            }
            else
            {
                newPos = PanelManager.goRight("pos").transform.position;
                newCamPos = PanelManager.goRight("cam").transform.position;
                collision.GetComponentInParent<Transform>().transform.position = newPos;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
            }
        }
        else if (collision.tag == "Player" && gameObject.tag == "Left")
        {
            if (PanelManager.i % 2 == 1) PanelManager.i -= 1;
            PanelManager.i -= 1;
            PanelManager.y -= 1;

            if (PanelManager.y > 5)
            {
                newCamPos = PanelManager.goLeft("cam").transform.position;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;

            }
            else
            {
                newPos = PanelManager.goLeft("pos").transform.position;
                newCamPos = PanelManager.goLeft("cam").transform.position;
                collision.GetComponentInParent<Transform>().transform.position = newPos;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
            }

            
                audioback.volume = 1;
                audioboss.Pause();

        }

        else if (collision.tag == "Player" && gameObject.tag == "Vert")
        {
            player = GameObject.Find("Player");

            if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                PanelManager.y += 1;
                newCamPos = PanelManager.goRight("cam").transform.position;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
                this.GetComponent<Collider2D>().enabled = false;
                this.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y-4f, this.transform.position.z);
                count = 0;
             
            }
            else
            {
                PanelManager.y -= 1;
                newCamPos = PanelManager.goLeft("cam").transform.position;
                GameObject.Find("MainCam").GetComponent<Transform>().transform.position = newCamPos;
                this.GetComponent<Collider2D>().enabled = false;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4f, this.transform.position.z);
                count = 0;
            }
        }
    }
}
