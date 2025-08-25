using UnityEngine;

public class Enemy : MonoBehaviour {
    void Awake() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<Health>(out var health) && !health.evil) {
            health.TakeDamage(1);
            health.TakeKnockback((collision.transform.position - transform.position).normalized);
        }
    }
}
