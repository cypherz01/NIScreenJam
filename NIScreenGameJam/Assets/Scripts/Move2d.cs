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

    wallCheck blocked;

    Vector3 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        blocked = GameObject.Find("Player").GetComponentInChildren<wallCheck>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        
        mayJump -= Time.deltaTime;
        jumpBufferTimer += Time.deltaTime;

        if (!blocked.isblocked) transform.position += movement * Time.deltaTime * moveSpeed;
        if (Input.GetButtonDown("Jump")) jumpBufferTimer = 0;
        if (Input.GetButtonUp("Jump") &&(rb.velocity.y >0))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        isGrounded = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        if (other.CompareTag("platform"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent = null;
    }

}
