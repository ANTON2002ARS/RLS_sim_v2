using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Trace_of_Target : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> Canvas;
    public GameObject Line_Group;
    public GameObject TaiL;
    void Start()
    {
        // if(TaiL != null)
        //     TaiL.SetActive(false);
        Turn_on_IKO();
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Line" )
        {
            foreach(var image in Canvas){
                Color color = image.color;
                color.a -=0.2f;
                if(color.a < 0.1f)
                    Destroy(this.gameObject);
                image.color = color;
            }              
        }
    }   

    private void Turn_on_IKO()
    {        
        this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, this.transform.localPosition) + 180);        
    }

    public void Use_Group()=> Line_Group.SetActive(true); 

    public void Show_tail(){
        
        if(TaiL != null){
            TaiL.SetActive(true);
            Debug.Log("SHOW TAIL");
        }
            
    }
}
