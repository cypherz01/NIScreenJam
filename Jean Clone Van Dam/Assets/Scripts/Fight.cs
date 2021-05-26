using System.Collections;
using UnityEngine;

[System.Serializable]
public class Move : ICommand
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    private float distance;
    private Transform objectToMove;

    public Move(Transform objectToMove, Vector3 direction, float distance = 1f)
    {
        this.direction = direction;
        this.objectToMove = objectToMove;
        this.distance = distance;
    }

    public void Execute()
    {
        objectToMove.position += direction * distance;
    }

}

public class Jump : ICommand
{
    [SerializeField]
    private Rigidbody2D objectToMove;
    private float jumpForce;
    private bool groundCheck;

    public Jump(Rigidbody2D objectToMove, float jumpForce, bool groundCheck)
    {
        this.objectToMove = objectToMove;
        this.jumpForce = jumpForce;
        this.groundCheck = groundCheck;
    }

    public void Execute()
    {
        objectToMove.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        WaitForlanding();
    }

    private IEnumerator WaitForlanding()
    {
        int count = 0;
        do
        {
            count++;
            yield return null;
        } while (count < 20);
    }

}

public class Punch : ICommand
{
    [SerializeField]
    private Animator animator;
    private GameObject player;
    private LayerMask enemyLayers;
    private Transform attackPoint;

    public Punch(GameObject player, Animator animator, Transform attackPoint, LayerMask enemyLayers)
    {
        this.animator = animator;
        this.player = player;
        this.attackPoint = attackPoint;
        this.enemyLayers = enemyLayers;
    }

    public void Execute()
    {
        Collider2D hitEnemy = Physics2D.OverlapArea(attackPoint.position, attackPoint.position + new Vector3(0.5f, 0, 0), enemyLayers);

        if (!(hitEnemy == null))
        {
            if (hitEnemy.GetComponent<Animator>().GetBool("isblocking"))
            {
                hitEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
                hitEnemy.GetComponent<Animator>().SetBool("isblocking", false);
            }
            else
            {
                hitEnemy.GetComponent<SpriteRenderer>().color = Color.red;
                hitEnemy.GetComponent<Health>().loseHealth();
            }

        }
        animator.SetTrigger("Attack");
    }
}

public class Block : ICommand
{
    [SerializeField]
    private Animator animator;

    public Block(Animator animator)
    {
        this.animator = animator;
    }

    public void Execute()
    {
        Debug.Log("blocking");
        animator.SetBool("isblocking", true);
    }
}

public class Unblock : ICommand
{
    [SerializeField]
    private Animator animator;

    public Unblock(Animator animator)
    {
        this.animator = animator;
    }

    public void Execute()
    {
        animator.SetBool("isblocking", false);
    }

}



public class Reset : ICommand
{
    [SerializeField]
    private Transform objectToMove;
    private Transform startPoint;
    private Vector3 temp;

    public Reset(Transform objectToMove, Transform startPoint)
    {
        this.objectToMove = objectToMove;
        this.startPoint = startPoint;
        this.temp = objectToMove.position;
    }

    public void Execute()
    {
        temp = startPoint.position;
        objectToMove.position = startPoint.position;
    }
}
