using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Dialog,
    Movement
}

[Serializable]
public class Event
{
    public EventName nextEvent;

    public virtual void Do()
    {

    }
}

[Serializable]
public class DialogEvent : Event
{
    public string dialogName;

    public override void Do()
    {
        base.Do();

        DialogManager.instance.StartDialog(dialogName);

        if(nextEvent != EventName.None)
            EventManager_.instance.TrigerEvent(nextEvent);
    }
}

[Serializable]
public class AnimalMoveEvent : Event
{
    public AnimalMovements animal;
    public string animalName;
    public List<MovePoint> movePoints;

    public override void Do()
    {
        base.Do();

        if(animal == null)
        {
            animal = GameObject.Find("Event_" + animalName).GetComponent<AnimalMovements>();
        }
        animal.SetTargetMoves(movePoints);

        if (nextEvent != EventName.None)
            EventManager_.instance.TrigerEvent(nextEvent);
    }
}

[Serializable]
public class SpawnAnimalEvent : Event
{
    public GameObject animal;
    public Transform position;

    public override void Do()
    {
        base.Do();

        GameObject tmp = GameObject.Instantiate(animal);
        tmp.transform.position = position.position;
        tmp.transform.rotation = position.rotation;
        tmp.GetComponent<AnimalMovements>().lifeTime = 9999;
        tmp.name = "Event_" + animal.name;

        if (nextEvent != EventName.None)
            EventManager_.instance.TrigerEvent(nextEvent);
    }
}

public class EventTriggerer : MonoBehaviour
{
    public EventName eventToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        EventManager_.instance.TrigerEvent(eventToTrigger);
    }
}
