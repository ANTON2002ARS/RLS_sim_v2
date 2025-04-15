using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Text_Car
// {
//     [TextArea(3, 10)] public string Text_Learnihg_for_Center;
//     [TextArea(3, 10)] public string Text_Learnihg_for_Panel;
// }

public class Machine_1 : MonoBehaviour
{
    [TextArea(3, 10)] public string[] Text_Learnihg_for_Center;
    [TextArea(3, 10)] public string[] Text_Learnihg_for_Panel;
    [SerializeField] private Camera_Controller controller_game;
    
    public static Machine_1 instance_car_1 { get; private set; }
    private void Awake() => instance_car_1 = this;

    public void Show_Text_Center(int index)
    {
        if(index < Text_Learnihg_for_Center.Length)
            controller_game.Show_Text_Center(Text_Learnihg_for_Center[index]);
        else
            Debug.LogError("index is out on array");
    }

    public void Show_Text_Center(string text)
    {
        controller_game.Show_Text_Center(text);
    }

    public void Show_Text_Panel(int index)
    {
        if(index < Text_Learnihg_for_Center.Length)
            controller_game.Show_Text_Center(Text_Learnihg_for_Center[index]);
        else
            Debug.LogError("index is out on array");
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
