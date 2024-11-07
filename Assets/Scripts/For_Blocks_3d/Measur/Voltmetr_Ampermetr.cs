using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voltmetr_Ampermetr : Abst_Measurs
{
    [SerializeField] private bool is_voltmert;
    [Header("for object")]
    [SerializeField] private GameObject Line;
    [SerializeField] private GameObject Text_0_300;
    [SerializeField] private GameObject Text_0_1;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject V;

    private void Start() => Is_Voltmetr();   


    public void Set_Position(float valum_measurs)
    {
        if (is_voltmert)
            Voltmetr(valum_measurs);
        else
            Ampermetr(valum_measurs);        
    }

    private void Voltmetr(float valum_measurs)
    {
        float outpun_value = Mathf.Lerp(-70f, 70f, valum_measurs / 300f);
        Debug.Log("Вольтмерт в позицию: " + valum_measurs + " угол:" + outpun_value);    
        Line.transform.rotation = Quaternion.Euler(0f, 0f, outpun_value);
    }

    private void Ampermetr(float valum_measurs)
    {
        float outpun_value = Mathf.Lerp(0f, 1f, valum_measurs / 300f);
        Debug.Log("Ампермерт в позицию: " + valum_measurs + " угол:" + outpun_value);
        Line.transform.rotation = Quaternion.Euler(0f, 0f, outpun_value);        
    }

    public void Is_Voltmetr()
    {
        if (is_voltmert)
        {
            Text_0_300.SetActive(true);
            V.SetActive(true);
            Text_0_1.SetActive(false);
            A.SetActive(false);
        }
        else
        {
            Text_0_1.SetActive(true);
            A.SetActive(true);
            Text_0_300.SetActive(false);
            V.SetActive(false);
        }
    }
}
