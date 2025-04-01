using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domain : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
   
    void Start() => image.gameObject.SetActive(false);    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag != "Line")
            return;  
        
        image.gameObject.SetActive(true);

        if(GetComponentInParent<Body_Interference>() != null && GetComponentInParent<Body_Interference>().End_Work == true)
        {
            Color color =  image.color;
            color.a -=0.2f;            
            if(color.a < 0.01f)
                GetComponentInParent<Body_Interference>().Delete(); 
            image.color = color;    
        }
        
    }    
    
}
