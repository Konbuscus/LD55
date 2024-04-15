// 15/04/2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class ComputerDoorInteraction : MonoBehaviour
{
    public DoorController doorController;

    public void InteractWithDoors()
    {
        doorController.ToggleDoors();
    }
}