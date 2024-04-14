using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveMode
{
    Walk,
    Jump,
    Fly,
    PickUp,
    UnPickUp
}

[Serializable]
public class MovePoint
{
    public Vector3 targetPosition;
    public GameObject targetGameobject;
    public MoveMode moveMode;
    public float timeBefore = 0;
    public float timeAfter = 0;
    public float stopDistance = 0.25f;

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

    public MovePoint(Vector3 targetPosition, MoveMode moveMode, float timeBefore, float timeAfter, float stopDistance)
    {
        this.targetPosition = targetPosition;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
        this.timeAfter = timeAfter;
        this.stopDistance = stopDistance;
    }

    public MovePoint(GameObject targetGameobject, MoveMode moveMode)
    {
        this.targetGameobject = targetGameobject;
        this.moveMode = moveMode;
    }

    public MovePoint(GameObject targetGameobject, MoveMode moveMode, float timeBefore)
    {
        this.targetGameobject = targetGameobject;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
    }

    public MovePoint(GameObject targetGameobject, MoveMode moveMode, float timeBefore, float timeAfter)
    {
        this.targetGameobject = targetGameobject;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
        this.timeAfter = timeAfter;
    }

    public MovePoint(GameObject targetGameobject, MoveMode moveMode, float timeBefore, float timeAfter, float stopDistance)
    {
        this.targetGameobject = targetGameobject;
        this.moveMode = moveMode;
        this.timeBefore = timeBefore;
        this.timeAfter = timeAfter;
        this.stopDistance = stopDistance;
    }
}
