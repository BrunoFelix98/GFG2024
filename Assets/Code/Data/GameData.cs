using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<ScriptablePeople> peopleTypes = new List<ScriptablePeople>();
    public List<ScriptableHouse> houseTypes = new List<ScriptableHouse>();
    public List<ScriptableDrone> droneTypes = new List<ScriptableDrone>();

    public List<GameObject> inactivePeoplePool = new List<GameObject>();
    public List<GameObject> inactiveDronePool = new List<GameObject>();

    public GameObject peoplePrefab;
    public GameObject dronePrefab;

    public static GameData instance;

    public int totalInfluence;
    public int currentInfluence;
    public int droneCount;
    public int mealsCount;

    public ScriptableDrone currentDrone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Create pool for people
        for (int i = 0; i < 30; i++)
        {
            GameObject peopleInstance = Instantiate(peoplePrefab, transform.position, Quaternion.identity);
            inactivePeoplePool.Add(peopleInstance);
            peopleInstance.SetActive(false);
        }

        //Create pool for drones
        for (int i = 0; i < 50; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }

        currentDrone = droneTypes[0];

        StartCoroutine(SpawnPeople());
    }

    IEnumerator SpawnPeople()
    {
        for (int i = 0; i < 15; i++) //To be changed to a variable so that it can be dependant on the influence size
        {
            yield return new WaitForSeconds(2f);
            SpawnPerson();
        }
        
    }

    //Do the opposite of this for "DespawnPeople"
    public void SpawnPerson()
    {
        GameObject newPerson = inactivePeoplePool[0];
        newPerson.SetActive(true);
        inactivePeoplePool.Remove(newPerson);
        newPerson.transform.position = transform.position; //Change to navmesh available position
        PeopleBehaviour newPersonBehaviour = newPerson.GetComponent<PeopleBehaviour>();
        int random = Random.Range(0, peopleTypes.Count);
        newPersonBehaviour.peopleData = peopleTypes[random];
    }

    //Do the opposite of this for "DespawnDrone"
    public void SpawnDrone()
    {
        GameObject newDrone = inactiveDronePool[0];
        newDrone.SetActive(true);
        inactiveDronePool.Remove(newDrone);
        newDrone.transform.position = transform.position;
        DroneBehaviour newPersonBehaviour = newDrone.GetComponent<DroneBehaviour>();
        newPersonBehaviour.droneData = currentDrone;
    }

    public void DespawnPeople(GameObject people)
    {
        //Handle the despawning of people on the streets using a pool (João)
    }

    public void DespawnDrone(GameObject drone)
    {
        //Handle the despawning of drones using a pool (João)
    }

    public void SpawnIcon(Vector3 position)
    {
        //Handle spawning of the icons on top of the houses (Rodrigo)
    }

    public void ReduceMeals(int quantity)
    {
        if (mealsCount - quantity < 0)
        {
            print("You dont have enough meals"); //Change to a visual representation (Ariel(visuals) Bruno(implementation))
        }
        else
        {
            mealsCount -= quantity;
        }
    }

    public void SendDrone(int peopleAmount, GameObject people)
    {
        if (peopleAmount <= currentDrone.C_Capacity)
        {
            SpawnDrone();
            //Call "DespawnDrone(drone)" whenever the drone reaches the destination, as well as "DespawnPeople"
            //Object pool drone objects with navMeshAgent to get to the people
            //Call "DespawnPeople(people)"
        }
    }
}