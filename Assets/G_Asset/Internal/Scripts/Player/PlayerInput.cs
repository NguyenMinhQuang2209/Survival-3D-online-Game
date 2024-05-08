using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions action;
    public PlayerInputActions.OnFootActions onFoot;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        action = new PlayerInputActions();
        onFoot = action.onFoot;
        playerMovement = GetComponent<PlayerMovement>();
        onFoot.Jump.performed += ctx => playerMovement.Jump();
    }
    private void Update()
    {
        playerMovement.Movement(onFoot.Movement.ReadValue<Vector2>().normalized);
    }
    private void LateUpdate()
    {
        playerMovement.Rotation(onFoot.Mouse.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }
}
