using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampokhkoy : MonoBehaviour
{
    [SerializeField] private Material material_off;
    [SerializeField] private Material material_on;
    [SerializeField] private MeshRenderer blenk;
    private void Start() => Change_State(false);
        
   
    public void Change_State(bool is_on)
    {       
        if (is_on)
        {
            if(material_on == null)
            {
                Debug.LogError("material on is null");
                return;
            }
            blenk.sharedMaterial = material_on;
        }
        else
        {
            if(material_off == null)
            {
                Debug.LogError("material off is null");
                return;
            }
            blenk.sharedMaterial = material_off;
        }
    }
}
