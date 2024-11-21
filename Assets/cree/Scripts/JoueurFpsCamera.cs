using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoueurFpsCamera : MonoBehaviour
{
    [SerializeField]
    private Transform joueurBody;
    [SerializeField]
    private Transform fpsCamera;

    [SerializeField]
    private float sensitivity = 80f;
    [SerializeField]
    private Vector2 sourisInput = Vector2.zero;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (sourisInput != Vector2.zero)
        {
            float sourisX = sourisInput.x * sensitivity * Time.deltaTime;
            joueurBody.Rotate(Vector3.up, sourisX);

            float sourisY = sourisInput.y * sensitivity * Time.deltaTime;
            xRotation -= sourisY;
            xRotation = Mathf.Clamp(xRotation, -70, 40f);


            fpsCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    public void OnLook(InputAction.CallbackContext contexte)
    {
        sourisInput = contexte.ReadValue<Vector2>();
    }
}
