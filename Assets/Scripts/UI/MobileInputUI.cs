using InputContent;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MobileInputUI : MonoBehaviour
    {
        [SerializeField] private Button jumpButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private GameObject _joystick;
        [SerializeField] private Button _dropButton; 

        private MobileInput mobileInput;

        public void Init(MobileInput input)
        {
            mobileInput = input;

            jumpButton.onClick.AddListener(() => mobileInput.SetJump(true));
            interactButton.onClick.AddListener(() => mobileInput.SetInteract(true));
            _dropButton.onClick.AddListener(() => mobileInput.SetDrop(true));
        }

        public void Deactivate()
        {
            jumpButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            _joystick.gameObject.SetActive(false);
            _dropButton.gameObject.SetActive(false);
        }
    }
}