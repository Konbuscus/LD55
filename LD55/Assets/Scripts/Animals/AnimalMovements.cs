using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimalMovements : MonoBehaviour
{
    protected Rigidbody rb;

    protected MovePoint targetPoint;
    protected Vector3 lookDir;
    protected Vector3 flatLookDir;

    protected List<MovePoint> targetMoves = new List<MovePoint>();
    protected float curentWaitTime = 0;
    protected float refTime;
    protected bool moveStarted = false;

    public bool isLandedType = true;
    public float moveSpeed = 2;
    public float moveSpeedFactor = 1f;

    public float jumpForce = 10000f;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        refTime = Time.time;
    }

    // Update is called once per frame
    protected void Update()
    {
        Look();
        Move();
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
                MoveToward();
            }
        }
        else
        {
            if(targetMoves.Count > 0 && refTime + curentWaitTime < Time.time)
            {
                targetPoint = targetMoves[0];
                curentWaitTime = targetPoint.timeBefore;
                refTime = Time.time;
                targetMoves.RemoveAt(0);
                moveStarted = false;
                Debug.Log(targetMoves.Count);
            }
        }
    }

    void StartMove()
    {
        if (targetPoint.moveMode == MoveMode.Jump)
        {
            float height = (targetPoint.targetPosition.y - transform.position.y + 0.35f);
            height = Mathf.Log(1 + Mathf.Pow(1 + height, 2)) * 0.85f;
            rb.AddForce(Vector3.up * jumpForce * height);
        }
    }

    void MoveToward()
    {
        float dist = Mathf.Sqrt(Mathf.Pow(targetPoint.targetPosition.x - transform.position.x, 2) + Mathf.Pow(targetPoint.targetPosition.z - transform.position.z, 2));
        //UIDebuger.DisplayValue("targetPoint", targetPoint.ToString());
        //UIDebuger.DisplayValue("dist", dist.ToString());
        if (dist < 0.25f)
        {
            curentWaitTime = targetPoint.timeAfter;
            refTime = Time.time;
            targetPoint = null;
            rb.velocity = Vector3.zero;
            Debug.Log("on");
            return;
        }


        Vector3 moveChange;

        if (targetPoint.moveMode == MoveMode.Walk)
        {
            moveChange = flatLookDir * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }
        else
        {
            moveChange = lookDir * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }

        //UIDebuger.DisplayValue("moveChange", moveChange.ToString());
        rb.velocity += moveChange;
    }

    void Look()
    {
        if(targetPoint != null)
        {
            lookDir = targetPoint.targetPosition - transform.position;
            flatLookDir = lookDir.normalized;
            flatLookDir.y = 0;

            UIDebuger.DisplayValue("flatLookDir", (transform.position + flatLookDir).ToString());

            transform.LookAt(transform.position + flatLookDir, Vector3.up);
        }
    }
}
