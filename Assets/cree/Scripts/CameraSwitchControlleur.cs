using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitchControlleur : MonoBehaviour
{
    [SerializeField]
    private Camera fpsCamera;
    [SerializeField]
    private Camera topCamera;

    private bool estFpsView = true;

    void Start()
    {
        fpsCamera.enabled = true;
        topCamera.enabled = false;
    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Permet de changer la caméra qui doit être affiché 
            estFpsView = !estFpsView;
            fpsCamera.enabled = estFpsView;
            topCamera.enabled = !estFpsView;
        }
    }
    
}
