using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B70 : AbstractBlock
{
    [Header("MultistateToggle and Action")]
    [SerializeField]
    private MultistateToggle Switch_Work_I_II;
    [SerializeField]
    private MultistateToggleAction Switch_Work_I_II_action;

    [SerializeField]
    private MultistateToggle Switch_compensation;
    [SerializeField]
    private MultistateToggleAction Switch_compensation_action;

    private void Start()
    {
        UpdateUI(false);
        Switch_Work_I_II.OnStateChange += Handle_1;
        Switch_compensation.OnStateChange += Handle_2; 
    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            Switch_Work_I_II_action.Reset();
            Switch_compensation_action.Reset();            
        }

        Switch_Work_I_II.SetStateNoEvent(Switch_Work_I_II_action.CurrentState);
        Switch_compensation.SetStateNoEvent(Switch_compensation_action.CurrentState);
        
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }


    private void Handle_1(string state)
    {
        Switch_Work_I_II_action.CurrentState = state;
        GameManager.Instance.AddToState(Switch_Work_I_II_action);
    }

    private void Handle_2(string state)
    {
        Switch_compensation_action.CurrentState = state;
        GameManager.Instance.AddToState(Switch_compensation_action);
    }    
}
