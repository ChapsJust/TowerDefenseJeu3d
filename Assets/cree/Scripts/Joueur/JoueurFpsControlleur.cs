using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoueurFpsControlleur : MonoBehaviour
{
    [SerializeField]
    private Transform joueurBody;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sprint = 12f;

    private Rigidbody rb;

    private Vector2 mouvementInput;

    private bool isSprinting;


    [SerializeField]
    private int vie = 5;
    public int Vie
    {
        get { return vie; }
        set { vie = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(vie == 0)
        {
            Debug.Log("DEAD");
        }
    }

    private void FixedUpdate()
    {
        MoveJoueur();
    }

    /// <summary>
    /// Fonction qui permet de géere déplacement du joueur
    /// </summary>
    private void MoveJoueur()
    {
        Vector3 forward = joueurBody.forward;
        Vector3 right = joueurBody.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * mouvementInput.y + right * mouvementInput.x;

        float currentSpeed = isSprinting ? sprint : speed;

        Vector3 mouvement = direction * currentSpeed;
        rb.linearVelocity = new Vector3(mouvement.x, rb.linearVelocity.y, mouvement.z);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mouvementInput = context.action.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
            isSprinting = true;
        else if (context.canceled)
            isSprinting = false;
    }
}

