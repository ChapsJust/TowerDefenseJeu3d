using UnityEngine;
using UnityEngine.InputSystem;

public class JoueurTopCamera : MonoBehaviour
{
    [SerializeField]
    private Transform topCamera;
    [SerializeField]
    private float speed = 10f;

    private Vector2 movementInput;

    void Start()
    {
        
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        topCamera.position += movement * speed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
