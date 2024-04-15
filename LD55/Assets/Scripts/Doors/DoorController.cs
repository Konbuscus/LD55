using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    private bool isOpen = false;

    public void ToggleDoors()
    {
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

    private void OpenDoors()
    {
        // Add your code here to open the doors
        // For example, if you are rotating the door to open
        leftDoor.transform.Rotate(Vector3.up * 90);
        rightDoor.transform.Rotate(Vector3.up * -90);
    }

    private void CloseDoors()
    {
        // Add your code here to close the doors
        // For example, if you are rotating the door to close
        leftDoor.transform.Rotate(Vector3.up * -90);
        rightDoor.transform.Rotate(Vector3.up * 90);
    }
}