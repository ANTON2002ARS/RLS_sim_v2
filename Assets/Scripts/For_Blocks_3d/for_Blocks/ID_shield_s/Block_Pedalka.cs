using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block_Pedalka : MonoBehaviour
{
    [SerializeField] GameObject axis_pedalka;
    public UnityEvent jump_foot;
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void OnMouseUpAsButton()
    {
        animator.SetTrigger("jump");
        jump_foot.Invoke();
    }
}
