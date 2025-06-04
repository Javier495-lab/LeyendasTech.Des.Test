using HeneGames.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    public PuzzleController puzzleController;
    private Outline outline;

    [Header("UI")]
    public InputActionReference interactAction;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    public void Interact()
    {
        puzzleController.Interact(gameObject);
    }

    // Solo para prueba con clic en el editor
    private void OnTriggerStay(Collider other)
    {
        DialogueUI.instance.ShowInteractionUI(true);
        outline.enabled = true;
         if (interactAction.action.IsPressed())
         {
            Interact();
            outline.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DialogueUI.instance.ShowInteractionUI(false);
        outline.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
