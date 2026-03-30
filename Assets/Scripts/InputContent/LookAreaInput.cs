using UnityEngine;
using UnityEngine.EventSystems;

namespace InputContent
{
    public class LookAreaInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Vector2 Delta { get; private set; }

        private Vector2 _lastPos;
        private bool _dragging;

        private void LateUpdate()
        {
            Delta = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _lastPos = eventData.position;
            _dragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_dragging) return;

            Vector2 current = eventData.position;
            Delta = current - _lastPos;
            _lastPos = current;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
            Delta = Vector2.zero;
        }
    }
}