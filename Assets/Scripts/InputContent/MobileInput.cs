using Interfaces;
using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

namespace InputContent
{
    public class MobileInput : IPlayerInput
    {
        private Joystick _moveJoystick;
        private LookAreaInput _lookInput;

        private Vector2 _lastTouchPos;
        private Vector2 _lastPointerPos;
        private bool _isTouching;
        private float _lookSensitivity = 0.2f;

        private bool _interactPressed;
        private bool _jumpPressed;

        public MobileInput(Joystick moveJoystick, LookAreaInput lookInput)
        {
            _moveJoystick = moveJoystick;
            _lookInput = lookInput;
            Debug.Log($"MobileInput created. LookArea: {_lookInput}, MoveJoystick: {_moveJoystick}");
        }

        public Vector2 Move => _moveJoystick != null ? _moveJoystick.Direction : Vector2.zero;
        public Vector2 Look => _lookInput != null ? _lookInput.Delta * _lookSensitivity : Vector2.zero;

        public bool JumpPressed
        {
            get
            {
                if (_jumpPressed)
                {
                    _jumpPressed = false;
                    return true;
                }

                return false;
            }
        }

        private bool _dropPressed;

        public bool DropPressed
        {
            get
            {
                if (_dropPressed)
                {
                    _dropPressed = false;
                    return true;
                }

                return false;
            }
        }

        public bool InteractPressed
        {
            get
            {
                if (_interactPressed)
                {
                    _interactPressed = false;
                    return true;
                }

                return false;
            }
        }

        public void SetJump(bool pressed) => _jumpPressed = pressed;
        public void SetInteract(bool pressed) => _interactPressed = pressed;

        public void SetDrop(bool pressed)
        {
            _dropPressed = pressed;
        }
    }
}