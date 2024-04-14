using System.Collections;
using System.Collections.Generic;
using Unity.Sentis.Layers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    private Vector2 moveDir;
    private Vector2 lookDir;
    private bool sprint;

    public Transform cameraHolder;

    public float moveSpeed = 2f;
    public float sprintMultiplicator = 1.5f;
    public float xLookSpeed = 25f;
    public float yLookSpeed = 15f;
    public float distToStair = 0.25f;

    private float curRot = 0;
    private float minRot = -55f;
    private float maxRot = 55f;

    private float maxLookInterationDistance = 5f;
    private Interactable interactableInFront;

    private float stairSpeedRatio = 0.75f;
    private bool isInStair;

    public AudioClip footStepsHard;
    public AudioClip footStepsHardSpeed;
    public AudioClip footStepsHardStairs;
    public AudioSource footStepSource;

    private List<string> inventory = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        isInStair = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
        Animate();
        CheckInteractable();
        DebugToUI();
    }

    void FixedUpdate()
    {
        
    }

    void Animate()
    {
        animator.SetFloat("Speed", moveDir.y);
    }

    void Move()
    {
        Vector3 moveChange = (transform.forward * moveDir.y) * moveSpeed * Time.deltaTime * 10f + transform.up * (isInStair ? 10.5f * Time.deltaTime : 0f);
        Vector3 down = Vector3.zero;
        if (!Physics.Raycast(transform.position, -transform.up, distToStair) && isInStair)
        {
            down.y = -25f * Time.deltaTime;
            //down += -transform.forward * Time.deltaTime * 20f;
        }
        rb.velocity += moveChange + down;
    }

    void Look()
    {
        transform.Rotate(new Vector3(0, lookDir.x * xLookSpeed * Time.deltaTime, 0));

        float rotVal = -lookDir.y * yLookSpeed * Time.deltaTime;
        if (lookDir.y > 0 && curRot < maxRot)
        {
            float finalRot = curRot - rotVal;
            if (finalRot > maxRot)
            {
                rotVal = -(maxRot - curRot);
            }
            curRot -= rotVal;
            cameraHolder.Rotate(new Vector3(rotVal, 0, 0));
        }
        else if (lookDir.y < 0 && curRot > minRot)
        {
            float finalRot = curRot + rotVal;
            if (finalRot < minRot)
            {
                rotVal = minRot - curRot;
            }
            curRot -= rotVal;
            cameraHolder.Rotate(new Vector3(rotVal, 0, 0));
        }
    }

    void PlayFootStepsSound()
    {
        if (moveDir.magnitude > 0)
        {
            if (isInStair && footStepSource.clip != footStepsHardStairs)
            {
                footStepSource.clip = footStepsHardStairs;
            }
            if (sprint && !isInStair && footStepSource.clip != footStepsHardSpeed)
            {
                footStepSource.clip = footStepsHardSpeed;
            }
            else if (!sprint & !isInStair && footStepSource.clip != footStepsHard)
            {
                footStepSource.clip = footStepsHard;
            }
            if (!footStepSource.isPlaying)
            {
                footStepSource.Play();
            }
        }
        else
        {
            footStepSource.Stop();
        }
    }

    private void CheckInteractable()
    {
        if(Physics.Raycast(cameraHolder.position, cameraHolder.forward, out RaycastHit hit, 50f))
        {
            interactableInFront = hit.collider.GetComponent<Interactable>();
            if (interactableInFront != null)
            {
                interactableInFront.SetOutlined();
            }
        }
    }

    void DebugToUI()
    {
        //UIDebuger.DisplayValue("isInStair", "isInStair : " + isInStair);
    }

    void OnMove(InputValue v)
    {
        moveDir = v.Get<Vector2>();
    }

    void OnLook(InputValue v)
    {
        lookDir = v.Get<Vector2>();
    }

    void OnInteract(InputValue v)
    {
        if (interactableInFront != null)
        {
            if (interactableInFront.Interact())
            {
                inventory.Add(interactableInFront.name);
                GameObject.Destroy(interactableInFront.gameObject);
            }
            else
            {
                Debug.Log("Can't interact !");
            }
        }
    }
}
