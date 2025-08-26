using UnityEngine;

public class Enemy : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    Vector3 GetTarget() {
        return Oven.Instance.transform.position;
    }
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    private void FixedUpdate() {
        var direction = GetTarget() - transform.position;
        direction.Normalize();
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.TryGetComponent<Health>(out var health) && !health.evil) {
            health.TakeDamage(1);
            health.TakeKnockback((collision.transform.position - transform.position).normalized);
        }
    }
}
