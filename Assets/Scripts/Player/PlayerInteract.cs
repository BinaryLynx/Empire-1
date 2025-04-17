using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private bool showDebugRay = true;

    private int interactiveLayer; // Will store the layer mask for "Interactive"
    public event Action<GameObject> OnContainerPlayerInteract;
    public event Action OnContainerPlayerClose;

    void Start()
    {
        // Get the layer by name and convert it to a layer mask
        interactiveLayer = LayerMask.GetMask("Interactive");

    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (showDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);
        }

        // Only detect objects on the "Interactive" layer
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactiveLayer))
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                Debug.Log("Hit Interactive Object: " + hit.collider.gameObject.name);

                OpenContainer(hit.collider.gameObject);
            }

        }
    }
    void OpenContainer(GameObject container)
    {
        // Inventory containerInventory = container.GetComponent<Inventory>();
        gameObject.GetComponent<Player>().SwitchUIFocus();
        OnContainerPlayerInteract(container);

    }

    public void CloseContainer()
    {
        OnContainerPlayerClose();
    }
}
