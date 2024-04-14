using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum InteractableEventType
{
    Carry,
    Inventory
}

public class Interactable : MonoBehaviour
{
    public InteractableEventType interactEventType = InteractableEventType.Inventory;

    private int outlinedCounter = 0;
    public GameObject objectToOutline;

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
            objectToOutline.layer = 0;
        }
    }

    public void SetOutlined()
    {
        outlinedCounter = 5;
        objectToOutline.layer = LayerMask.NameToLayer("Outlined");
    }
}
