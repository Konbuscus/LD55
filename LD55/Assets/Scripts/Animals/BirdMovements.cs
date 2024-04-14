using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovements : AnimalMovements
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        targetMoves.Add(new MovePoint(new Vector3(2, 0.5f, 6), MoveMode.Fly, 0f, 1f));
        targetMoves.Add(new MovePoint(Vector3.zero, MoveMode.PickUp, 0f, 1f));
        targetMoves.Add(new MovePoint(new Vector3(0, 0, 1), MoveMode.Fly, 0.5f));
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
