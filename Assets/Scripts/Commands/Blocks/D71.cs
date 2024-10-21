using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D71 : AbstractBlock
{
    [Header("MultistateToggle and Action")]
    [SerializeField]
    private MultistateToggle Switch_Controll;
    [SerializeField]
    private MultistateToggleAction Switch_Controll_action;
        

    [SerializeField]
    private string _strobModeName;

    [Header("Triggers and Actions")] 

    public Toggle Trigger_manager_start_I_II;   
    public ToggleAction Trigger_manager_start_I_II_action; 
            

    public void Of_Action(bool state) => TriggerEventInGM(Trigger_manager_start_I_II_action, state);


    private void Start()
    {
        UpdateUI(false);
        Switch_Controll.OnStateChange += Handle_1;
        Trigger_manager_start_I_II.OnToggle.AddListener(Of_Action);
    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            Switch_Controll_action.Reset();
            Trigger_manager_start_I_II_action.currentState = Trigger_manager_start_I_II_action.DefaultState;
        }

        Switch_Controll.SetStateNoEvent(Switch_Controll_action.CurrentState);
        Trigger_manager_start_I_II.SetStateNoEvent(Trigger_manager_start_I_II_action.currentState);        
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }

    private void Handle_1(string state)
    {
        Switch_Controll_action.CurrentState = state;
        GameManager.Instance.AddToState(Switch_Controll_action);
    }   
}
