using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public string interactEvent;

    public virtual bool Interact()
    {   
        if(interactEvent != null && interactEvent != "")
        {
            //EventScriptsManager.StartEvent(interactEvent);
            interactEvent = "";
            return true;
        }
        return false;
    }
}
