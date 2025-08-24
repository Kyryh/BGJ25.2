using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeathersUI : MonoBehaviour {
    TextMeshProUGUI label;
    [SerializeField]
    Image featherProgress;
    void Awake() {
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        var mainCharacter = MainCharacterController.Instance;
        var feathers = (int)mainCharacter.Feathers;
        label.text = $"{feathers} / {mainCharacter.maxFeathers}";
        featherProgress.fillAmount = mainCharacter.Feathers - feathers;
    }
}
