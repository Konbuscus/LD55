using UnityEngine;

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
            CloseDoors();
        }
        else
        {
            OpenDoors();
        }

        isOpen = !isOpen;
    }

    // private void OpenDoors()
    // {
    //     // Add your code here to open the doors
    //     // For example, if you are rotating the door to open
    //     leftDoor.transform.Rotate(Vector3.left * 90);
    //     rightDoor.transform.Rotate(Vector3.right * -90);
    // }

    // private void CloseDoors()
    // {
    //     // Add your code here to close the doors
    //     // For example, if you are rotating the door to close
    //     leftDoor.transform.Rotate(Vector3.up * -90);
    //     rightDoor.transform.Rotate(Vector3.up * 90);
    // }

    // 15/04/2024 AI-Tag
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    IEnumerator OpenDoors()
    {
        Quaternion startRotation = leftDoor.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 90, 0);

        float elapsedTime = 0;
        float totalAnimationTime = 1f; // Duration in seconds

        while (elapsedTime < totalAnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float amount = elapsedTime / totalAnimationTime;

            leftDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, amount);
            rightDoor.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(0, -90, 0), amount);

            yield return null;
        }
    }

    IEnumerator CloseDoors()
    {
        Quaternion startRotation = leftDoor.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        float elapsedTime = 0;
        float totalAnimationTime = 1f; // Duration in seconds

        while (elapsedTime < totalAnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float amount = elapsedTime / totalAnimationTime;

            leftDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, amount);
            rightDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, amount);

            yield return null;
        }
    }
}