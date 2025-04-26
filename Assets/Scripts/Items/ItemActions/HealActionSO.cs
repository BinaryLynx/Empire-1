using UnityEngine;

[CreateAssetMenu(fileName = "HealActionSO", menuName = "Scriptable Objects/HealActionSO")]
public class HealActionSO : ItemActionSO
{
    // public int healAmount = 20;

    public override void Execute(GameObject owner, UsableItemSO item)
    {
        HealingItemSO healingItem = (HealingItemSO)item;
        if (owner.TryGetComponent<Health>(out var health))
        {
            health.Heal(healingItem.healAmount);
        }
    }

    public override bool CanExecute(GameObject owner, UsableItemSO item)
    {
        return owner.TryGetComponent<Health>(out var health) && health.CurrentHealth < health.MaxHealth;
    }

    public override void Cleanup(GameObject owner, UsableItemSO item)
    {

    }
}
