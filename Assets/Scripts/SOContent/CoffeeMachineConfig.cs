using UnityEngine;

namespace SOContent
{
    [CreateAssetMenu(fileName = "CoffeeMachineConfig", menuName = "SOContent/Coffee Machine")]
    public class CoffeeMachineConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration;
    }
}