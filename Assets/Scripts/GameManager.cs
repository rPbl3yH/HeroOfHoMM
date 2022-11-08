using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static InputService InputService;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        RegisterInputService();
    }

    private static void RegisterInputService() {
        if (Application.isEditor) {
            InputService = new StandaloneInputService();
        }
        else {
            InputService = new MobileInputService();
        }
    }

    private void Update() {
        
    }
}