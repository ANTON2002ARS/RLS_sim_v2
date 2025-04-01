using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vector_Click : MonoBehaviour
{
    [SerializeField] UnityEvent Ckick;
    private void OnMouseUpAsButton()=>Ckick.Invoke();    
}
