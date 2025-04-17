using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    InputAction lookAction;
    public float mouseSensitivity = 50;
    float xRotation = 0f;
    public Transform playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        Vector2 rotationinput = lookAction.ReadValue<Vector2>();
        Vector2 rotation = rotationinput * Time.deltaTime * mouseSensitivity;

        //left-right Y rotation
        player.Rotate(Vector3.up * rotation.x);

        //up-down X rotation
        xRotation -= rotation.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
