using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Trace_of_Target : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> Canvas;
    public GameObject Use_Group;
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Line" )
        {
            foreach(var image in Canvas){
                image.alpha -= 0.2f;
                if(image.alpha <0.1f){
                    Destroy(this.gameObject);
                }
            }   
            Debug.Log("Marge line");
        }
    }   

    public void Set_Group()=> Use_Group.SetActive(true); 
}
