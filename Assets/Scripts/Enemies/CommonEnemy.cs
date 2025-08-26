using UnityEngine;

public class CommonEnemy : MonoBehaviour {
    Rigidbody2D rb;
    Health health;
    [SerializeField]
    float speed;
    Vector3 GetTarget() {
        return Oven.Instance.transform.position;
    }
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void Update() {

    }

    private void FixedUpdate() {
        var direction = GetTarget() - transform.position;
        direction.Normalize();
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.TryGetComponent<Health>(out var other) && !other.evil) {
            other.TakeDamage(1);
            if (other.immovable) {
                health.TakeKnockback((transform.position - collision.transform.position).normalized);
            } else {
                other.TakeKnockback((collision.transform.position - transform.position).normalized);
            }
        }
    }
}
