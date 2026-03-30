using Interfaces;
using ItemsContent;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _heldItemPos;
        [SerializeField] private Transform _cameraTransform;
        
        private IPlayerInput _input;
        private PlayerMotor _motor;
        private PlayerLook _look;
        private IInteractable _currentInteractable;
        
        public BaseItem HeldItem { get; private set; }

        private void Update()
        {
            _motor.Move(_input.Move);
            _look.Look(_input.Look);
            _motor.ApplyGravity();

            if (_input.JumpPressed)
                _motor.Jump();

            if (_input.InteractPressed)
                TryInteract();
            
            if (_input.DropPressed)
                DropItem();
        }

        public void SetInteractable(IInteractable interactable)
        {
            _currentInteractable = interactable;
        }

        public void Init(IPlayerInput input)
        {
            _input = input;
            _motor = GetComponent<PlayerMotor>();
            _look = GetComponent<PlayerLook>();
        }

        private void TryInteract()
        {
            if(_currentInteractable != null)
                _currentInteractable.Interact(this);
        }

        public void PickupItem(BaseItem item)
        {
            HeldItem = item;
            item.transform.SetParent(_heldItemPos);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            HeldItem.SetRBValueCollider(true);
        }

        private void DropItem()
        {
            if (HeldItem == null) return;

            BaseItem item = HeldItem;
            item.transform.SetParent(null);
            Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb != null)
            {
                HeldItem.SetValueCollider(true);
                HeldItem.SetRBValueCollider(false);
                Vector3 throwDir = _cameraTransform.forward + Vector3.up * 0.3f;
                rb.AddForce(throwDir * 5f, ForceMode.Impulse);
            }
            
            HeldItem = null;
        }

        public void ClearHands()
        {
            HeldItem = null;
        }
    }
}