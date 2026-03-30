using System;
using HighlightPlus;
using Interfaces;
using PlayerContent;
using UnityEngine;

public class InteractableObject : MonoBehaviour,IInteractable
{
    [SerializeField] private HighlightEffect[] _highlightEffects;

    public event Action<PlayerController> Action;
    
    public void Interact(PlayerController player)
    {
        Action?.Invoke(player);
        Debug.Log("Interacting with player " + gameObject.name);
    }

    public bool CanInteract(PlayerController player)
    {
        return false;
    }

    public void EnableOutline()
    {
        if (_highlightEffects.Length > 0)
            foreach (var effect in _highlightEffects)
                effect.enabled = true;
    }

    public void DisableOutline()
    {
        if (_highlightEffects.Length > 0)
            foreach (var effect in _highlightEffects)
                effect.enabled = false;
    }
}