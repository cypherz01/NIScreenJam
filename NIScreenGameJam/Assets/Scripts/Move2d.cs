using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move2d : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    [HideInInspector]
    public float mayJump;
    public bool isGrounded = false;
    public bool isleft = false;
    public bool blocked;
    public Vector3 movement;
    
    // Start is called before the first frame update
    void Start()
    {
         blocked = GameObject.Find("Player").GetComponentInChildren<wallCheck>().isblocked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHoriz = Input.GetAxis("Horizontal");
        float inputVert = Input.GetAxis("Vertical");
        movement = new Vector3(inputHoriz, 0f, 0f);
        Direction(inputHoriz);
        if (isGrounded) mayJump = 0.2f;
    }

    private void Update()
    {
        Jump();
        mayJump -= Time.deltaTime;
        if (!blocked) transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump")) && ((isGrounded)||(mayJump >0)))
        {
            if ((mayJump > 0)&& (mayJump != 0.2f)) Debug.Log("cyote time.");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Direction(float input)
    {
        Vector3 newScale = transform.localScale;

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
