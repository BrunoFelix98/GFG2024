using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drone", menuName = "ScriptableObjects/ScriptableDrone", order = 1)]
public class ScriptableDrone : ScriptableObject
{
    [SerializeField]
    private int Speed;
    [SerializeField]
    private int C_Capacity;
}
