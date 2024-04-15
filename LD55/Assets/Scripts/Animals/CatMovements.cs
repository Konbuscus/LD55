using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovements : AnimalMovements
{
    private Transform target;
    private float circleRadius = 1f;
    private float circleSpeed = 0.05f;

    private bool isMovingAround = false;
    private float currentCirclePos = -1;

    public List<AudioClip> audios;

    protected new void Start()
    {

        GetComponentInChildren<AudioSource>().clip = audios[new System.Random().Next(0,audios.Count )];
        base.Start();
        /*
        targetMoves.Add(new MovePoint(new Vector3(3, 0, 6), MoveMode.Walk, 1f, 0.5f));
        targetMoves.Add(new MovePoint(new Vector3(2, 0.5f, 6), MoveMode.Jump, 0f, 1f));
        targetMoves.Add(new MovePoint(Vector3.zero, MoveMode.PickUp, 0f, 1f));
        targetMoves.Add(new MovePoint(new Vector3(3, 0, 6), MoveMode.Jump));
        targetMoves.Add(new MovePoint(new Vector3(3, 0, 1), MoveMode.Walk, 0.5f));
        */


        //Transform tmp = GameObject.Find("marker").transform;
        //SetMoveAroundTarget(tmp);

        //SetHandledObject(GameObject.Find("Pickup Key Card"));
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        if (targetMoves.Count == 0)
        {
            if (isMovingAround)
            {
                MoveAround();
            }
            else if (target != null)
            {
                float dist = Mathf.Sqrt(Mathf.Pow(target.position.x - transform.position.x, 2) + Mathf.Pow(target.position.z - transform.position.z, 2));
                isMovingAround = dist < circleRadius;
                if (isMovingAround) return;

                targetMoves.Add(new MovePoint(target.position, MoveMode.Walk));
            }
        }
    }

    protected override void StartMove()
    {
        if (targetPoint.moveMode == MoveMode.Jump)
        {
            float height = (targetPoint.targetPosition.y - transform.position.y + 0.35f);
            height = Mathf.Log(1 + Mathf.Pow(1 + height, 2)) * 0.85f;
            Mathf.Max(0, height);
            rb.AddForce(Vector3.up * jumpForce * height);
            animator.SetTrigger("Jump");
        }
        base.StartMove();
    }

    protected void MoveAround()
    {
        if(currentCirclePos < 0)
        {
            currentCirclePos = Mathf.Atan2(target.position.z - transform.position.z, target.position.x - transform.position.x);
        }
        currentCirclePos += circleSpeed;
        Vector3 tmpPos = target.position + new Vector3(-Mathf.Cos(currentCirclePos), 0,-Mathf.Sin(currentCirclePos));
        tmpPos.y = transform.position.y;
        transform.LookAt(tmpPos);
        rb.MovePosition(tmpPos);
        moveSpeedAnimationSpeed = circleSpeed * 100f;
    }

    public void SetMoveAroundTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].point.y < transform.position.y)
        {
            animator.SetBool("Landed", true);
        }
    }
}
