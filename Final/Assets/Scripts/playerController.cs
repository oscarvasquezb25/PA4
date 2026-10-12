using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float speed = 8f; // Velocidad de movimiento del jugador
    public float gravity = -20f; // Gravedad aplicada al jugador
    public float jumpHeight = 2f; // Altura del salto del jugador
    public Transform spawnPoint; // Punto de reaparición del jugador
    public float respawnHeightOffset = 2f; // Offset vertical para la reaparición del jugador
    public float rotationSpeed = 10f; // Velocidad de rotación del jugador
    Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity; // Variable para controlar la velocidad vertical del jugador
    private bool jumpPressed; // Variable para controlar el salto

    private Animator animator;
    //private bool isDead = false; //Nuevo para el Bouser direccional cuando muere el player
    //private bool isBoucing = false; //Nuevo para el Bouser direccional cuando muere el player

    public Vector3 externalMoveSpeed; //Variable para el movimiento externo (por ejemplo, empujado por un enemigo o una fuerza externa)

    //Clase power-Up
    public bool canShoot = false; // Para decir que el player ya recolcto
    public GameObject powerEffect; // Efecto visual del power-up



    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;

        animator = GetComponentInChildren<Animator>();

        //Clase Power-Up
        if (powerEffect != null)
        {
            powerEffect.SetActive(false); // Desactivar el efecto visual al inicio
        }
    }
    void Update()
    {
        bool isGrounded = controller.isGrounded; // Verificar si el jugador está en el suelo
        float speedValue = moveInput.magnitude; // Valor de velocidad basado en la magnitud del input de movimiento

        animator.SetFloat("Speed", speedValue); // Actualizar el parámetro de velocidad en el Animator

        //Animator en el suelo
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;           
        }

        //Salto
        if (jumpPressed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;

            //animator - salto
            animator.SetTrigger("Jump");
        }

        verticalVelocity += gravity * Time.deltaTime;

        //Aaniator -  velocidad vertical
        animator.SetFloat("VerticalVelocity", verticalVelocity);

        //movimiento relativo a la camara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Evitar inclinacion vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        //Rotacion del player
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Movimiento original del player
        Vector3 move = moveDirection.normalized;
        move.y = verticalVelocity / speed;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log("OnMove Llamado." + moveInput);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
            //Debug.Log("OnJump Llamado.");
        }
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }


    public void Die()
    {
        controller.enabled = false;

        Vector3 respawnPosition = spawnPoint.position + Vector3.up * respawnHeightOffset;
        transform.position = respawnPosition;
        verticalVelocity = 0f;
        controller.enabled = true;
    }


    //Clase Power-Up
    public void UnlockShottPower()
    {
        canShoot = true;
        if (powerEffect != null)
        {
            powerEffect.SetActive(true); // Activar el efecto visual cuando se desbloquea el power-up
        }
        Debug.Log("Power-Up optenido.");
    }
}
