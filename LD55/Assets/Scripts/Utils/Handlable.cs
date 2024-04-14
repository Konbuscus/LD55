using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Handlable : MonoBehaviour
{
    public Transform handlePoint;

    //public Dictionary<AnimalType, List<MovePoint>> waypoints;
    public List<MyObjectObject<AnimalType, List<MovePoint>>> waypoints;

    public List<MovePoint> GetWaypoints(AnimalType animalType)
    {
        return waypoints.FirstOrDefault(x => x.obj1 == animalType).obj2;
    }
}
