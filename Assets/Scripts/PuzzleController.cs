using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class PuzzleController : MonoBehaviour
{
    public float timerDuration = 15f;
    private GameObject firstObject = null;
    private Coroutine timerCoroutine;

    private bool puzzleCompleted = false;
    [HideInInspector] public bool canIntercat = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Visual Indicator")]
    private float remainingTime;

    public void Interact(GameObject interactedObject)
    {
        if (puzzleCompleted) return;

        if (firstObject == null)
        {
            canIntercat = false;
            //candleLight.enabled = true;
            firstObject = interactedObject;
            timerCoroutine = StartCoroutine(TimerCoroutine());
            Debug.Log("Primer objeto activado. Inicia temporizador.");
        }
        else if (interactedObject != firstObject)
        {
            canIntercat = false;
            //candleLight.enabled = true;
            StopCoroutine(timerCoroutine);
            puzzleCompleted = true;
            UpdateTimerUI(""); // Oculta texto
            Debug.Log("¡Puzle completado!");
        }
    }

    private IEnumerator TimerCoroutine()
    {
        remainingTime = timerDuration;

        while (remainingTime > 0f)
        {
            UpdateTimerUI(remainingTime.ToString("F1")); // Muestra 1 decimal
            yield return null;
            remainingTime -= Time.deltaTime;
        }

        Debug.Log("Tiempo agotado. Reinicia el puzle.");
        UpdateTimerUI(""); // Oculta texto
        ResetPuzzle();
    }

    private void UpdateTimerUI(string text)
    {
        if (timerText != null)
        {
            timerText.text = text;
        }
    }

    private void ResetPuzzle()
    {
        canIntercat = false;
        //candleLight.enabled = false;
        firstObject = null;
        timerCoroutine = null;
    }
}
