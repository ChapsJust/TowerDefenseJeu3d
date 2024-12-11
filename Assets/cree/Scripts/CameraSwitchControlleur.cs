using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitchControlleur : MonoBehaviour
{
    [SerializeField]
    private Camera fpsCamera;
    [SerializeField]
    private Camera topCamera;
    [SerializeField]
    private GameObject playerBody;

    private bool estFpsView = true;
    public bool EstFpsView => estFpsView;

    void Start()
    {

    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Permet de changer la caméra qui doit être affiché 
            estFpsView = !estFpsView;

            if (estFpsView)
                SwitchFps();
            else
                SwitchTop();
        }
    }

    private void SwitchFps()
    {
        fpsCamera.enabled = true;
        topCamera.enabled = false;
        if (playerBody != null)
            playerBody.SetActive(true);
    }

    private void SwitchTop()
    {
        fpsCamera.enabled = false;
        topCamera.enabled = true;
        if (playerBody != null)
            playerBody.SetActive(false);
    }
    
}
