using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public enum AnimalType
{
    Bird,
    Cat,
    Mouse,
    Monkey,
    Boar,
    Gorilla
}

public class AnimalMovements : MonoBehaviour
{
    protected Rigidbody rb;

    protected Animator animator;

    protected MovePoint targetPoint;
    protected Vector3 lookDir;
    protected Vector3 flatLookDir;

    protected List<MovePoint> targetMoves = new List<MovePoint>();
    protected float curentWaitTime = 0;
    protected float refTime;
    protected bool moveStarted = false;

    public AnimalType animalType;
    public bool isLandedType = true;
    public float moveSpeed = 2;
    public float moveSpeedFactor = 1f;
    protected float moveSpeedAnimationSpeed = 0;

    public float jumpForce = 10000f;

    public Transform floorDetector;
    private bool isLanded;

    public Transform handledObjectHandler;
    private bool isHandlingObject = false;
    protected GameObject objectToHandle;

    public float lifeTime;
    private float refLifeTime;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        refTime = Time.time;
        refLifeTime = Time.time;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Time.time > refLifeTime + lifeTime)
        {
            GameObject.Destroy(gameObject);
        }

        Look();
        Move();

        CheckLanded();

        Animate();
        HandleObject();
    }

    void Move()
    {
        if (targetPoint != null)
        {
            //Debug.Log((refTime + curentWaitTime).ToString() + " :: " + Time.time.ToString());
            if (refTime + curentWaitTime < Time.time)
            {
                if (!moveStarted)
                {
                    StartMove();
                    moveStarted = true;
                }

                if (targetPoint.moveMode == MoveMode.PickUp)
                {
                    PickUpClosestObject();
                }
                else if (targetPoint.moveMode == MoveMode.UnPickUp)
                {
                    ReleaseHandledObject();
                }
                else if (targetPoint.moveMode == MoveMode.Walk || targetPoint.moveMode == MoveMode.Jump || targetPoint.moveMode == MoveMode.Fly)
                {
                    MoveToward();
                }
            }
        }
        else
        {
            if(targetMoves.Count > 0 && refTime + curentWaitTime < Time.time)
            {
                Debug.LogWarning("next");
                targetPoint = targetMoves[0];
                Debug.LogWarning(targetPoint.moveMode.ToString());
                Debug.LogWarning(targetPoint.targetPosition.ToString());
                Debug.LogWarning(targetPoint.targetGameobject.transform.position.ToString());
                curentWaitTime = targetPoint.timeBefore;
                refTime = Time.time;
                targetMoves.RemoveAt(0);
                moveStarted = false;
            }
        }
    }

    void CheckLanded()
    {
        RaycastHit[] hits = Physics.RaycastAll(floorDetector.position, Vector3.down, 0.25f);
        hits = hits.Where(h => h.collider.gameObject != gameObject).ToArray();
        isLanded = hits.Length > 0;
    }

    protected virtual void StartMove()
    {

    }

    void MoveToward()
    {
        Vector3 targetPosition = ((targetPoint.targetGameobject != null) ? targetPoint.targetGameobject.transform.position : targetPoint.targetPosition);
        float dist = Mathf.Sqrt(Mathf.Pow(targetPosition.x - transform.position.x, 2) + Mathf.Pow(targetPosition.z - transform.position.z, 2));
        UIDebuger.DisplayValue("targetPosition", targetPosition.ToString());
        //UIDebuger.DisplayValue("dist", dist.ToString());
        if (dist < targetPoint.stopDistance)
        {
            EndStep();

            return;
        }


        Vector3 moveChange;

        if (targetPoint.moveMode == MoveMode.Walk || (targetPoint.moveMode == MoveMode.Jump && floorDetector.position.y > targetPosition.y + 0.2f))
        {
            moveChange = flatLookDir * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }
        else
        {
            moveChange = lookDir.normalized * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }

        UIDebuger.DisplayValue("moveChange", moveChange.ToString());
        rb.velocity += moveChange;

        moveSpeedAnimationSpeed = rb.velocity.magnitude / 2f;
    }

    void Look()
    {
        if(targetPoint != null)
        {
            Vector3 targetPosition = ((targetPoint.targetGameobject != null) ? targetPoint.targetGameobject.transform.position : targetPoint.targetPosition);

            lookDir = targetPosition - transform.position;
            flatLookDir = lookDir.normalized;
            flatLookDir.y = 0;

            UIDebuger.DisplayValue("flatLookDir", (transform.position + flatLookDir).ToString());

            transform.LookAt(transform.position + flatLookDir, Vector3.up);
        }
    }

    void HandleObject()
    {
        if (objectToHandle != null && !isHandlingObject && HasPickedUp())
        {
            objectToHandle.transform.parent = handledObjectHandler;
            Transform tmp = objectToHandle.GetComponent<Handlable>().handlePoint;
            //objectToHandle.transform.localPosition = tmp.localPosition * tmp.localScale.x;
            objectToHandle.transform.localRotation = tmp.localRotation;
            objectToHandle.transform.position = handledObjectHandler.position + (objectToHandle.transform.position - tmp.position);
            isHandlingObject = true;
        }
        else if(objectToHandle == null && isHandlingObject && HasUnPickedUp())
        {
            if (handledObjectHandler.childCount > 0)
            {
                handledObjectHandler.GetChild(0).parent = null;
            }
            isHandlingObject = false;
        }
    }

    protected void Animate()
    {
        animator.SetBool("Landed", isLanded);
        animator.SetFloat("Speed", moveSpeedAnimationSpeed);
    }

    protected void EndStep()
    {
        if (targetPoint != null)
        {
            curentWaitTime = targetPoint.timeAfter;
            refTime = Time.time;
            targetPoint = null;
            rb.velocity = Vector3.zero;
            moveSpeedAnimationSpeed = rb.velocity.magnitude / 2f;
        }
    }

    protected virtual bool HasPickedUp()
    {
        EndStep();

        return true;
    }

    protected virtual bool HasUnPickedUp()
    {
        EndStep();

        return true;
    }

    protected void SetHandledObject(GameObject o)
    {
        if (!isHandlingObject)
        {
            objectToHandle = o;
        }
    }

    protected void ReleaseHandledObject()
    {
        if (isHandlingObject)
        {
            objectToHandle = null;
            moveSpeedAnimationSpeed = 0;
        }
        else
        {
            EndStep();
        }
    }

    protected void PickUpClosestObject()
    {
        Collider[] cols = Physics.OverlapSphere(handledObjectHandler.position, 1f);
        Handlable h = cols.Select(c => c.GetComponent<Handlable>()).FirstOrDefault(c =>  c != null);
        if (h)
        {
            SetHandledObject(h.gameObject);
            moveSpeedAnimationSpeed = 0;
        }
        else
        {
            EndStep();
        }
    }

    public void SetTargetMoves(List<MovePoint> targetMoves)
    {
        this.targetMoves = targetMoves;
    }
}
