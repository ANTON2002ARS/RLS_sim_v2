using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammeter : Abst_Measurs
{
    [SerializeField] private Transform Line;
    [SerializeField] private GameObject text_measur;
<<<<<<< HEAD
=======
    [SerializeField] private float max_measurs;
>>>>>>> Test_building
    [SerializeField] private GameObject text;
    [SerializeField] private float min_angle;
    [SerializeField] private float max_angle;

    public override void Replace_valum(float valum_measurs)
    {
<<<<<<< HEAD
        float outpun_value = Mathf.Lerp(min_angle, max_angle, valum_measurs / 300f);
        Debug.Log("Ампермерт в позицию: " + valum_measurs + " угол:" + outpun_value);
=======
        float outpun_value = Mathf.Lerp(min_angle, max_angle, valum_measurs / max_measurs);
        Debug.Log("пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ: " + valum_measurs + " пїЅпїЅпїЅпїЅ:" + outpun_value);
>>>>>>> Test_building
        Line.rotation = Quaternion.Euler(0f, 0f, outpun_value);
    }
}
