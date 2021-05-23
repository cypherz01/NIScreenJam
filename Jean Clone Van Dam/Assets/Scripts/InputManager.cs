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
    public Transform player;
    public Transform opponentChar;
    Transform playerStart;
    Transform oppStart;
    public Animator playerAnim;
    public Rigidbody2D playerRb;
    public Invoker opponent;
    [SerializeField]
    float jumpForce = 10f;
    public GroundCheck groundCheck;
    [SerializeField]
    private Invoker invoker;
    [SerializeField]

    public List<ICommand> commands = new List<ICommand>();

    private float time = 0;


    private void Start()
    {
        oppStart = opponentChar;
        playerStart = player;
    }


    void Update()
    {

        if (Input.GetKeyDown(jump) && groundCheck.isGrounded)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            groundCheck.isGrounded = false;
            
            // SendMoveCommand(invoker.transform, Vector3.up, 1f);
            SendOppCommand(opponent.transform, Vector3.up, 1f);
            time = 0;
        }

        if (Input.GetKeyDown(block))
        {
            SendMoveCommand(invoker.transform, Vector3.back, 1f);
            SendOppCommand(opponent.transform, Vector3.back, 1f);
            time = 0;
        }
        if (Input.GetKeyDown(left))
         {
            SendMoveCommand(invoker.transform, Vector3.left, 1f);
            SendOppCommand(opponent.transform, Vector3.right, 1f);
            time = 0;
        }
        if (Input.GetKeyDown(right))
        {
            SendMoveCommand(invoker.transform, Vector3.right, 1f);
            SendOppCommand(opponent.transform, Vector3.left, 1f);
            time = 0;
        }

        if (Input.GetKeyDown(lightAttack))
        {
            playerAnim.SetTrigger("Attack");
            // SendFightCommand(PlayerArm.GetComponent<Transform>(), Vector3.right, 1f);
            // SendOppCommand(opponent.transform, Vector3.left, 1f);
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
            resetCommand(invoker.transform, playerStart);
            resetCommand(opponent.transform, oppStart);
            replayCommand();
        }

        time = time + Time.deltaTime;
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void SendMoveCommand(Transform objectToMove, Vector3 direction, float distance)
    {
        ICommand movement = new Move(objectToMove, direction, distance);
        Invoker.AddCommand(movement);
    }

    private void SendFightCommand(Transform objectToMove, Vector3 direction, float distance)
    {
        ICommand movement = new Punch(objectToMove, direction, distance);
        Invoker.AddCommand(movement);
    }


    private void SendOppCommand(Transform oppToMove, Vector3 direction, float distance)
    {
        ICommand oppMovement = new Move(oppToMove, direction, distance);
        commands.Add(oppMovement);
    }

    private void replayCommand()
    {
        foreach(ICommand command in commands)
        {
            Invoker.AddOppCommand(command);
 
        }
        commands.Clear();

    }

    private void resetCommand(Transform objectToMove, Transform startPoint)
    {
        ICommand movement = new Reset(objectToMove, startPoint);

        Invoker.AddOppCommand(movement);
    }


}
