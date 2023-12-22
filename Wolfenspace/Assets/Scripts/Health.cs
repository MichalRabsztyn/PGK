using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    [System.NonSerialized] public float currentHealth;

    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;
    private DamageEffect damageEffect;

    void Start()
    {
        currentHealth = maxHealth;
        damageEffect = GetComponentInChildren<DamageEffect>();
    }

    public void ReduceHp(float damage)
    {
        damageEffect.StartEffect();

        currentHealth -= damage;

        OnHealthChanged.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHealth + healAmount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = currentHealth + healAmount;
        }
    }

    void Die()
    {
        OnDeath.Invoke();

        Destroy(gameObject);
    }
}