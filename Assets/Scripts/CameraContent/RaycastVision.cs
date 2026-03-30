using Interfaces;
using PlayerContent;
using UnityEngine;

namespace CameraContent
{
    public class RaycastVision : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableLayer;
        [SerializeField] private float _maxDistance;
        [SerializeField] private PlayerController _playerController;

        private IInteractable _currentInteractable;
        private Ray _ray;
        private  RaycastHit _hit; 

        private void FixedUpdate()
        {
            CheckOutline();
        }

        private void CheckOutline()
        {
            _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(_ray, out _hit, _maxDistance, _interactableLayer))
            {
                IInteractable interactable = _hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (_currentInteractable != interactable)
                    {
                        if (_currentInteractable != null)
                            _currentInteractable.DisableOutline();

                        _currentInteractable = interactable;
                        _currentInteractable.EnableOutline();
                        _playerController.SetInteractable(_currentInteractable);
                    }
                }
                else
                {
                    DisableCurrentOutline();
                    _playerController.SetInteractable(null);
                }
            }
            else
            {
                DisableCurrentOutline();
                _playerController.SetInteractable(null);
            }
        }

        private void DisableCurrentOutline()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.DisableOutline();
                _currentInteractable = null;
                _playerController.SetInteractable(null);
            }
        }
    }
}