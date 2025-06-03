using HeneGames.DialogueSystem;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public PuzzleController puzzleController;
    private bool canIntercat = true;

    public void Interact()
    {
        puzzleController.Interact(gameObject);
    }

    // Solo para prueba con clic en el editor
    private void OnTriggerStay(Collider other)
    {
        DialogueUI.instance.ShowInteractionUI(true);
         if (Input.GetKeyDown(DialogueUI.instance.actionInput) && canIntercat)
         {
            Interact();
            canIntercat = false;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueUI.instance.ShowInteractionUI(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
