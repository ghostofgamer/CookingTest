using Enum;
using ItemsContent;

namespace Serializible
{
    [System.Serializable]
    public class CookingRecipe
    {
        public ItemType ItemType;      
        public BaseItem ResultPrefab;
        public BaseItem FiledPrefab;
        public float Duration;
        public float WarningTime;
        public float FailTime;
        public bool CanFail;
        public string StartText;
        public string WarningText;
        public string FailText;
        public string DoneText;
    }
}