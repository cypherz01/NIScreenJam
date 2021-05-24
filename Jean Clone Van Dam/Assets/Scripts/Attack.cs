using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().loseHealth();
        gameObject.SetActive(false);
        Debug.Log(collision.gameObject.GetComponent<Health>().currentHealth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gameObject.transform.position, new Vector2 (0.5f,0.5f));
    }
}
