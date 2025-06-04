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

    [Header("Raycast Spread Settings")]
    public int rayCount = 5;
    public float horizontalSpread = 1;

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

        for (int i = 0; i < rayCount; i++)
        {
            float offset = ((i - (rayCount - 1) / 2f) / (rayCount - 1)) * horizontalSpread;
            Vector3 lateralOffset = transform.right * offset;

            Vector3 origin = transform.position + lateralOffset;
            Vector3 direction = targetToFollow.position - origin;
            float distance = direction.magnitude;

            Ray ray = new Ray(origin, direction.normalized);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance, obstructionMask);

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
                    for (int m = 0; m < mats.Length; m++)
                    {
                        Material mat = new Material(mats[m]); // copia para no modificar el original

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

                        mats[m] = mat;
                    }

                    rend.materials = mats;
                    if (!transparentRenderers.Contains(rend))
                        transparentRenderers.Add(rend);
                }
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
