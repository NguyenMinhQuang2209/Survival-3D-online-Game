using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float moveSpeed = 1f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpHeight = 10f;

    [Header("Player Rotation")]
    [SerializeField] private Transform lookAt;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float topAngle = 70f;
    [SerializeField] private float downAngle = 30f;
    [SerializeField] private float mouseSmooth = 0.1f;
    float rotateXAxis;
    float rotateYAxis;
    float currentVelocity;

    bool isGround;
    Vector3 velocity;
    private PlayerAnimator playerAnimator;
    bool jumping = false;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
        Gravity();
    }
    public void Movement(Vector2 input)
    {
        float speed = 0f;
        if (input.sqrMagnitude >= 0.1f)
        {
            speed = 1f;
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + lookAt.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, mouseSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            controller.Move(moveSpeed * Time.deltaTime * moveDir);
        }
        playerAnimator.SetFloat("Speed", speed);
    }
    public void Gravity()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        if (isGround && velocity.y <= 0f)
        {
            velocity.y = -2f;
            if (jumping)
            {
                jumping = false;
                playerAnimator.SetBool("Jump", jumping);
            }
        }
        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumping = true;
            playerAnimator.SetBool("Jump", jumping);
        }
    }
    public void Rotation(Vector2 input)
    {
        Vector3 rotDir = new(input.x * mouseSensitivity * Time.deltaTime, 0f, input.y * mouseSensitivity * Time.deltaTime);
        rotateYAxis += rotDir.x;
        rotateXAxis += rotDir.z;
        rotateXAxis = ClampAngle(rotateXAxis, downAngle, topAngle);
        rotateYAxis = ClampAngle(rotateYAxis, float.MinValue, float.MaxValue);
        lookAt.transform.rotation = Quaternion.Euler(rotateXAxis, rotateYAxis, 0f);
    }
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
