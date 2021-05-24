using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private Animator animator;
    private bool alive;
    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        currentHealth = maxHealth;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentHealth <= 0 && alive ) death();
    }

    public void loseHealth()
    {
        currentHealth--;
        Debug.Log(currentHealth);
    }

    void death()
    {
        animator.SetTrigger("Death");
        alive = false;

    }
}
