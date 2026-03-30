using AttentionContent;
using Enum;
using PlayerContent;

namespace KitchenApplianceContent
{
    public class FryingPan : KitchenAppliance
    {
        /*protected override void SetItemOnAppliance(BaseItem item)
        {
            CurrentItem = item;
            CurrentItem.SetRBValueCollider(true);
            CurrentItem.gameObject.transform.SetParent(ItemPosition);
            CurrentItem.gameObject.transform.localPosition = Vector3.zero;
            CurrentItem.gameObject.transform.localRotation = Quaternion.identity;
            CurrentItem.SetValueCollider(false);
        }*/

        protected override void OnAction(PlayerController player)
        {
            if (State == TaskState.InProgress || State == TaskState.Warning)
            {
                AttentionHintActivator.ShowHint("Жарится еда...");
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
                if (player.HeldItem == null)
                {
                    AttentionHintActivator.ShowHint("У тебя нет ничего в руках");
                    return;
                }

                var recipe = GetRecipe(player.HeldItem);

                if (recipe == null)
                {
                    AttentionHintActivator.ShowHint("Это не подходит для готовки тут ...");
                    return;
                }

                SetItemOnAppliance(player.HeldItem);
                player.ClearHands();
                return;
            }

            if (CurrentItem != null && State == TaskState.Idle)
            {
                TryStartProcess(player);
            }
            else if (State == TaskState.Completed || State == TaskState.Failed && IsReadyToTake)
            {
                if (player.HeldItem == null)
                    TakeResult(player);
                else
                    AttentionHintActivator.ShowHint("Освободи руки чтобы взять !");
            }
        }

        private void TryStartProcess(PlayerController player)
        {
            var recipe = GetRecipe(CurrentItem);

            if (recipe == null)
            {
                AttentionHintActivator.ShowHint("Это не подходит для готовки тут ...");
                return;
            }

            StartProcess(recipe);
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
    }
}