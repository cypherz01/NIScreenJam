using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2dEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool isleft;
    public float direction = -1f;

    public float health;
    float counter;


    wallCheck blocked;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        blocked = GameObject.Find("Troll").GetComponentInChildren<wallCheck>();
        movement = new Vector2(direction, 0f);

        health = 3.0f;
        counter = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Direction(direction);

        if (counter > 10)
        {
            GameObject.Find("Troll").GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            counter = 0;
        }
        counter++;
    }

    private void Update()
    {
        if (!blocked.isblocked)
        {
            movement.x = direction * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else
        {
            direction *= -1;
            Direction(direction); ;
        }
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
