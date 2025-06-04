using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlignCam : MonoBehaviour
{
    /*public InputActionReference recenterAction; // Asignar en el Inspector
    public CinemachineCamera virtualCamera;

    private CinemachinePanTilt pov;

    private void Start()
    {
        pov = virtualCamera.GetCinemachineComponent<CinemachinePanTilt>();
    }

    private void OnEnable()
    {
        recenterAction.action.performed += OnRecenter;
        recenterAction.action.Enable();
    }

    private void OnDisable()
    {
        recenterAction.action.performed -= OnRecenter;
        recenterAction.action.Disable();
    }

    private void OnRecenter(InputAction.CallbackContext context)
    {
        if (pov != null)
        {
            pov.m_RecenterToTargetHeading.m_enabled = true; // Activa recentrado

            // OPCIONAL: lo desactivamos después de un tiempo para que no se quede encendido
            Invoke(nameof(DesactivarRecentrado), 1.5f); // ajusta el tiempo según tu gusto
        }
    }

    private void DesactivarRecentrado()
    {
        if (pov != null)
        {
            pov.m_RecenterToTargetHeading.m_enabled = false;
        }
    }*/
}
