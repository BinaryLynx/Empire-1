using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ItemActionSO : ScriptableObject
{
    public string actionName = "Use";
    public InputActionReference inputAction;
    public string actionDescription;

    public abstract void Execute(GameObject owner, UsableItemSO item);

    public abstract bool CanExecute(GameObject owner, UsableItemSO item);

    public abstract void Cleanup(GameObject owner, UsableItemSO item);
}
