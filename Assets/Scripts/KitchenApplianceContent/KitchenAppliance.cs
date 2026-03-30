using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enum;
using ItemsContent;
using PlayerContent;
using Serializible;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenApplianceContent
{
    public abstract class KitchenAppliance : MonoBehaviour
    {
        [SerializeField] private InteractableObject _interactableObject;
        [SerializeField] protected List<CookingRecipe> _recipes;
        [SerializeField] protected Image _fillImage;
        [SerializeField] protected TMP_Text _statusText;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected ParticleSystem _particleSystem;

        [field: SerializeField] public Transform ItemPosition { get; private set; }
        
        protected BaseItem CurrentItem;
        protected CookingRecipe CurrentRecipe;
        protected TaskState State = TaskState.Idle;
        protected CancellationTokenSource Cts;
        protected bool IsReadyToTake = false;
        
        private void OnEnable()
        {
            _interactableObject.Action += OnAction;
        }

        private void OnDisable()
        {
            _interactableObject.Action -= OnAction;
        }

        protected CookingRecipe GetRecipe(BaseItem item)
        {
            return _recipes.Find(r => r.ItemType == item.Data.Type);
        }

        protected virtual void SetItemOnAppliance(BaseItem item)
        {
            CurrentItem = item;
            CurrentItem.SetRBValueCollider(true);
            CurrentItem.gameObject.transform.SetParent(ItemPosition);
            CurrentItem.gameObject.transform.localPosition = Vector3.zero;
            CurrentItem.gameObject.transform.localRotation = Quaternion.identity;
            CurrentItem.SetValueCollider(false);
        }

        protected abstract void OnAction(PlayerController player);

        protected void StartProcess(CookingRecipe recipe)
        {
            CurrentRecipe = recipe;
            Cts?.Cancel();
            Cts = new CancellationTokenSource();

            RunProcess(Cts.Token).Forget();
        }

        protected void StopProcess()
        {
            Cts?.Cancel();
            Cts = null;

            if (State == TaskState.Completed)
                ReplaceItemPrefab(CurrentRecipe.ResultPrefab);
            else if (State == TaskState.Failed)
                ReplaceItemPrefab(CurrentRecipe.FiledPrefab);

            _particleSystem?.Stop();
            _canvasGroup.alpha = 0f;
            _fillImage.fillAmount = 0f;
        }

        protected void TakeResult(PlayerController player)
        {
            if (CurrentItem == null) return;

            player.PickupItem(CurrentItem);
            CurrentItem = null;
            CurrentRecipe = null;
            State = TaskState.Idle;
            IsReadyToTake = false;
        }

        private void ReplaceItemPrefab(BaseItem prefab)
        {
            if (CurrentItem == null || prefab == null) return;

            Vector3 pos = CurrentItem.transform.position;
            Quaternion rot = CurrentItem.transform.rotation;

            Destroy(CurrentItem.gameObject);
            CurrentItem = Instantiate(prefab, pos, rot, ItemPosition);
            CurrentItem.SetRBValueCollider(true);
            CurrentItem.SetValueCollider(false);
        }

        private async UniTaskVoid RunProcess(CancellationToken token)
        {
            State = TaskState.InProgress;
            _canvasGroup.alpha = 1f;
            _particleSystem?.Play();
            _fillImage.fillAmount = 0f;

            float startTime = Time.time;

            while (true)
            {
                float elapsed = Time.time - startTime;
                float progress = 0f;

                if (elapsed < CurrentRecipe.WarningTime)
                {
                    State = TaskState.InProgress;
                    progress = Mathf.InverseLerp(0f, CurrentRecipe.WarningTime, elapsed);
                    _statusText.text = CurrentRecipe.StartText;
                    SetVisual(Color.yellow, false);
                }
                else if (elapsed < CurrentRecipe.Duration)
                {
                    State = TaskState.Warning;
                    progress = Mathf.InverseLerp(CurrentRecipe.WarningTime, CurrentRecipe.Duration, elapsed);
                    _statusText.text = CurrentRecipe.WarningText;
                    SetVisual(Color.green, false);
                }
                else if (elapsed < CurrentRecipe.FailTime)
                {
                    State = TaskState.Completed;
                    progress = Mathf.InverseLerp(CurrentRecipe.Duration, CurrentRecipe.FailTime, elapsed);
                    _statusText.text = CurrentRecipe.DoneText;
                    SetVisual(Color.red, true);
                }
                else
                {
                    State = TaskState.Failed;
                    progress = 1f;
                    _statusText.text = CurrentRecipe.FailText;
                }

                _fillImage.fillAmount = progress;

                await UniTask.Yield(token);
            }
        }
        
        private void SetVisual(Color color, bool pulse)
        {
            _fillImage.color = color;

            if (pulse)
            {
                float pulseValue = Mathf.PingPong(Time.time * 3f, 1f);
                float scale = 1f + pulseValue * 0.15f;
                _canvasGroup.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                _canvasGroup.transform.localScale = Vector3.one;
            }
        }
    }
}