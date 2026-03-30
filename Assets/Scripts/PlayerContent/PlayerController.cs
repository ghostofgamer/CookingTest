using InputContent;
using Interfaces;
using ItemsContent;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _heldItemPos;
        [SerializeField] private Transform _cameraTransform;

        public BaseItem HeldItem { get; private set; }

        private IPlayerInput _input;
        private PlayerMotor _motor;
        private PlayerLook _look;
        
        private IInteractable _currentInteractable;

        private void Update()
        {
            if (_input is MobileInput mobile)
                mobile.UpdateInput();

            _motor.Move(_input.Move);
            _look.Look(_input.Look);

            _motor.ApplyGravity();

            if (_input.JumpPressed)
                _motor.Jump();

            if (_input.InteractPressed)
            {
                Debug.Log("Interact!");
                TryInteract();
            }
            
            if (_input.DropPressed)
            {
                Debug.Log("Drop!");
                DropItem();
            }
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

            Rigidbody rb = item.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        public void DropItem()
        {
            if (HeldItem == null) return;

            BaseItem item = HeldItem;
            item.transform.SetParent(null);

            Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;
                Vector3 throwDir = _cameraTransform.forward + Vector3.up * 0.3f;
                rb.AddForce(throwDir * 5f, ForceMode.Impulse);
            }

            HeldItem = null;
        }

        public void PlaceItem(IInteractable target)
        {
            if (HeldItem != null && target.CanInteract(this))
            {
                target.Interact(this);
                HeldItem = null;
            }
        }
    }
}