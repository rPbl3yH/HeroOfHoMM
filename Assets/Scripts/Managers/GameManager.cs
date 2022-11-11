using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static InputService InputService;

    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public EnemyManager EnemyManager { get; private set; }
    [field: SerializeField] public EventManager EventManager { get; private set; }
    [field: SerializeField] public SkillsManager SkillsManager { get; private set; }
    [field: SerializeField] public UIController UIController { get; private set; }

    
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