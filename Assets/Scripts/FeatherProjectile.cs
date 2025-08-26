using UnityEngine;

public class FeatherProjectile : MonoBehaviour {
    Rigidbody2D rb;
    public float speed;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<Health>(out var health)
            && health.evil
        ) {
            health.CurrentHealth -= 1;
            Destroy(gameObject);
        }
    }
}
