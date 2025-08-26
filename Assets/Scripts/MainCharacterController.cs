using System.Collections;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {
    public static MainCharacterController Instance {
        get; private set;
    }

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

        velocity = Vector2.MoveTowards(velocity, GetMoveDirection() * speed, acceleration * Time.deltaTime);
        Feathers = Mathf.MoveTowards(Feathers, maxFeathers, Time.deltaTime * feathersRegenRate);
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

    float GetLookRotation() {
        var rotationVector = (
            Input.mousePosition
            - Camera.main.WorldToScreenPoint(transform.position)
        );

        return Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate() {
        rb.linearVelocity = velocity;
    }
}
