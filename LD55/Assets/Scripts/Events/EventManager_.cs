using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
    None,
    RoomIn,
    RoomOut,
    RoomOut2,
    RoomOut3,
    BeforeFalseExit,
    BeforeFalseExit2,
    FalseExit,
    GoToElevator,
    ClosedDoors,
    GoToElevator2,
    SawElevator,
    GoToElevator3,
    InFrontOfElevator,
    InsideElevator,
    GoInElevator,
    LoadLevel2,
    RommIn2,
    SpawnedBird,
    //addenum
}

public class EventManager_ : MonoBehaviour
{
    public static EventManager_ instance;

    public List<MyObjectObject<EventName, DialogEvent>> dialogEvents;
    public List<MyObjectObject<EventName, AnimalMoveEvent>> animalMoveEvents;
    public List<MyObjectObject<EventName, SpawnAnimalEvent>> spawnAnimalEvent;

    private Dictionary<EventName, Event> events;

    public List<EventName> trigeredList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        events = new Dictionary<EventName, Event>();

        foreach(MyObjectObject<EventName, DialogEvent> obj in dialogEvents)
            events.Add(obj.obj1, obj.obj2);

        foreach(MyObjectObject<EventName, AnimalMoveEvent> obj in animalMoveEvents)
            events.Add(obj.obj1, obj.obj2);

        foreach(MyObjectObject<EventName, SpawnAnimalEvent> obj in spawnAnimalEvent)
            events.Add(obj.obj1, obj.obj2);
    }

    public void TrigerEvent(EventName eventName)
    {
        if (eventName != EventName.None)
        {
            trigeredList.Add(eventName);
            events[eventName].Do();
        }
    }
}
