using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestVariantAction", menuName = "ScriptableObjects/TestVariantAction", order = 11)]
public class TestVariantAction : ScriptableObject
{
    [Multiline] public string ActionName;
    public bool boolis_selected;
    public int number;
    public int SelectedIndex { get; set; } = -1;
}
