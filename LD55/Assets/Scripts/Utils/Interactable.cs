using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public string interactEvent;

    private int outlinedCounter = 0;
    public GameObject objectToOutline;

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
