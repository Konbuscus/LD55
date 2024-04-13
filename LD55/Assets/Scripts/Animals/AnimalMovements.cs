using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimalMovements : MonoBehaviour
{
    protected Rigidbody rb;

    protected Vector3 targetPosition;
    protected Vector3 lookDir;
    protected Vector3 flatLookDir;

    public bool isLandedType = true;
    public float moveSpeed = 2;
    public float moveSpeedFactor = 1f;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected void Update()
    {
        Look();
        Move();
    }

    void Move()
    {
        if (targetPosition != Vector3.zero)
        {
            MoveToward();
        }
    }

    void MoveToward()
    {
        if (Vector3.Distance(targetPosition, transform.position) < 0.25f)
        {
            targetPosition = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        Vector3 moveChange;

        if (isLandedType)
        {
            moveChange = flatLookDir * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }
        else
        {
            moveChange = lookDir * moveSpeed * moveSpeedFactor * Time.deltaTime;
        }

        UIDebuger.DisplayValue("moveChange", moveChange.ToString());
        rb.velocity += moveChange;
    }

    void Look()
    {
        if(targetPosition != Vector3.zero)
        {
            lookDir = targetPosition - transform.position;
            flatLookDir = lookDir.normalized;
            flatLookDir.y = 0;

            transform.LookAt(transform.position + flatLookDir);
        }
    }
}
