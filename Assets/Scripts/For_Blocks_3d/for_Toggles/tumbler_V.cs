using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tumbler_V : MonoBehaviour
{
    [SerializeField] private switch_position first_action;
    [SerializeField] private float first_pos_z;
    [SerializeField] private UnityEvent action_of_first;
    [SerializeField] private switch_position second_action;
    [SerializeField] private float secont_pos_z;
    [SerializeField] private UnityEvent action_of_second;
    [SerializeField] private Transform lever; 

    private void Event_Action_to_first() => action_of_first?.Invoke();
    private void Event_Action_to_second() => action_of_second?.Invoke();
}
