using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DTP_block : MonoBehaviour
{
    [SerializeField] private TextMeshPro text_up;
    [SerializeField] private TextMeshPro text_down;
    [SerializeField] private counter_valum counter;

    void Start()
    {
        Show_Number_UP(Random.Range(10,999));
        Show_Number_DOWN(Random.Range(10,999));
    }

    public void Show_Number_UP(int number){
        if(number < 0 || number > 999)
            return;
        string str = number.ToString();    
        string.Join(".", str.ToCharArray());
        text_up.text = str;
    }

    public void Show_Number_DOWN(int number){
        if(number < 0 || number > 999)
            return;
        string str = number.ToString();    
        string.Join(".", str.ToCharArray());
        text_down.text = str;
    }

    public void STOP_COUNTING() => counter.STOP_COUNTING();
    public void START_COUNTING() => counter.START_COUNTING();
}
