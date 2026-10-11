using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public Transform spawnPoint;
    public float respawnHeightOffset = 2f;
    public float rotationSpeed = 10f;
    Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool jumpPressed;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;

        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        float speedValue = moveInput.magnitude;

        animator.SetFloat("Speed", speedValue);
        
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

        //rotacion del player
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



}
