using HeneGames.DialogueSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DoorButton1 : MonoBehaviour
{
    public InputActionReference interactAction;
    private Outline outline;

    [SerializeField] private GameObject[] puertas;
    private bool canInteract = true;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }
    
    public void Interact()
    {
        foreach (GameObject puerta in puertas)
        {
            if (puerta != null)
            {
                puerta.SetActive(!puerta.activeSelf);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        DialogueUI.instance.ShowInteractionUI(true);
        outline.enabled = true;

        if (interactAction.action.IsPressed())
        {
            Interact();
            StartCoroutine(Deelay());
            canInteract = false;
            outline.enabled = false;
        }
    }

    private IEnumerator Deelay()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }
}
