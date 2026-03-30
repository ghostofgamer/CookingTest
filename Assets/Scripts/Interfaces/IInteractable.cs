using PlayerContent;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Interact(PlayerController player);
        public bool CanInteract(PlayerController player);
        public void EnableOutline();
        public void DisableOutline();
    }
}