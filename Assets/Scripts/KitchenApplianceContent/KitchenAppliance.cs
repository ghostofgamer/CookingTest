using PlayerContent;
using UnityEngine;

namespace KitchenApplianceContent
{
    public abstract class KitchenAppliance : MonoBehaviour
    {
        [SerializeField]private InteractableObject _interactableObject;

        private void OnEnable()
        {
            _interactableObject.Action += OnAction;
        }

        private void OnDisable()
        {
            _interactableObject.Action -= OnAction;
        }

        protected virtual void OnAction(PlayerController playerController)
        {
            Debug.Log("Запускаем ");
        } 
    }
}
