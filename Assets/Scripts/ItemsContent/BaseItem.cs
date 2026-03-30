using PlayerContent;
using SOContent;
using UnityEngine;

namespace ItemsContent
{
    public class BaseItem : MonoBehaviour
    {
        [SerializeField] private InteractableObject _interactableObject;

        [field: SerializeField] public ItemData Data { get; private set; }

        private void OnEnable()
        {
            _interactableObject.Action += OnAction;
        }

        private void OnDisable()
        {
            _interactableObject.Action -= OnAction;
        }

        private void OnAction(PlayerController playerController)
        {
            if (playerController.HeldItem == null)
                playerController.PickupItem(this);
        }
    }
}