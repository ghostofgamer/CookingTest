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

        private void FixedUpdate()
        {
            CheckOutline();
        }

        private void CheckOutline()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxDistance, _interactableLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

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