using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BI71 : AbstractBlock
{
    [Header("MultistateToggle and Action")]
    [SerializeField]
    private MultistateToggle Controll_Voltage;
    [SerializeField]
    private MultistateToggleAction Controll_Voltage_action;  

    [SerializeField]
    private string _strobModeName;

    [Header("Triggers and Actions")]    
    public Toggle Anode;
    public ToggleAction Anode_action;
    
    public Toggle Intensity;
    public ToggleAction Intensity_action;

    public void Of_Action_1(bool state) => TriggerEventInGM(Anode_action, state);
    public void Of_Action_2(bool state) => TriggerEventInGM(Intensity_action, state);

    private void Start()
    {
        UpdateUI(false);
        Controll_Voltage.OnStateChange += Handle_1;

        Anode.OnToggle.AddListener(Of_Action_1);
        Intensity.OnToggle.AddListener(Of_Action_2);

    }

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            // «брасывание значение\\
            Controll_Voltage_action.Reset();

            Anode_action.currentState = Anode_action.DefaultState;
            Intensity_action.currentState = Intensity_action.DefaultState;
        }

        Controll_Voltage.SetStateNoEvent(Controll_Voltage_action.CurrentState);

        Anode.SetStateNoEvent(Anode_action.currentState);
        Intensity.SetStateNoEvent(Intensity_action.currentState);
    }

    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        GameManager.Instance.AddToState(a);
    }


    private void Handle_1(string state)
    {
        Controll_Voltage_action.CurrentState = state;
        GameManager.Instance.AddToState(Controll_Voltage_action);
    }
   
}
