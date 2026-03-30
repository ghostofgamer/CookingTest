using Enum;
using UnityEngine;

namespace SOContent
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "SOContent/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public ItemType Type { get; private set; }
    }
}