using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Siigment : MonoBehaviour
{
    [SerializeField] private Material off;
    [SerializeField] private Material on;

    [SerializeField] private GameObject Led_diod_a;
    [SerializeField] private GameObject Led_diod_b;
    [SerializeField] private GameObject Led_diod_c;
    [SerializeField] private GameObject Led_diod_d;
    [SerializeField] private GameObject Led_diod_e;
    [SerializeField] private GameObject Led_diod_f;
    [SerializeField] private GameObject Led_diod_g;
    [SerializeField] private GameObject Led_diod_point;

    

    public void Show_Number(int number, bool use_point)
    {
        switch (number)
        {
            case 0:
                set_0();
                break;
            case 1:
                set_1();
                break;
            case 2:
                set_2();
                break;
            case 3:
                set_3();
                break;
            case 4:
                set_4();
                break;
            case 5:
                set_5();
                break;
            case 6:
                set_6();
                break;
            case 7:
                set_7();
                break;
            case 8:
                set_8();
                break;
            case 9:
                set_9();
                break;
            default:
                Debug.LogError("Number is out rage");
                break;
        }
        if (use_point == true)
            On_Point();
        else
            Off_Point();
    }

    private void On_Point() => Led_diod_point.GetComponent<MeshRenderer>().sharedMaterial = on;
    private void Off_Point() => Led_diod_point.GetComponent<MeshRenderer>().sharedMaterial = off;

    private void set_0()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = off;        
    }

    private void set_1()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = off;       
    }

    private void set_2()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }
    private void set_3()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }

    private void set_4()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }

    private void set_5()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }

    private void set_6()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }
    private void set_7()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = off;        
    }
    private void set_8()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }

    private void set_9()
    {
        Led_diod_a.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_b.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_c.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_d.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_e.GetComponent<MeshRenderer>().sharedMaterial = off;
        Led_diod_f.GetComponent<MeshRenderer>().sharedMaterial = on;
        Led_diod_g.GetComponent<MeshRenderer>().sharedMaterial = on;        
    }  
}
