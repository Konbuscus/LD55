using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovements : AnimalMovements
{
    protected new void Start()
    {
        base.Start();
        targetPosition = new Vector3(1, 0, 5);
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
