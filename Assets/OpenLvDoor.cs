using UnityEngine;

public class OpenLvDoor : MonoBehaviour
{
    public GameObject GameObject;
    public void OpenDoor(bool isOpen)
    {
        GameObject.SetActive(isOpen);
    }
}
