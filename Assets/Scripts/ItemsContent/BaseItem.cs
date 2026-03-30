using PlayerContent;
using SOContent;
using UnityEngine;

namespace ItemsContent
{
    public class BaseItem : MonoBehaviour
    {
        [SerializeField] private InteractableObject _interactableObject;
        [SerializeField] private Collider[] _colliders;
        [SerializeField]private Rigidbody _rigidbody;
        
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

        public void SetValueCollider(bool value)
        {
            foreach (var collider in _colliders)
                collider.enabled = value;
        }

        public void SetRBValueCollider(bool value)
        {
            _rigidbody.isKinematic = value;
        }
    }
}