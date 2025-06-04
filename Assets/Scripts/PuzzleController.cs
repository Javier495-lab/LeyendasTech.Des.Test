using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PuzzleController : MonoBehaviour
{
    [Header("References")]
    public GameObject interactableA;
    public GameObject interactableB;
    public Light lightA;
    public Light lightB;
    public TextMeshProUGUI timerText;

    [Header("Puzzle Timer")]
    public float timerDuration = 15f;

    [Header("Puzzle Event")]
    public UnityEvent endDialogueEvent;

    private GameObject firstActivated;
    private Coroutine timerCoroutine;
    private bool puzzleCompleted = false;

    public void Interact(GameObject interactedObject)
    {
        if (puzzleCompleted) return;

        if (firstActivated == null)
        {
            firstActivated = interactedObject;
            ActivateLight(interactedObject);
            timerCoroutine = StartCoroutine(StartTimer());
            Debug.Log("Primer objeto activado.");
        }
        else if (interactedObject != firstActivated)
        {
            ActivateLight(interactedObject);
            StopCoroutine(timerCoroutine);
            CompletePuzzle();
        }
    }

    private IEnumerator StartTimer()
    {
        float remaining = timerDuration;

        while (remaining > 0f)
        {
            timerText.text = remaining.ToString("F1");
            remaining -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Tiempo agotado. Reinicio.");
        ResetPuzzle();
    }

    private void CompletePuzzle()
    {
        puzzleCompleted = true;
        endDialogueEvent.Invoke();
        timerText.text = ""; // seguro que hay mejores formas de hacerlo, pero esta es basante fácil para lo que es
        Debug.Log("¡Puzle completado!");
    }

    private void ResetPuzzle()
    {
        firstActivated = null;
        puzzleCompleted = false;
        timerText.text = "";

        if (lightA) lightA.enabled = false;
        if (lightB) lightB.enabled = false;
    }

    private void ActivateLight(GameObject obj)
    {
        if (obj == interactableA && lightA) lightA.enabled = true;
        if (obj == interactableB && lightB) lightB.enabled = true;
    }
}
