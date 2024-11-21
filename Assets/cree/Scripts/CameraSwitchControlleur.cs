using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitchControlleur : MonoBehaviour
{
    [SerializeField]
    private Camera fpsCamera;
    [SerializeField]
    private Camera topCamera;
    [SerializeField]
    private PlayerInput playerInput;

    private bool estFpsView = true;
    public bool EstFpsView => estFpsView;

    void Start()
    {
        fpsCamera.enabled = true;
        topCamera.enabled = false;
        playerInput.SwitchCurrentActionMap("Joueur");
    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Permet de changer la cam�ra qui doit �tre affich� 
            estFpsView = !estFpsView;

            //Cam�ra
            fpsCamera.enabled = estFpsView;
            topCamera.enabled = !estFpsView;

            if (estFpsView)
                playerInput.SwitchCurrentActionMap("Joueur");
            else
                playerInput.SwitchCurrentActionMap("TopView");
        }
    }
    
}
