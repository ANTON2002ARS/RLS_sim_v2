using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Door : MonoBehaviour
{
    [SerializeField] bool is_open;
    private Animator animator;  

    void Start()
    {
        is_open = false;
        animator = this.GetComponent<Animator>();
    }
    public void Change_State()
    {        
        if(is_open == false)
        {
            is_open = true;
            Open_Door();
            Debug.Log("door is open");
        }
        else
        {
            is_open = false;
            Close_Door();
            Debug.Log("door is close");
        }
    }

    private void Open_Door() => animator.SetTrigger("open");
    
    private void Close_Door() => animator.SetTrigger("close");
    
}
