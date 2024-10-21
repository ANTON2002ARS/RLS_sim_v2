using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POS72 : AbstractBlock
{
    [SerializeField]
    private MultistateToggle _workModeToggle;
    [SerializeField]
    private MultistateToggleAction _workModeAction;

    [SerializeField]
    private MultistateToggle _switch_canals_toggle;
    [SerializeField]
    private MultistateToggleAction _switch_canals_action;

    [SerializeField]
    private string _strobModeName;

    [Header("Triggers and Actions")]

    public Toggle_Button HighVoltageTrigger;
    public ToggleAction HighVoltage_Action;    

    public Toggle_Button PowerTrigger;
    public ToggleAction Power_Action;
    
    public Toggle RotationTrigger; 
    public ToggleAction Rotation_Action;

    public Toggle SpeedTrigger;
    public ToggleAction Speed_Action;

    public Toggle Blink;
    public ToggleAction Blink_Action;


    public void HighVoltageAction(bool state) => TriggerEventInGM(HighVoltage_Action, state);
    public void PowerAction(bool state) => TriggerEventInGM(Power_Action, state);
    public void RotationAction(bool state) => TriggerEventInGM(Rotation_Action, state);
    public void SpeedAction(bool state) => TriggerEventInGM(Speed_Action, state);
    public void BlinkAction(bool state) => TriggerEventInGM(Blink_Action, state);

    private void Start()
    {
        UpdateUI(false);
        _workModeToggle.OnStateChange += Handle_1;
        _switch_canals_toggle.OnStateChange += Handle_2;

        HighVoltageTrigger.OnToggle.AddListener(HighVoltageAction);
        PowerTrigger.OnToggle.AddListener(PowerAction);
        RotationTrigger.OnToggle.AddListener(RotationAction);
        SpeedTrigger.OnToggle.AddListener(SpeedAction);
        Blink.OnToggle.AddListener(BlinkAction);        
    }
   

    public override void UpdateUI(bool clearState)
    {
        if (clearState)
        {
            // Збрасывание значение\\
            _workModeAction.Reset();
            _switch_canals_action.Reset();

            HighVoltage_Action.currentState = HighVoltage_Action.DefaultState;
            Power_Action.currentState = Power_Action.DefaultState;
            Rotation_Action.currentState = Rotation_Action.DefaultState;
            Speed_Action.currentState = Speed_Action.DefaultState;
            Blink_Action.currentState = Blink_Action.DefaultState;
        }

        _workModeToggle.SetStateNoEvent(_workModeAction.CurrentState);
        _switch_canals_toggle.SetStateNoEvent(_switch_canals_action.CurrentState);
        HighVoltageTrigger.SetStateNoEvent(HighVoltage_Action.currentState);
        PowerTrigger.SetStateNoEvent(Power_Action.currentState);
        RotationTrigger.SetStateNoEvent(Rotation_Action.currentState);
        SpeedTrigger.SetStateNoEvent(Speed_Action.currentState);
        Blink.SetStateNoEvent(Blink_Action.currentState);
    }


    private void TriggerEventInGM(ToggleAction a, bool state)
    {
        a.currentState = state;
        if (a.ActionName == "Вскрыть крышку и включить режим мерцания")
        {
            IKO_Controll.Instance.Set_Mode_Frickering();
            return;
        }
            
        GameManager.Instance.AddToState(a);
    }


    private void Handle_1(string state)
    {
        _workModeAction.CurrentState = state;
        GameManager.Instance.AddToState(_workModeAction);
        /*if (state == _strobModeName)
            IkoController.Instance.EnableStrobControl();      
        else
            IkoController.Instance.DisableStrobControl();*/
    }


    private void Handle_2(string state)
    {
        _switch_canals_action.CurrentState = state;
        GameManager.Instance.AddToState(_switch_canals_action);
    }
}
