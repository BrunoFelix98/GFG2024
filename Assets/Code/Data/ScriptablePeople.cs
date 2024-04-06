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
    private bool is_Influenced;

    public bool Is_influenced
    {
        get => is_Influenced;
        set => is_Influenced = value;
    }

    public int P_Qty
    {
        get => p_Qty;
        set => p_Qty = value;
    }
    public GameObject Visual
    {
        get => visual;
        set => visual = value;
    }
}