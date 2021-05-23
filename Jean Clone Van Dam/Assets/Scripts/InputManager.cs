using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public String up;
    [SerializeField]
    public String down;
    [SerializeField]
    public String left;
    [SerializeField]
    public String right;
    [SerializeField]
    public String punch;
    [SerializeField]
    public String kick;
    [SerializeField]
    public String undo;
    [SerializeField]
    public String replay;
    public Transform startPoint;
    public Transform oppStartPoint;

    public GameObject PlayerArm;

    public Invoker opponent;


    [SerializeField]
    private Invoker invoker;
    [SerializeField]

    public List<ICommand> commands = new List<ICommand>();

    private float time = 0;


    void Update()
    {

        if (Input.GetKeyDown(up))
        {
            SendMoveCommand(invoker.transform, Vector3.up, 1f);
            SendOppCommand(opponent.transform, Vector3.up, 1f);
            time = 0;
        }

        if (Input.GetKeyDown(down))
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

        if (Input.GetKeyDown(punch))
        {
            SendFightCommand(PlayerArm.GetComponent<Transform>(), Vector3.right, 1f);
            SendOppCommand(opponent.transform, Vector3.left, 1f);
            time = 0;
        }
        if (Input.GetKeyDown(kick))
        {
            SendMoveCommand(invoker.transform, Vector3.right, 1f);
            SendOppCommand(opponent.transform, Vector3.left, 1f);
            time = 0;
        }



        if (Input.GetKeyDown(replay))
        {
            resetCommand(invoker.transform, startPoint);
            resetCommand(opponent.transform, oppStartPoint);
            replayCommand();
        }

        time = time + Time.deltaTime;
        


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
