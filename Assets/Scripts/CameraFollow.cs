using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    float lerp = 0.1f;
    void FixedUpdate() {
        var player = MainCharacterController.Instance;
        if (player == null)
            return;

        var playerPos = player.transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var target = (playerPos + mousePos) / 2;
        target.x = Mathf.Clamp(target.x, -12, 12);
        target.y = Mathf.Clamp(target.y, -7, 7);
        target.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, target, lerp);
    }
}
