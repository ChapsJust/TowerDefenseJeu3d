using UnityEngine;
using UnityEngine.InputSystem;

public class JoueurFpsControlleur : MonoBehaviour
{
    [SerializeField]
    private Transform joueurBody;

    private Rigidbody rb;

    private Vector2 mouvementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveJoueur();
    }

    private void MoveJoueur()
    {
        Vector3 forward = joueurBody.forward;
        Vector3 right = joueurBody.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * mouvementInput.y + right * mouvementInput.x;

        Vector3 mouvement = direction * 5f;
        rb.linearVelocity = new Vector3(mouvement.x, rb.linearVelocity.y, mouvement.z);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("1");
        mouvementInput = context.action.ReadValue<Vector2>();
    }
}

