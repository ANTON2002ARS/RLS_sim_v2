using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K71 : AbstractBlock
{
    [SerializeField]
    private MultistateToggle _switch_mode_toggle;

    
    [SerializeField]
    private MultistateToggleAction _switch_mode_action;

    /*[SerializeField]
    private ToggleAction _switch_mode_action;*/


    [Header("Actions")]
    public ToggleAction focal_dist_action;
    

    [Header("Triggers")]
    public Toggle focal_dist_toggle;
    public void HighVoltageAction(bool state) => TriggerEventInGM(focal_dist_action, state);

    private void Start()
    {
        UpdateUI(false);
        focal_dist_toggle.OnToggle.AddListener(HighVoltageAction);
        _switch_mode_toggle.OnStateChange += HandleWorkMode;        
    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            _switch_mode_action.Reset();
            focal_dist_action.currentState = focal_dist_action.DefaultState;
        }

        _switch_mode_toggle.SetStateNoEvent(_switch_mode_action.CurrentState);        
        focal_dist_toggle.SetStateNoEvent(focal_dist_action.currentState);
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }

    private void HandleWorkMode(string state)
    {
        _switch_mode_action.CurrentState = state;        
        GameManager.Instance.AddToState(_switch_mode_action);
    }
}
