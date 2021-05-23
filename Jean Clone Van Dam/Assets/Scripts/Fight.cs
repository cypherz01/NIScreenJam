﻿using UnityEngine;

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

public class Punch : ICommand
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    private float distance;
    private Transform fist;

    public Punch(Transform fist, Vector3 direction, float distance = 1f)
    {
        this.direction = direction;
        this.fist = fist;
        this.distance = distance;
    }

    public void Execute()
    {
        fist.position += direction * distance;
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
