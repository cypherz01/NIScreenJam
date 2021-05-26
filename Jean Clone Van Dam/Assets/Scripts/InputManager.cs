using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum GameState
    {
        STATE_WON,
        STATE_LOST,
        STATE_PLAYING
    }

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
    public int iteration;
    private int count;
    private int oppcount;
    private int attackCount;

    public float jumpForce;
    public bool canAttack;

    public GroundCheck groundCheck;

    private Invoker Invoker;

    public bool canReplay;
    private bool isflipped;

    public LayerMask enemylayers;
    public LayerMask playerLayer;

    public GameState gameState;

    public Text text;
    public Text text2;

    public List<ICommand> commands = new List<ICommand>();

    private void Start()
    {   
        gameState = GameState.STATE_PLAYING;
        canReplay = true;
        canAttack = true;
        isflipped = false;
        iteration = 0;
        count = 0;
        oppcount = 0;
        attackCount = 0;
        player.transform.position = playerStart.position;
        opponent.transform.position = oppStart.position;
        Invoker = player.GetComponent<Invoker>();
        moveCount = moveCountMax;
    }

    private void FixedUpdate()
    {
        text.text = "iteration:" + iteration;
        text2.text = "moves left:" + moveCount;

        if (player.GetComponent<SpriteRenderer>().color != Color.white && count == 10)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            count = 0;
        }
        if (player.GetComponent<SpriteRenderer>().color != Color.white && count < 10) count++;

        if (opponent.GetComponent<SpriteRenderer>().color != Color.white && oppcount == 10)
        {
            opponent.GetComponent<SpriteRenderer>().color = Color.white;
            oppcount = 0;
        }
        if (opponent.GetComponent<SpriteRenderer>().color != Color.white && oppcount < 10) oppcount++;


        if (attackCount == 40) canAttack = true;
        if (attackCount < 40) attackCount++;

    }


    void Update()
    {

        if (gameState.Equals(GameState.STATE_PLAYING))
        {
            if ((opponent.transform.position.x - player.transform.position.x) < 0 && (!isflipped))
            {

                player.transform.localRotation = Quaternion.Euler(0, 180, 0);
                opponent.transform.localRotation = Quaternion.Euler(0, 180, 0);
                isflipped = true;
            }

            if ((opponent.transform.position.x - player.transform.position.x) > 0 && (isflipped))
            {
                player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                opponent.transform.localRotation = Quaternion.Euler(0, 0, 0);
                isflipped = false;
            }

           /* if (Input.GetKeyDown(jump) && groundCheck.isGrounded && moveCount > 0)
            {
                SendJumpCommand(player.GetComponent<Rigidbody2D>(),jumpForce,groundCheck,true);
                SendJumpCommand(opponent.GetComponent<Rigidbody2D>(),jumpForce,groundCheck, false);
                groundCheck.isGrounded = false;
                moveCount--;
            }*/

            if (Input.GetKeyDown(block) && moveCount > 0)
            {
                SendBlockCommand(player.GetComponent<Animator>(),true);
                SendBlockCommand(opponent.GetComponent<Animator>(),false);
                moveCount--;
            }

            if (Input.GetKeyUp(block))
            {
                SendUnblockCommand(player.GetComponent<Animator>(),true);
                SendUnblockCommand(opponent.GetComponent<Animator>(),false);
            }

            if (Input.GetKeyDown(left) && moveCount > 0)
            {
                SendMoveCommand(player.transform, Vector3.left, 1f,true);
                SendMoveCommand(opponent.transform, Vector3.right, 1f,false);
                moveCount--;
            }

            if (Input.GetKeyDown(right) && moveCount > 0)
            {
                SendMoveCommand(player.transform, Vector3.right, 1f,true);
                SendMoveCommand(opponent.transform, Vector3.left, 1f,false);
                moveCount--;
            }

            if (Input.GetKeyDown(lightAttack) && moveCount > 0 && canAttack)
            {
                canAttack = false;
                attackCount = 0;
                SendFightCommand(player, player.GetComponent<Animator>(), player.transform.Find("attack point"), enemylayers,true);
                SendFightCommand(opponent, opponent.GetComponent<Animator>(), opponent.transform.Find("Attack point"), playerLayer,false);
                moveCount--;
            }

            if (Input.GetKeyDown(replay) && canReplay)
            {
                canReplay = false;
                replayCommand();
                iteration++;
                moveCountMax++;
                moveCount = moveCountMax;
            }

            if (player.GetComponent<Health>().currentHealth == 0) gameState = GameState.STATE_LOST;
            if (opponent.GetComponent<Health>().currentHealth == 0) gameState = GameState.STATE_WON;
        }

        if (gameState.Equals(GameState.STATE_LOST)) 
        {

            Debug.Log("PLAYER LOST");
        }
        if (gameState.Equals(GameState.STATE_WON))
        {
            Debug.Log("PLAYER WON");
        }
    }

    private void SendMoveCommand(Transform objectToMove, Vector3 direction, float distance,bool isplayer)
    {
        ICommand movement = new Move(objectToMove, direction, distance);
        if (isplayer) Invoker.AddCommand(movement);
        else commands.Add(movement);
    }
    private void SendFightCommand(GameObject objectAttack, Animator selectedAnimator,Transform attackPoint, LayerMask enemyLayers, bool isplayer)
    {
        ICommand animation = new Punch(objectAttack, selectedAnimator,attackPoint, enemyLayers);
        if (isplayer) Invoker.AddCommand(animation);
        else commands.Add(animation);
    }
    private void SendJumpCommand(Rigidbody2D objectToMove, float jumpForce, bool groundCheck,bool isplayer)
    {
        ICommand movement = new Jump(objectToMove,jumpForce,groundCheck);
        if(isplayer) Invoker.AddCommand(movement);
        else commands.Add(movement);
    }

    private void SendBlockCommand(Animator selectedAnimator, bool isplayer)
    {
        ICommand animation = new Block(selectedAnimator);
        if (isplayer) Invoker.AddCommand(animation);
        else commands.Add(animation);
    }

    private void SendUnblockCommand(Animator selectedAnimator, bool isplayer)
    {
        ICommand animation = new Unblock(selectedAnimator);
        if (isplayer) Invoker.AddCommand(animation);
        else commands.Add(animation);
    }

    private void replayCommand()
    {
        foreach(ICommand command in commands)
        {
            Invoker.AddOppCommand(command);
        }

    }
    private void resetCommand(Transform objectToMove, Transform startPoint)
    {
        ICommand movement = new Reset(objectToMove, startPoint);

        Invoker.AddCommand(movement);
    }


}
