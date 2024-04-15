using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Sentis.Layers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

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

    private GameObject selectedAnimal;

    public List<MyObjectObject<AnimalType, GameObject>> animalsPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        isInStair = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        AnimalSelector.instance.AddAnimalSelector(AnimalType.Bird);
        AnimalSelector.instance.AddAnimalSelector(AnimalType.Cat);
        AnimalSelector.instance.AddAnimalSelector(AnimalType.Gorilla);
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
        if (Physics.Raycast(cameraHolder.position, cameraHolder.forward, out RaycastHit distHit, 25f))
        {
            float radius = 0.2f;
            Interactable h = distHit.collider.GetComponent<Interactable>();
            if (h == null)
            {
                RaycastHit[] hits = Physics.SphereCastAll(cameraHolder.position, radius, cameraHolder.forward, 25f);
                h = hits.Select(h => h.collider.GetComponent<Interactable>()).FirstOrDefault(h => h != null);
            }
            if (h != null)
            {
                interactableInFront = h;
                h.SetOutlined();
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
            if(interactableInFront.interactEventType == InteractableEventType.Animal)
            {
                selectedAnimal = interactableInFront.mainGameObject;
            }
            else if(interactableInFront.interactEventType == InteractableEventType.Inventory || interactableInFront.interactEventType == InteractableEventType.Carry)
            {
                if (Vector3.Distance(interactableInFront.mainGameObject.transform.position, cameraHolder.position) > 2)
                {
                    if(selectedAnimal == null)
                    {
                        Debug.Log("no animal selected !");
                        return;
                    }
                    AnimalMovements a = selectedAnimal.GetComponent<AnimalMovements>();
                    a.SetTargetMoves(interactableInFront.GetComponent<Handlable>().GetWaypoints(a.animalType));
                }
                else
                {
                    if (interactableInFront.Interact())
                    {
                        inventory.Add(interactableInFront.mainGameObject.name);
                        GameObject.Destroy(interactableInFront.mainGameObject);
                    }
                    else
                    {
                        Debug.Log("Can't interact !");
                    }
                }
            }
        }
    }

    void OnScroll(InputValue v)
    {
        Vector2 val = v.Get<Vector2>();
        if(val.sqrMagnitude > 0)
            AnimalSelector.instance.ChangeSelection(val.y < 0);
    }

    void OnInvoke(InputValue v)
    {
        if (Physics.Raycast(cameraHolder.position, cameraHolder.forward, out RaycastHit hit, 5f))
        {
            GameObject tmp = Instantiate(animalsPrefabs.FirstOrDefault(x => x.obj1 == AnimalSelector.instance.GetSelectedAnimalType()).obj2);
            tmp.transform.position = hit.point + Vector3.up * (tmp.transform.position.y - tmp.transform.Find("FloorDetector").position.y);
        }
    }
}
