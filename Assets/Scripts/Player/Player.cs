using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool mouseActive = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            SwitchUIFocus();
        }
    }

    public void SwitchUIFocus()
    {
        mouseActive = !mouseActive;
        PlayerLook playerLook = gameObject.GetComponentInChildren<PlayerLook>();

        playerLook.enabled = !mouseActive;
        Cursor.lockState = mouseActive ? CursorLockMode.None : CursorLockMode.Locked;

    }

}