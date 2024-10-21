using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
//public class BoolEvent_Button : UnityEvent<bool> { }

public class Toggle_Button : MonoBehaviour
{
    public GameObject B_On;
    public GameObject B_Off;

    public BoolEvent OnToggle;

    private bool _isToggledOn;
    public bool IsToggledOn
    {
        get => _isToggledOn;
        set {
            SetStateNoEvent(value);
            OnToggle?.Invoke(value);
        }
    }

    public void SetStateNoEvent(bool value)
    {
        _isToggledOn = value;
        //StateOn.SetActive(value);
        //StateOff.SetActive(!value);
    }

    public void ToggleState(bool _on)
    {
        IsToggledOn = _on;
        //IsToggledOn = !IsToggledOn;
    }

    private void Awake()
    {
        OnToggle = new BoolEvent();       
    }
    
    void Start()
    {
        SetStateNoEvent(false);
    }
}
