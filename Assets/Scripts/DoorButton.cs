using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using HeneGames.DialogueSystem;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    [Header("Puertas a controlar")]
    public List<GameObject> doors;

    [Header("Input")]
    public InputActionReference interactAction;

    [Header("Temporizador")]
    public float duration = 5f;

    private bool isActive = false;
    private Coroutine resetCoroutine;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueUI.instance.ShowInteractionUI(true);
            outline.enabled = true;

            if (interactAction.action.WasPressedThisFrame())
            {
                Interact();
                outline.enabled = false;
            }
        }
    }

    private void Interact()
    {
        isActive = !isActive;
        SetDoorsActive(isActive);

        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine); // Reinicia el temporizador si ya estaba corriendo
        }

        resetCoroutine = StartCoroutine(ResetDoorsAfterTime());
    }

    private IEnumerator ResetDoorsAfterTime()
    {
        yield return new WaitForSeconds(duration);
        isActive = !isActive;
        SetDoorsActive(isActive);
        resetCoroutine = null;
    }

    private void SetDoorsActive(bool active)
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
                door.SetActive(active);
        }
    }
}
