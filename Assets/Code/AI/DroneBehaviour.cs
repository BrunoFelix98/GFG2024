using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneBehaviour : MonoBehaviour
{
    public GameData data;
    public ScriptableDrone droneData;
    public GameObject child;
    public GameObject target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //If drone reaches destination, call "data.DespawnDrone(this)" (Khevynn)
        agent.SetDestination(target.transform.position);
        if(agent.remainingDistance < 1f)
        {
            target.GetComponent<PeopleBehaviour>().peopleQuantity -= droneData.C_Capacity;
            data.DespawnDrone(this.gameObject);
        }
    }
}