using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

namespace InputContent
{
    public class MobileInput : IPlayerInput
    {
        private Joystick _moveJoystick;
        private RectTransform _lookArea;

        private Vector2 _look;
        private Vector2 _lastTouchPos;
        private bool _isTouching;
        private float _lookSensitivity = 0.2f;

        private bool _interactPressed;
        private bool _jumpPressed;

        public MobileInput(Joystick moveJoystick, RectTransform lookArea)
        {
            _moveJoystick = moveJoystick;
            _lookArea = lookArea;
            Debug.Log($"MobileInput created. LookArea: {_lookArea}, MoveJoystick: {_moveJoystick}");
        }

        public Vector2 Move => _moveJoystick != null ? _moveJoystick.Direction : Vector2.zero;
        public Vector2 Look => _look;

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
        
        public void UpdateInput()
        {
            _look = Vector2.zero;

            Vector2 pointerPos = Vector2.zero;
            bool isPressed = false;

#if UNITY_EDITOR
            // мышь через Input System
            var mouse = UnityEngine.InputSystem.Mouse.current;
            if (mouse != null && mouse.leftButton.isPressed)
            {
                pointerPos = mouse.position.ReadValue();
                isPressed = true;
            }
#else
    // мобильный ввод
    if (UnityEngine.Input.touchCount > 0)
    {
        Touch touch = UnityEngine.Input.touches[0];
        pointerPos = touch.position;
        isPressed =
 touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary;
    }
#endif

            if (isPressed && _lookArea != null &&
                RectTransformUtility.RectangleContainsScreenPoint(_lookArea, pointerPos))
            {
                if (_isTouching)
                {
                    Vector2 delta = pointerPos - _lastTouchPos;
                    _look = delta * _lookSensitivity;
                }

                _lastTouchPos = pointerPos;
                _isTouching = true;
            }
            else
            {
                _isTouching = false;
                _look = Vector2.zero;
            }
        }
    }
}