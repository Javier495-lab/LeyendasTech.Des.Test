using UnityEngine;

public class UpdateReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ResetPuzzle.Instance.UpdateReset(transform);
            Debug.Log("bhbvhjf");
        }
    }
}
