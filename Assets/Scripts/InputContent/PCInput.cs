using Interfaces;
using UnityEngine;

namespace InputContent
{
    public class PCInput : IPlayerInput
    {
        private InputSystem_Actions _inputActions;
        
        private Vector2 _move;
        private Vector2 _look;
        private bool _interact;
        private bool _jump;
        private bool _drop;
        
        public PCInput()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Enable();
            _inputActions.Player.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += ctx => _move = Vector2.zero;
            _inputActions.Player.Look.performed += ctx => _look = ctx.ReadValue<Vector2>();
            _inputActions.Player.Look.canceled += ctx => _look = Vector2.zero;
            _inputActions.Player.Interact.started += ctx => _interact = true;
            _inputActions.Player.Jump.performed += ctx => _jump = true;
            _inputActions.Player.Drop.started += ctx => _drop = true;
        }
        
        
        public Vector2 Move => _move;
        public Vector2 Look => _look;

        public bool InteractPressed
        {
            get
            {
                if (_interact)
                {
                    _interact = false;
                    return true;
                }
                return false;
            }
        }
        
        public bool JumpPressed
        {
            get
            {
                if (_jump)
                {
                    _jump = false;
                    return true;
                }
                return false;
            }
        }
        
        public bool DropPressed
        {
            get
            {
                if (_drop)
                {
                    _drop = false;
                    return true;
                }
                return false;
            }
        }
    }
}