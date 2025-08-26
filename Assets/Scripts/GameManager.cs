using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    CommonEnemy[] enemyPrefabs = new CommonEnemy[4];

    [SerializeField]
    float[] enemySpawnChance = new float[10];

    [SerializeField]
    Weights[] enemyTypeWeights = new Weights[10];

    [System.Serializable]
    class Weights {
        [SerializeField]
        int[] arr = new int[4];

        public int this[int i] {
            get => arr[i];
        }
        public int Sum() {
            return arr.Sum();
        }
    }

    public static GameManager Instance {
        get; private set;
    }



    void Awake() {
        Instance = this;
    }


    void FixedUpdate() {
        var time = Time.timeSinceLevelLoad;

        var phase = (int)(time / 30);

        if (Random.value < enemySpawnChance[phase] * Time.fixedDeltaTime) {
            var weights = enemyTypeWeights[phase];

            var random = Random.Range(0, weights.Sum());
            int i;

            for (i = 0; weights[i] <= random; i++) {
                random -= weights[i];
            }

            var enemyPrefab = enemyPrefabs[i];

            var position = RandomPointOnUnitCircle() * 25;
            Instantiate(enemyPrefab, position, Quaternion.identity);

        }
    }

    static Vector2 RandomPointOnUnitCircle() {
        var angle = Random.Range(0f, Mathf.PI * 2);
        return new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        );
    }
}
