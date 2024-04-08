using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drone", menuName = "ScriptableObjects/ScriptableDrone", order = 1)]
public class ScriptableDrone : ScriptableObject
{
    [SerializeField]
    private int speed;
    [SerializeField]
    private int c_Capacity;
    [SerializeField]
    private int currentLoad;

    public int CurrentLoad
    {
        get => currentLoad;
        set => currentLoad = value;
    }
    public int C_Capacity
    {
        get => c_Capacity;
        set => c_Capacity = value;
    }
    public int Speed
    {
        get => speed;
        set => speed = value;
    }
}
