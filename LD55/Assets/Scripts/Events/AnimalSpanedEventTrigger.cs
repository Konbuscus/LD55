using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpanedEventTrigger : MonoBehaviour
{
    public EventName eventName;
    public string animalName;
    public float lifeTime;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("la");
        if(other.name == animalName)
        {
            if(lifeTime > 0)
            {
                other.GetComponent<AnimalMovements>().lifeTime = lifeTime;
            }
            Debug.Log("ui");
            EventManager_.instance.TrigerEvent(eventName);
            gameObject.SetActive(false);
        }
    }
}
