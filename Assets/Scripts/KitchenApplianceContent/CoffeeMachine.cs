using Cysharp.Threading.Tasks;
using PlayerContent;
using SOContent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenApplianceContent
{
    public class CoffeeMachine : KitchenAppliance
    {
        [SerializeField] private CoffeeMachineConfig _coffeeMachineConfig;
        [SerializeField] private Image _fillImage; // UI Fill сверху
        [SerializeField] private TMP_Text _statusText;
        [SerializeField]private CanvasGroup _canvasGroup;
        [SerializeField]private ParticleSystem _particleSystem;
        
        private bool _isBrewing = false;
        private float _timer = 0f;
        
        protected override void OnAction(PlayerController playerController)
        {
            if (_isBrewing)
            {
                AttentionContent.AttentionHintActivator.ShowHint("Кофе уже варится!");
                return;
            }

            StartBrewingAsync().Forget(); 
        }

        private async UniTaskVoid StartBrewingAsync()
        {
            _particleSystem.Play();
            _canvasGroup.alpha = 1f;
            _isBrewing = true;
            _fillImage.fillAmount = 0f;
            _statusText.text = "Варится ...";

            float timer = 0f;
            float brewTime = _coffeeMachineConfig.Duration;

            while (timer < brewTime)
            {
                timer += Time.deltaTime;
                _fillImage.fillAmount = Mathf.Clamp01(timer / brewTime);
                await UniTask.Yield();
            }

            _fillImage.fillAmount = 1f;
            _statusText.text = "Готово! Забирай кофе";
            _particleSystem.Stop();
            _isBrewing = false;
        }
    }
}