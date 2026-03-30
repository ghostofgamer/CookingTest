using Enum;

namespace Serializible
{
    [System.Serializable]
    public class CookingRecipe
    {
        public ItemType ItemType;
        public float CookTime;
        public float WarningTime;
        public bool CanBurn;
    }
}