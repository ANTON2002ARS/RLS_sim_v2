using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O71 : AbstractBlock
{
    [Header("MultistateToggle and Action")]
    [SerializeField]
    private MultistateToggle our_phase;
    [SerializeField]
    private MultistateToggleAction _our_phase_action;

    [SerializeField]
    private MultistateToggle in_valtage;
    [SerializeField]
    private MultistateToggleAction _in_valtage_action;

    [SerializeField]
    private MultistateToggle scale;
    [SerializeField]
    private MultistateToggleAction _scale_action;

    [SerializeField]
    private string _strobModeName;

    [Header("Actions")]
    public ToggleAction _start_distation_action;
    public ToggleAction _pin_action;

    [Header("Triggers")]
    public Toggle start_distation;
    public Toggle pin;

    public void Of_Action_1(bool state) => TriggerEventInGM( _start_distation_action, state);
    public void Of_Action_2(bool state) => TriggerEventInGM(_pin_action, state);

    private void Start()
    {
        UpdateUI(false);
        our_phase.OnStateChange += Handle_1;
        in_valtage.OnStateChange += Handle_2;
        scale.OnStateChange += Handle_3;

        start_distation.OnToggle.AddListener(Of_Action_1);
        pin.OnToggle.AddListener(Of_Action_2);

    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            // «брасывание значение\\
            _our_phase_action.Reset();
            _in_valtage_action.Reset();
            _start_distation_action.Reset();

            _start_distation_action.currentState = _pin_action.DefaultState;
            _pin_action.currentState = _pin_action.DefaultState;
        }

        our_phase.SetStateNoEvent(_our_phase_action.CurrentState);
        in_valtage.SetStateNoEvent(_in_valtage_action.CurrentState);
        scale.SetStateNoEvent(_scale_action.CurrentState);
       
        start_distation.SetStateNoEvent(_start_distation_action.currentState);
        pin.SetStateNoEvent(_pin_action.currentState);
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }


    private void Handle_1(string state)
    {
        _our_phase_action.CurrentState = state;
        GameManager.Instance.AddToState(_our_phase_action);
    }

    private void Handle_2(string state)
    {
        _in_valtage_action.CurrentState = state;
        GameManager.Instance.AddToState(_in_valtage_action);
    }

    private void Handle_3(string state)
    {
        _scale_action.CurrentState = state;
        GameManager.Instance.AddToState(_scale_action);
    }
}
