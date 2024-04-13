using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovements : AnimalMovements
{
    protected new void Start()
    {
        base.Start();
        targetMoves.Add(new MovePoint(new Vector3(3, 0, 6), MoveMode.Walk, 2f, 3f));
        targetMoves.Add(new MovePoint(new Vector3(2, 0.5f, 6), MoveMode.Jump));
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    private void JumpToward()
    {

    }
}
