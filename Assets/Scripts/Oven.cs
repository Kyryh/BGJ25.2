using UnityEngine;

public class Oven : MonoBehaviour {
    public static Oven Instance {
        get; private set;
    }
    void Awake() {
        Instance = this;
    }

    void Update() {

    }
}
