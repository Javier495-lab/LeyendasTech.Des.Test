using System.Collections;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    // Ñapa sencilla a un problema del diálogo
    public BoxCollider dialogueColl;
    void Awake()
    {
        StartCoroutine(Enumerator());
    }

    private IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(0.4f);
        dialogueColl.enabled = true;
    }
}
