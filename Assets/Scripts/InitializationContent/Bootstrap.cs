using AttentionContent;
using Cysharp.Threading.Tasks;
using InputContent;
using Interfaces;
using PlayerContent;
using UI;
using UI.Screens;
using UnityEngine;

namespace InitializationContent
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private MobileInputUI _mobileInputUI;
        [SerializeField] private AttentionHintViewer _attentionHintViewer;
        [SerializeField]private LookAreaInput _lookAreaInput;
        
        private IPlayerInput _input;
        private PlayerController _playerController;

        private void Awake()
        {
            Initialization();
        }

        private async UniTask Initialization()
        {
            _loadingScreen.Show();
            await _loadingScreen.AnimateSlider(0.5f, 0.5f);
            AttentionHintActivator.Init(_attentionHintViewer);
            InitInput();
            await _loadingScreen.AnimateSlider(1f, 0.5f);
            InitPlayer();
            _loadingScreen.Hide();
        }

        private void InitInput()
        {
            var factory = new InputFactory();
            _input = factory.Create(_moveJoystick,_lookAreaInput); // должно возвращать MobileInput

            if (_input is MobileInput mobileInput)
                _mobileInputUI.Init(mobileInput);
            else
                _mobileInputUI.Deactivate();
        }

        private void InitPlayer()
        {
            var playerGO = Instantiate(_playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            _playerController = playerGO.GetComponent<PlayerController>();
            _playerController.Init(_input);
        }
    }
}