using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "House", menuName = "ScriptableObjects/ScriptableHouse", order = 2)]
public class ScriptableHouse : ScriptableObject
{
    [SerializeField]
    private int food_Quantity;
    [SerializeField]
    private bool is_Influenced;

    public bool Is_influenced
    {
        get => is_Influenced;
        set => is_Influenced = value;
    }
}
