using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField]
    int maxHealth;
    int currentHealth;

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

}
