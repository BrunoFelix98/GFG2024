using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    public GameData data;
    public ScriptableDrone droneData;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //If drone reaches destination, call "data.DespawnDrone(this)" (Rodrigo)
    }
}