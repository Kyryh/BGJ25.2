using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Health : MonoBehaviour {

    public new SpriteRenderer renderer;
    [SerializeField]
    float iframes;
    bool invincible = false;

    public bool evil;
    public bool immovable;

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
        if (invincible)
            return;
        CurrentHealth -= damage;
        StartCoroutine(GiveIFrames());
    }

    public void Heal(int value) {
        CurrentHealth += value;
    }

    public void HealFull() {
        CurrentHealth = MaxHealth;
    }

    private void FixedUpdate() {
        if (rb) {
            rb.position += knockback;
            knockback = Vector2.Lerp(knockback, Vector2.zero, 0.1f);
        }

    }

    IEnumerator GiveIFrames() {
        if (iframes == 0)
            yield break;
        yield return null;
        invincible = true;
        var color = renderer.color;
        color.a = 0.5f;
        renderer.color = color;
        yield return new WaitForSeconds(iframes / 60);
        invincible = false;
        color.a = 1f;
        renderer.color = color;
    }

    internal void TakeKnockback(Vector2 force) {
        if (invincible)
            return;
        knockback = force / 3;
    }

}
