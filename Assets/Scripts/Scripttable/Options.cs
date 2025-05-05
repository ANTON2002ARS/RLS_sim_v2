using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Options", menuName = "ScriptableObjects/Options", order = 11)]
public class Options : ScriptableObject
{
    public string Action_to;
    public List<TestVariantAction> variants_deoloyment;
    public bool use_list_deoloyment;
    public List<TestVariantAction> variants_folding;

}
