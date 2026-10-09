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

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool jumpPressed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;           
        }
        if (jumpPressed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        //Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        //move.y= verticalVelocity / speed;

        //Direccion del movimiento 
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

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
