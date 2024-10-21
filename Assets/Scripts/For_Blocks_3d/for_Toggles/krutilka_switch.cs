using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position_krutilka
{
    public float of_Blocks;
    public switch_position Action_sw;
}

public class krutilka_switch : MonoBehaviour
{
    [SerializeField] private GameObject krutilka;
    [SerializeField] private List<Position_krutilka> list_switch;    
    [SerializeField] private int index_ckicks;
}
