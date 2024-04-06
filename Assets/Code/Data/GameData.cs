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
            peopleInstance.SetActive(false);
        }

        //Create pool for drones
        for (int i = 0; i < 50; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            droneInstance.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnIcon(Vector3 position)
    {
        //Handle spawning of the icons on top of the houses (Rodrigo)
    }

    public void DespawnPeople(GameObject people)
    {
        //Handle the despawning of people on the streets using a pool (João)
    }

    public void ReduceMeals(int quantity)
    {
        mealsCount -= quantity;
    }

    public void SendDrone(Vector3 position, int peopleAmount)
    {
        if (peopleAmount <= currentDrone.C_Capacity)
        {
            //Handle object pooling (Khevynn)
        }
    }
}
