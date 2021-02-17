using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move2d : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool isGrounded = false;
    public bool isleft;

    private Rigidbody2D rb;

    [HideInInspector]
    float mayJump;
    float jumpBufferTimer = 0f;
    float buffermax = 0.3f;
    float inputHoriz;
    bool isdash = false;

    wallCheck blocked;


    Vector3 movement;

    Transform startPos;

    AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        blocked = GameObject.Find("Player").GetComponentInChildren<wallCheck>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        startPos = GameObject.Find("startpos_0").GetComponent<Transform>();
        GameObject.Find("Player").GetComponent<Transform>().position = startPos.position;
        audiosrc = GameObject.Find("AudioManager_jump").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputHoriz = Input.GetAxis("Horizontal");
        movement = new Vector3(inputHoriz, 0f, 0f);
        Direction(inputHoriz);
        if (isGrounded) mayJump = 0.2f;
    }

    private void Update()
    {
        if ((isGrounded || mayJump > 0)&&(jumpBufferTimer < buffermax)){
            Jump();
            jumpBufferTimer = buffermax;
        }

        if (isdash && isGrounded)
        {
            Jump();
            isdash = false;
        }
        
        mayJump -= Time.deltaTime;
        jumpBufferTimer += Time.deltaTime;

        if (!blocked.isblocked) transform.position += movement * Time.deltaTime * moveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = 0;
        }

        if (Input.GetButtonUp("Jump") &&(rb.velocity.y >0))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
            
        }

        if (Input.GetButtonDown("Fire1") && !isGrounded && rb.velocity.y<-4)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *4);
            isdash = true;
        }
        
        if((rb.velocity.y < -4)&&(rb.velocity.y > -12))
        {
            GameObject.Find("Player").GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        isGrounded = false;
        audiosrc.Play();
    }

    void Direction(float input)
    {
        Vector2 newScale = transform.localScale;

        if ((input < 0) && (!isleft))   //going right to left
        {
            newScale.x *= -1f;
            transform.localScale = newScale;
            isleft = true;
        }
        else if ((input > 0) && (isleft)) //going left to right
        {
            newScale.x *= -1f;
            transform.localScale = newScale;
            isleft = false;
        }

    }

}
