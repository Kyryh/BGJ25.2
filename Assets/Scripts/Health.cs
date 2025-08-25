using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public bool evil;

    [SerializeField]
    int maxHealth;
    int currentHealth;

    Vector2 knockback;
    Rigidbody2D rb;

    public UnityEvent<int, int> onHealthUpdated;

    public UnityEvent onDeath;
    public int MaxHealth {
        get {
            return maxHealth;
        }
        set {
            maxHealth = value;
            onHealthUpdated?.Invoke(CurrentHealth, MaxHealth);
        }
    }

    public int CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            onHealthUpdated?.Invoke(CurrentHealth, MaxHealth);
            if (CurrentHealth == 0) {
                onDeath?.Invoke();
            }
        }
    }

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        HealFull();
    }

    public void TakeDamage(int damage) {
        CurrentHealth -= damage;
    }

    public void Heal(int value) {
        CurrentHealth += value;
    }

    public void HealFull() {
        CurrentHealth = MaxHealth;
    }

    private void FixedUpdate() {
        rb.position += knockback;
        knockback = Vector2.Lerp(knockback, Vector2.zero, 0.1f);

    }

    internal void TakeKnockback(Vector2 force) {
        knockback = force / 3;
    }

}
