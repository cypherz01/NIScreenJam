using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{

    static Queue<ICommand> commandBuffer;
    static Queue<ICommand> oppCommandBuffer;

    public List<float> actionTimes;
    public InputManager inputManager;
    private bool co_running = false;



    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
        oppCommandBuffer = new Queue<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }

    public static void AddOppCommand(ICommand command)
    {
        oppCommandBuffer.Enqueue(command);
    }

    private void FixedUpdate()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();
        }


        if (!co_running && oppCommandBuffer.Count > 0) StartCoroutine(placeholder1());

    }

    IEnumerator placeholder1()
    {
        co_running = true;
        yield return new WaitForSeconds(1f);

        do
        {
            if (!inputManager.gameState.ToString().Equals("STATE_PLAYING"))
            {
                oppCommandBuffer.Clear();
            }

            if ((oppCommandBuffer.Count > 0))
            {
                ICommand c = oppCommandBuffer.Dequeue();
                c.Execute();
                yield return new WaitForSeconds(1f);
            }


        } while (oppCommandBuffer.Count > 0);
        co_running = false;
        inputManager.canReplay = true;
    }
}
