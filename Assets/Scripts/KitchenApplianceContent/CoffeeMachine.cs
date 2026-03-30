using AttentionContent;
using Enum;
using PlayerContent;

namespace KitchenApplianceContent
{
    public class CoffeeMachine : KitchenAppliance
    {
        protected override void OnAction(PlayerController playerController)
        {
            if (State == TaskState.InProgress || State == TaskState.Warning)
            {
                AttentionHintActivator.ShowHint("Кофе готовится...");
                return;
            }

            if ((State == TaskState.Completed || State == TaskState.Failed) && !IsReadyToTake)
            {
                StopProcess();
                IsReadyToTake = true;
                return;
            }

            if (CurrentItem == null)
            {
                var held = playerController.HeldItem;

                if (held == null || held.Data.Type != ItemType.CupEmpty)
                {
                    AttentionHintActivator.ShowHint("Нужен пустой стакан");
                    return;
                }

                SetItemOnAppliance(held);
                playerController.ClearHands();
                return;
            }

            if (CurrentItem.Data.Type == ItemType.CupEmpty)
            {
                if (State == TaskState.Idle)
                    TryStartProcess(playerController);
            }
            else
            {
                if (State == TaskState.Completed || State == TaskState.Failed )
                {
                    if (playerController.HeldItem == null)
                        TakeResult(playerController);
                    else
                        AttentionHintActivator.ShowHint("Освободи руки чтобы взять !");
                }
            }
        }

        /*protected override void ReplaceItemPrefab(BaseItem prefab)
        {
            if (CurrentItem == null || prefab == null) return;

            Vector3 pos = CurrentItem.transform.position;
            Quaternion rot = CurrentItem.transform.rotation;

            Destroy(CurrentItem.gameObject);
            CurrentItem = Instantiate(prefab, pos, rot, ItemPosition);
            CurrentItem.SetRBValueCollider(true);
            CurrentItem.SetValueCollider(false);
        }*/

        /*protected override void SetItemOnAppliance(BaseItem item)
        {
            CurrentItem = item;
            CurrentItem.SetRBValueCollider(true);
            CurrentItem.gameObject.transform.SetParent(ItemPosition);
            CurrentItem.gameObject.transform.localPosition = Vector3.zero;
            CurrentItem.gameObject.transform.localRotation = Quaternion.identity;
            CurrentItem.SetValueCollider(false);
        }*/

        private void TryStartProcess(PlayerController player)
        {
            var recipe = GetRecipe(CurrentItem);

            if (recipe == null)
            {
                AttentionHintActivator.ShowHint("Это не подходит для кофе");
                return;
            }

            StartProcess(recipe);
        }
    }
}