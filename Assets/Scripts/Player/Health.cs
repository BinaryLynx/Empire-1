using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }
}
