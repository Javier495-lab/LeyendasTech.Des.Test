using HeneGames.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DoorButtonTemp : MonoBehaviour
{
    public InputActionReference interactAction;
    private Outline outline;

    private bool canInteract = true;

    [SerializeField] private GameObject[] puertas;
    [SerializeField] private float tiempoActivo = 5f;

    private float temporizador = 0f;
    private bool activadas = false;
    private Dictionary<GameObject, bool> estadosOriginales = new Dictionary<GameObject, bool>();
    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (activadas)
        {
            temporizador -= Time.deltaTime;
            if (temporizador <= 0f)
            {
                RevertirPuertas();
                activadas = false;
            }
        }
    }

    public void Interact()
    {
        if (!activadas)
        {
            // Guardamos los estados originales
            estadosOriginales.Clear();
            foreach (GameObject puerta in puertas)
            {
                if (puerta != null)
                {
                    estadosOriginales[puerta] = puerta.activeSelf;
                    puerta.SetActive(!puerta.activeSelf);
                }
            }

            activadas = true;
        }

        temporizador = tiempoActivo; // Reinicia temporizador
    }

    private void RevertirPuertas()
    {
        foreach (GameObject puerta in puertas)
        {
            if (puerta != null && estadosOriginales.ContainsKey(puerta))
            {
                puerta.SetActive(estadosOriginales[puerta]);
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
