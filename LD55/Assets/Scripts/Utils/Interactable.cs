using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum InteractableEventType
{
    Carry,
    Inventory,
    Animal
}

public class Interactable : MonoBehaviour
{
    public InteractableEventType interactEventType = InteractableEventType.Inventory;

    private int outlinedCounter = 0;
    public List<GameObject> objectsToOutline;
    public GameObject mainGameObject;

    public virtual bool Interact()
    {   
        if(interactEventType == InteractableEventType.Inventory)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        outlinedCounter--;
        if (outlinedCounter == 0)
        {
            foreach (GameObject obj in objectsToOutline)
            {
                obj.layer = 0;
            }
        }
    }

    public void SetOutlined()
    {
        outlinedCounter = 5;
        foreach (GameObject obj in objectsToOutline)
        {
            obj.layer = LayerMask.NameToLayer("Outlined");
        }
    }
}
