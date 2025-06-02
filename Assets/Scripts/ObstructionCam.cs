using System.Collections.Generic;
using UnityEngine;

public class ObstructionCam : MonoBehaviour
{
    [Header("Target Reference")]
    public Transform targetToFollow; 

    [Header("Transparency Settings")]
    [Range(0f, 1f)]
    public float transparentAlpha = 0.3f;
    public float checkInterval = 0.1f;

    [Header("Layer Mask")]
    public LayerMask obstructionMask;

    private List<Renderer> transparentRenderers = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Start()
    {
        InvokeRepeating(nameof(CheckObstructions), 0f, checkInterval);
    }

    void CheckObstructions()
    {
        RestoreMaterials();

        Vector3 direction = targetToFollow.position - transform.position;
        float distance = direction.magnitude;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, obstructionMask); // 👈 LayerMask aquí

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null && hit.collider.gameObject != targetToFollow.gameObject)
            {
                if (!originalMaterials.ContainsKey(rend))
                {
                    originalMaterials[rend] = rend.materials;
                }

                Material[] mats = rend.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    Material mat = new Material(mats[i]); // Copia nueva

                    // Configuración para transparencia
                    mat.SetFloat("_Surface", 1); // Para URP
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;

                    Color c = mat.color;
                    c.a = transparentAlpha;
                    mat.color = c;

                    mats[i] = mat;
                }

                rend.materials = mats;
                transparentRenderers.Add(rend);
            }
        }
    }

    void RestoreMaterials()
    {
        foreach (var rend in transparentRenderers)
        {
            if (rend != null && originalMaterials.ContainsKey(rend))
            {
                rend.materials = originalMaterials[rend];
            }
        }

        transparentRenderers.Clear();
        originalMaterials.Clear();
    }
}
