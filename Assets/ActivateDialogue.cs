using System.Collections;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    // �apa sencilla a un problema del di�logo
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
