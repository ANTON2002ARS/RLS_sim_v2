using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potansiometr : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start() => animator = this.GetComponent<Animator>();
    public void Turning()=> animator.SetTrigger("purning");
}
