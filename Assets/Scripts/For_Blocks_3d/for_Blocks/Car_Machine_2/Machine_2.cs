using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Machine_2 : MonoBehaviour
{
    [SerializeField] private Camera_Controller controller_game;

    public void Show_Text_Center(string text)
    {
        controller_game.Show_Text_Center(text);
    }

    public void Show_Text_Panel(string text)
    {
        controller_game.Show_Text_Panel(text);
    }

    public void Use_Panel(bool show)
    {
        controller_game.Use_Panel(show);
    }


}
