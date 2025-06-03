using UnityEngine;
using TMPro;
using System.Collections;

public class PuzzleController : MonoBehaviour
{
    public float timerDuration = 15f;
    private GameObject firstObject = null;
    private Coroutine timerCoroutine;

    private bool puzzleCompleted = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Visual Indicator")]
    public Light candleLight;

    private float remainingTime;

    public void Interact(GameObject interactedObject)
    {
        if (puzzleCompleted) return;

        if (firstObject == null)
        {
            candleLight.enabled = true;
            firstObject = interactedObject;
            timerCoroutine = StartCoroutine(TimerCoroutine());
            Debug.Log("Primer objeto activado. Inicia temporizador.");
        }
        else if (interactedObject != firstObject)
        {
            candleLight.enabled = true;
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
        candleLight.enabled = false;
        firstObject = null;
        timerCoroutine = null;
    }
}
