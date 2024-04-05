using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "House", menuName = "ScriptableObjects/ScriptableHouse", order = 2)]
public class ScriptableHouse : ScriptableObject
{
    [SerializeField]
    private int Food_Quantity;
    [SerializeField]
    private bool Is_influenced;
}
