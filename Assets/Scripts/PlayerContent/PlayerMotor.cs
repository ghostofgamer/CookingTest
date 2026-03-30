using UnityEngine;

namespace PlayerContent
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _fallMultiplier = 2.5f;

        private CharacterController _controller;
        private Vector3 _velocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void Move(Vector2 input)
        {
            Vector3 move = transform.right * input.x + transform.forward * input.y;
            _controller.Move(move * _speed * Time.deltaTime);
        }

        public void Jump()
        {
            if (_controller.isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        public void ApplyGravity()
        {
            if (!_controller.isGrounded)
            {
                if (_velocity.y < 0) // падаем вниз
                    _velocity.y += _gravity * _fallMultiplier * Time.deltaTime;
                else
                    _velocity.y += _gravity * Time.deltaTime;
            }
            else if (_velocity.y < 0)
            {
                _velocity.y = -2f; // чтобы CharacterController “прилипал” к земле
            }

            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}