using System.Collections.Generic;
using AttentionContent;
using PlayerContent;
using Serializible;
using UnityEngine;

namespace KitchenApplianceContent
{
    public class FryingPan : KitchenAppliance
    {
        [SerializeField] private List<CookingRecipe> _recipes;

        protected override void OnAction(PlayerController playerController)
        {
            var item = playerController.HeldItem;
            
            if (item == null)
            {
                AttentionHintActivator.ShowHint("У тебя пусто в руках");
                return;
            }

            var recipe = _recipes.Find(r => r.ItemType == item.Data.Type);
            
            if (recipe == null)
            {
                AttentionHintActivator.ShowHint("Этот предмет нельзя жарить");
                return;
            }

            StartCooking(recipe);
        }

        private void StartCooking(CookingRecipe recipe)
        {
            Debug.Log("FryingPan");
        }
    }
}