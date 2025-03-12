using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class One_Sigment
{
    public GameObject Sigment;
    public List<GameObject> one_diot;
}

public class Sigment_Display : MonoBehaviour
{
    [SerializeField] private Material Off;
    [SerializeField] private Material On;
    [SerializeField] private List<One_Sigment> sigment;

    public void Set_Number_1(int number) {

    }
    public void Set_Number_2(int number) { }
    public void Set_Number_3(int number) { }





}
