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

    public void Undo()
    {
        objectToMove.position -= direction * distance;
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

    public void Undo()
    {
    }

    private IEnumerator WaitForlanding()
    {
        int count = 0;
        do
        {
            count++;
            yield return null;
        } while (count <20);
    }

}

public class Punch : ICommand
{
    [SerializeField]
    private Animator animator;
    private GameObject player;
    private LayerMask enemyLayers;

    public Punch(GameObject player,Animator animator, LayerMask enemyLayers)
    {
        this.animator = animator;
        this.player = player;
        this.enemyLayers = enemyLayers;
    }

    public void Execute()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(player.transform.Find("attack point").position, new Vector2(1, 1), enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            enemy.GetComponent<Health>().loseHealth();
        }
        animator.SetTrigger("Attack");
    }

    public void Undo()
    {
   
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
        temp= startPoint.position;
        objectToMove.position = startPoint.position;
    }

    public void Undo()
    {
        objectToMove.position =temp;
    }

}
