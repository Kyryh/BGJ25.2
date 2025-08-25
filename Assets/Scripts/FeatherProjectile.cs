using UnityEngine;

public class FeatherProjectile : MonoBehaviour {
    Rigidbody2D rb;
    public float speed;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<Health>(out var health)
            && !collision.TryGetComponent<MainCharacterController>(out _)
        ) {
            health.CurrentHealth -= 1;
            Destroy(gameObject);
        }
    }
}
