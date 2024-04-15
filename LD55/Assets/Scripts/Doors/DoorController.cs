using UnityEngine;
using System.Collections;
public class DoorController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    private bool isOpen = false;

    public void ToggleDoors()
    {
        Debug.Log("SLT");
        if (isOpen)
        {
            StartCoroutine(CloseDoors());
        }
        else
        {
            StartCoroutine(OpenDoors());
        }

        isOpen = !isOpen;
    }

    IEnumerator OpenDoors()
    {
        Vector3 leftStartPos = leftDoor.transform.position;
        Vector3 leftEndPos = leftStartPos + new Vector3(0, 0, 1);  

        Vector3 rightStartPos = rightDoor.transform.position;
        Vector3 rightEndPos = rightStartPos + new Vector3(0, 0, -1);  

        float elapsedTime = 0;
        float totalAnimationTime = 1f;   // Duration in seconds

        while (elapsedTime < totalAnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float amount = elapsedTime / totalAnimationTime;

            leftDoor.transform.position = Vector3.Lerp(leftStartPos, leftEndPos, amount);
            rightDoor.transform.position = Vector3.Lerp(rightStartPos, rightEndPos, amount);

            yield return null;
        }
    }

    IEnumerator CloseDoors()
    {
        Vector3 leftStartPos = leftDoor.transform.position;
        Vector3 leftEndPos = leftStartPos + new Vector3(0, 0, -1);  

        Vector3 rightStartPos = rightDoor.transform.position;
        Vector3 rightEndPos = rightStartPos + new Vector3(0, 0, 1);  

        float elapsedTime = 0;
        float totalAnimationTime = 1f;   // Duration in seconds

        while (elapsedTime < totalAnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float amount = elapsedTime / totalAnimationTime;

            leftDoor.transform.position = Vector3.Lerp(leftStartPos, leftEndPos, amount);
            rightDoor.transform.position = Vector3.Lerp(rightStartPos, rightEndPos, amount);

            yield return null;
        }
    }

}