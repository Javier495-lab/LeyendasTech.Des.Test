using HeneGames.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class ResetPuzzle : MonoBehaviour
{
    public static ResetPuzzle Instance { get; private set; }
    public Transform ResetPoint;   
    public InputActionReference interactAction;
    private GameObject player;
    private bool canInteract = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void UpdateReset(Transform resetPoint)
    {
        ResetPoint = resetPoint;
    }

    private void Update()
    {
        if (interactAction.action.IsPressed() && canInteract && ResetPoint != null)
        {
            player.transform.position = transform.position;
            StartCoroutine(Deelay());
            canInteract = false;
        }
    }
    private IEnumerator Deelay()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }
}
