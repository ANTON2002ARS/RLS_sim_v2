using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Text_Car
{
    [TextArea(3, 10)] public string Text_Learnihg_for_Center;
    [TextArea(3, 10)] public string Text_Learnihg_for_Panel;
}

public class Machine_1 : MonoBehaviour
{
    [SerializeField] private List<Text_Car> text_car;
    [SerializeField] private Camera_Controller controller_game;

    public void Show_Text_Center(string text)
    {
        controller_game.Show_Text_Center(text);
    }

    public void Show_Text_Center(Position_krutilka krutilka)
    {
        controller_game.Show_Text_Center(krutilka.Text_Learnihg_for_Center);
    }
    public void Show_Text_Panel(string text)
    {
        controller_game.Show_Text_Panel(text);
    }

    public void Show_Text_Panel(Position_krutilka krutilka)
    {
        controller_game.Show_Text_Panel(krutilka.Text_Learnihg_for_Panel);
    }

    public void Use_Panel(bool show)
    {
        controller_game.Use_Panel(show);
    }
}
