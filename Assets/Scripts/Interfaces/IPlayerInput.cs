using UnityEngine;

namespace Interfaces
{
    public interface IPlayerInput
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        bool InteractPressed { get; }
        bool JumpPressed { get; }
        bool DropPressed  { get; }
    }
}