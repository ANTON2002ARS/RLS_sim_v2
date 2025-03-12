using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammeter : Abst_Measurs
{
    [SerializeField] private Transform Line;
    [SerializeField] private GameObject text_measur;
    [SerializeField] private float max_measurs;
    [SerializeField] private GameObject text;
    [SerializeField] private float min_angle;
    [SerializeField] private float max_angle;

    public override void Replace_valum(float valum_measurs)
    {
        float outpun_value = Mathf.Lerp(min_angle, max_angle, valum_measurs / max_measurs);
        Debug.Log("��������� � �������: " + valum_measurs + " ����:" + outpun_value);
        Line.rotation = Quaternion.Euler(0f, 0f, outpun_value);
    }
}
