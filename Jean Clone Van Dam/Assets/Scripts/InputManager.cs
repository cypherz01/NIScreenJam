using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public String jump;
    [SerializeField]
    public String block;
    [SerializeField]
    public String left;
    [SerializeField]
    public String right;
    [SerializeField]
    public String lightAttack;
    [SerializeField]
    public String kick;
    [SerializeField]
    public String undo;
    [SerializeField]
    public String replay;


    public GameObject player;
    public GameObject opponent;

    public Transform playerStart;
    public Transform oppStart;

    public int moveCount;
    public int moveCountMax;
    private int count;
    private int oppcount;
    public int iteration;

    [SerializeField]
    public float jumpForce;
    public GroundCheck groundCheck;
    private Invoker Invoker;

    public LayerMask enemylayers;
    public LayerMask playerLayer;


    public List<ICommand> commands = new List<ICommand>();

    private float time = 0;

    private void Start()
    {
        iteration = 0;
        count = 0;
        oppcount = 0;
        player.transform.position = playerStart.position;
        opponent.transform.position = oppStart.position;

        Invoker = player.GetComponent<Invoker>();
        moveCount = moveCountMax;

        
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<SpriteRenderer>().color == Color.red && count == 5)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            count = 0;
        }

        if (player.GetComponent<SpriteRenderer>().color == Color.red && count < 5) count++;

        if (opponent.GetComponent<SpriteRenderer>().color == Color.red && oppcount == 5)
        {
            opponent.GetComponent<SpriteRenderer>().color = Color.white;
            oppcount = 0;
        }

        if (opponent.GetComponent<SpriteRenderer>().color == Color.red && oppcount < 5) oppcount++;

    }


    void Update()
    {



        if (Input.GetKeyDown(jump) && groundCheck.isGrounded && moveCount >0)
        {
            SendJumpCommand(player.GetComponent<Rigidbody2D>(), jumpForce, groundCheck);
            SendOppJumpCommand(opponent.GetComponent<Rigidbody2D>(), jumpForce, groundCheck);
            groundCheck.isGrounded = false;
            moveCount--;
            time = 0;
        }

        if (Input.GetKeyDown(block) && moveCount > 0)
        {
            SendMoveCommand(player.transform, Vector3.back, 1f);
            SendOppCommand(opponent.transform, Vector3.back, 1f);
            moveCount--;
            time = 0;
        }
        if (Input.GetKeyDown(left) && moveCount > 0)
         {
            SendMoveCommand(player.transform, Vector3.left, 1f);
            SendOppCommand(opponent.transform, Vector3.right, 1f);
            moveCount--;
            time = 0;
        }
        if (Input.GetKeyDown(right) && moveCount > 0)
        {
            SendMoveCommand(player.transform, Vector3.right, 1f);
            SendOppCommand(opponent.transform, Vector3.left, 1f);
            moveCount--;
            time = 0;
        }

        if (Input.GetKeyDown(lightAttack) && moveCount > 0)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(player.transform.Find("attack point").position, new Vector2(1, 1), enemylayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().loseHealth();
            }

            SendFightCommand(player,player.GetComponent<Animator>(),enemylayers) ;
            SendOppFightCommand(opponent,opponent.GetComponent<Animator>(),playerLayer);
            moveCount--;
            time = 0;
        }
        if (Input.GetKeyDown(kick))
        {
            // SendMoveCommand(invoker.transform, Vector3.right, 1f);
            // SendOppCommand(opponent.transform, Vector3.left, 1f);
            time = 0;
        }



        if (Input.GetKeyDown(replay))
        {
            resetCommand(player.transform, playerStart);
            resetCommand(opponent.transform, oppStart);
            iteration++;
            Debug.Log("iteration num :" + iteration);

            replayCommand();
            moveCountMax++;
            moveCount = moveCountMax;
        }

        time = time + Time.deltaTime;
        


    }

    private void SendMoveCommand(Transform objectToMove, Vector3 direction, float distance)
    {
        ICommand movement = new Move(objectToMove, direction, distance);
        Invoker.AddCommand(movement);
    }
    private void SendFightCommand(GameObject objectAttack, Animator selectedAnimator, LayerMask enemyLayers)
    {
        ICommand animation = new Punch(objectAttack, selectedAnimator, enemyLayers);
        Invoker.AddCommand(animation);
    }
    private void SendJumpCommand(Rigidbody2D objectToMove, float jumpForce, bool groundCheck)
    {
        ICommand movement = new Jump(objectToMove,jumpForce,groundCheck);
        Invoker.AddCommand(movement);
    }
    private void SendOppJumpCommand(Rigidbody2D objectToMove, float jumpForce, bool groundCheck)
    {
        ICommand movement = new Jump(objectToMove, jumpForce, groundCheck);
        commands.Add(movement);
    }
    private void SendOppCommand(Transform oppToMove, Vector3 direction, float distance)
    {
        ICommand oppMovement = new Move(oppToMove, direction, distance);
        commands.Add(oppMovement);
    }
    private void SendOppFightCommand(GameObject objectAttack, Animator selectedAnimator, LayerMask enemyLayers)
    {
        ICommand animation = new Punch(objectAttack, selectedAnimator, enemyLayers);
        commands.Add(animation);
    }
    private void replayCommand()
    {
        foreach(ICommand command in commands)
        {
            Invoker.AddOppCommand(command);
 
        }
        //commands.Clear();

    }
    private void resetCommand(Transform objectToMove, Transform startPoint)
    {
        ICommand movement = new Reset(objectToMove, startPoint);

        Invoker.AddCommand(movement);
    }


}
