using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    Enemy enemyPrefab;

    public static GameManager Instance {
        get; private set;
    }



    void Awake() {
        Instance = this;
    }

    void Update() {
        var position = RandomPointOnUnitCircle() * 25;
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    static Vector2 RandomPointOnUnitCircle() {
        var angle = Random.Range(0f, Mathf.PI * 2);
        return new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        );
    }
}
