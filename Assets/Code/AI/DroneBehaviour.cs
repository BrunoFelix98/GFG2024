using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneBehaviour : MonoBehaviour
{
    public GameData data;
    public ScriptableDrone droneData;
    public int currentCarry;
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
        // If drone reaches destination, despawn it
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 5.0f)
        {
            target.GetComponent<PeopleBehaviour>().peopleQuantity -= currentCarry;
            data.totalInfluence++;
            data.currentInfluence++;
            data.DespawnDrone(this.gameObject);
        }
    }
}