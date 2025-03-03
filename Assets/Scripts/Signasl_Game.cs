using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Signasl_Game : MonoBehaviour
{
    [SerializeField] private O71_block O71;


    public void Exit_Scene()
    {
        SceneManager.LoadScene("Menu_Scene");
        //MenuManager.Menu_Instance.Active_Task = null;
    }
    
}
