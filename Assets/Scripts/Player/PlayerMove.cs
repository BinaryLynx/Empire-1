using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    InputAction moveAction;
    public int moveSpeed = 5;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        //move
        Vector2 movement_input = moveAction.ReadValue<Vector2>();
        Vector3 movement = (transform.right * movement_input.x + transform.forward * movement_input.y) * Time.deltaTime * moveSpeed;
        controller.Move(movement);

    }
}
