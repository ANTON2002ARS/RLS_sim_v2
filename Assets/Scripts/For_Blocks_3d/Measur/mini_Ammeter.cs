using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_Ammeter : Abst_Measurs
{
    [SerializeField] private Transform Line;
    [SerializeField] private GameObject text_measur;
    [SerializeField] private GameObject text;
    [SerializeField] private float min_angle;
    [SerializeField] private float max_angle;

    private void Start()
    {
        text_measur.SetActive(true);
        text.SetActive(true);
    }

    public override void Replace_valum(float valum_measurs)
    {
        float outpun_value = Mathf.Lerp(min_angle, max_angle, valum_measurs / 300f);
        Debug.Log("Ампермерт в позицию: " + valum_measurs + " угол:" + outpun_value);
        Line.rotation = Quaternion.Euler(0f, 0f, outpun_value);
    }
}
