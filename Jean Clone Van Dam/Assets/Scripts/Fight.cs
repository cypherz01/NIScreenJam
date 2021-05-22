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

public class Fight : ICommand
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    private float distance;
    private GameObject objectToMove;
    private GameObject objectToHit;

    public Fight(GameObject objectToMove, GameObject objectToHit, Vector3 direction, float distance = 1f)
    {
        this.direction = direction;
        this.objectToMove = objectToMove;
        this.objectToHit = objectToMove;
        this.distance = distance;
    }

    public void Execute()
    {
        if (objectToHit != null)
        {
            objectToHit.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }
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
