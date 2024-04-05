using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "People", menuName = "ScriptableObjects/ScriptablePeople", order = 0)]
public class ScriptablePeople : ScriptableObject
{
    [SerializeField]
    private int p_Qty;
    [SerializeField]
    private GameObject visual;
    [SerializeField]
    private bool Is_influenced;
}
