using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Interference_warfare", menuName = "ScriptableObjects/Interference_warfare", order = 0)]
public class Interference_warfare : ScriptableObject
{
    public string Name;       

    [Header("Passive")]    
    public int Number_1;
    public Action[] Actions_Passive;

    [Header("Local")]    
    public int Number_2;
    public Action[] Actions_Local;

    [Header("NIP")]    
    public int Number_3;
    public Action[] Actions_NIP;

    [Header("ActiveNoise")]    
    public int Number_4;
    public Action[] Actions_ActiveNoise;

    [Header("Response")]    
    public int Number_5;
    public Action[] Actions_Response;

}
