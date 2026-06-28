using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public const float MaxHealth = 100f;

    [SerializeField]
    private GameControl gameControl;

    private float currentHealth;

    // Fired whenever health changes: (currentHealth, maxHealth)
    public event Action<float, float> OnHealthChanged;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0f, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

        if (currentHealth <= 0f)
            gameControl.GameOver();
    }

    public void FullHeal()
    {
        currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }
}
