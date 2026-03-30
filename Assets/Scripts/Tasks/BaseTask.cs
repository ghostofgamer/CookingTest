using System;
using Enum;
using UnityEngine;

public abstract class BaseTask : MonoBehaviour
{
    [SerializeField]private float WarningTime; // когда менять на Warning
    [SerializeField]private float Duration; // полное время до Done/Error

    public event Action<TaskState> OnStateChanged;
    
    public TaskState CurrentState { get; protected set; } = TaskState.Idle;
    public float Timer { get; protected set; }
    
    public abstract void StartTask();
    public abstract void CancelTask();
    public abstract void UpdateTask(float deltaTime);
    
    protected void SetState(TaskState state)
    {
        CurrentState = state;
        OnStateChanged?.Invoke(state);
    }
}