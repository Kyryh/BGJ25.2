using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {
    public static MainCharacterController Instance {
        get; private set;
    }

    // Scream
    bool screamAvailable = true;

    // Movement
    Rigidbody2D rb;
    public float speed;
    public float acceleration;
    Vector2 velocity = Vector2.zero;

    // Feathers
    public int maxFeathers;
    public float Feathers {
        get; private set;
    }
    public float feathersRegenRate;
    [SerializeField]
    FeatherProjectile featherPrefab;


    void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        Feathers = maxFeathers;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && Feathers >= 1) {
            Feathers--;
            Instantiate(featherPrefab, transform.position, Quaternion.Euler(0, 0, GetLookRotation()));
        }

        if (Input.GetMouseButtonDown(1) && screamAvailable) {
            StartCoroutine(Scream());
        }

        velocity = Vector2.MoveTowards(velocity, GetMoveDirection() * speed, acceleration * Time.deltaTime);
        Feathers = Mathf.MoveTowards(Feathers, maxFeathers, Time.deltaTime * feathersRegenRate);
    }


    private IEnumerator Scream() {
        screamAvailable = false;

        var direction = GetLookDirection();

        var enemies = Physics2D.OverlapCircleAll(
            transform.position + (Vector3)direction * 1.5f,
            1.5f
        );
        foreach (var enemy in enemies) {
            if (enemy.TryGetComponent<Health>(out var health) && health.evil) {
                health.TakeKnockback(direction);
            }
        }
        yield return new WaitForSeconds(1);
        screamAvailable = true;
    }

    Vector2 GetMoveDirection() {
        var direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction.x += 1;
        }

        direction.Normalize();

        return direction;
    }

    Vector2 GetLookDirection() {
        var rotationVector = (
            Input.mousePosition
            - Camera.main.WorldToScreenPoint(transform.position)
        );
        rotationVector.Normalize();
        return rotationVector;
    }
    float GetLookRotation() {
        var rotationVector = GetLookDirection();

        return Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate() {
        rb.linearVelocity = velocity;
    }
}
