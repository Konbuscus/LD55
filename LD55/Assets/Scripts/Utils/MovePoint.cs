using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveMode
{
    Walk,
    Jump,
    Fly
}

public class MovePoint
{
    public Vector3 targetPosition;
    public MoveMode moveMode;
    public float timeBefore = 0;
    public float timeAfter = 0;

    public MovePoint(Vector3 targetPosition, MoveMode moveMode)
    {
        this.targetPosition = targetPosition;
        this.moveMode = moveMode;
    }

    public MovePoint(Vector3 targetPosition, MoveMode moveMode, float timeBefore)
    {
        this.targetPosition = targetPosition;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
    }

    public MovePoint(Vector3 targetPosition, MoveMode moveMode, float timeBefore, float timeAfter)
    {
        this.targetPosition = targetPosition;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
        this.timeAfter = timeAfter;
    }
}