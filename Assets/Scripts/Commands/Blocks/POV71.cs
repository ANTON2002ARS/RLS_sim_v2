using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POV71 : AbstractBlock
{
    [Header("Actions")]
    public ToggleAction _light_action;
    public ToggleAction _Peling_action;
    

    [Header("Triggers")]
    public Toggle light_toggle;
    public Toggle Peling_toggle;    


    public void HighVoltageAction(bool state) => TriggerEventInGM(_light_action, state);
    public void PowerAction(bool state) => TriggerEventInGM(_Peling_action, state);
    

    private void Start()
    {
        UpdateUI(false);

        light_toggle.OnToggle.AddListener(HighVoltageAction);
        Peling_toggle.OnToggle.AddListener(PowerAction);        
    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            _light_action.currentState = _light_action.DefaultState;
            _Peling_action.currentState = _Peling_action.DefaultState;
        }

        light_toggle.SetStateNoEvent(_light_action.currentState);
        Peling_toggle.SetStateNoEvent(_Peling_action.currentState);
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }


    /*private void HandleWorkMode(string state)
    {
        _workModeAction.CurrentState = state;
        _switch_canals_action.CurrentState = state;
        
        GameManager.Instance.AddToState(_workModeAction);
        GameManager.Instance.AddToState(_switch_canals_action);
    }*/
}
