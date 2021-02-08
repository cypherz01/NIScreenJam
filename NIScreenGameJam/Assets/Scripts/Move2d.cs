using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2d : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool isGrounded = false;
    public bool isleft = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        float inputHoriz = Input.GetAxis("Horizontal");
        float inputVert = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputHoriz, 0f, 0f);


        Direction(inputHoriz);
        transform.position += movement * Time.deltaTime * moveSpeed;




    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump")) && (isGrounded))
        {
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
