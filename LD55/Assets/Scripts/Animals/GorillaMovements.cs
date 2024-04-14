using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaMovements : AnimalMovements
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override void SetHandledObject(GameObject o)
    {
        if (!isHandlingObject)
        {
            objectToHandle = o;
            objectToHandle.transform.parent = handledObjectHandler;
            Transform tmp = objectToHandle.GetComponent<Handlable>().handlePoint;
            //objectToHandle.transform.localPosition = tmp.localPosition * tmp.localScale.x;
            objectToHandle.transform.localRotation = tmp.localRotation;
            objectToHandle.transform.position = handledObjectHandler.position + (objectToHandle.transform.position - tmp.position);

            animator.SetTrigger("Pickup");
            animator.SetBool("IsPicking", true);
            Debug.Log("triger pickup");
        }
    }

    protected override void ReleaseHandledObject()
    {
        if (isHandlingObject)
        {
            objectToHandle = null;
            moveSpeedAnimationSpeed = 0;
            animator.SetTrigger("UnPickup");
            animator.SetBool("IsPicking", false);
        }
        else
        {
            EndStep();
        }
    }

    protected override bool HasPickedUp()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "pickup_idle")
        {
            EndStep();
            return true;
        }

        return false;
    }

    protected override bool HasUnPickedUp()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle")
        {
            EndStep();
            return true;
        }

        return false;
    }
}
