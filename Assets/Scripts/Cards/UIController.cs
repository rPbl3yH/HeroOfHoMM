using UnityEngine;

public class UIController : MonoBehaviour
{
    [field: SerializeField] public CardsManager CardsManager { get; private set; }
    [field: SerializeField] public HealthUI HealthUI { get; private set; }
}
