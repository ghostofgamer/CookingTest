using UnityEngine;

namespace PlayerContent
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private float _sensitivity = 2f;

        private float _xRotation = 0f;

        public void Look(Vector2 input)
        {
            float mouseX = input.x * _sensitivity;
            float mouseY = input.y * _sensitivity;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

            _cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}