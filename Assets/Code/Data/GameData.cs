using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

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
    public LayerMask FoodHouse;

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

    private void FixedUpdate()
    {
        VerifyFoodHouses();
    }

    void VerifyFoodHouses()
    {
        List<GameObject> allHouses = GameObject.FindGameObjectsWithTag("House").ToList();

        for (int i = 0; i < allHouses.Count; i++)
        {
            if (Vector3.Distance(transform.position, new Vector3(allHouses.ElementAt(i).transform.position.x, 0, allHouses.ElementAt(i).transform.position.z)) < totalInfluence)
            {
                if (allHouses.ElementAt(i).GetComponent<HouseBehaviour>())
                {
                    allHouses.ElementAt(i).GetComponent<HouseBehaviour>().isInfluenced = true;
                }
            }
        }
    }

    IEnumerator SpawnPeople()
    {
        for (int i = 0; i < 15; i++) //To be changed to a variable so that it can be dependant on the influence size
        {
            yield return new WaitForSeconds(2f);
            SpawnPerson();
        }
        
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    //Do the opposite of this for "DespawnPeople"
    public void SpawnPerson()
    {
        GameObject newPerson = inactivePeoplePool[0];
        newPerson.SetActive(true);
        inactivePeoplePool.Remove(newPerson);

        Vector3 point;
        if (RandomPoint(Vector3.zero, totalInfluence, out point))
        {
            newPerson.transform.position = point;
        }

        PeopleBehaviour newPersonBehaviour = newPerson.GetComponent<PeopleBehaviour>();
        int random = Random.Range(0, peopleTypes.Count);
        newPersonBehaviour.peopleQuantity = peopleTypes[random].P_Qty;
        newPersonBehaviour.peopleVisuals = peopleTypes[random].Visual;
        newPersonBehaviour.peopleIsInfluenced = peopleTypes[random].Is_influenced;
    }

    //Do the opposite of this for "DespawnDrone"
    public void SpawnDrone(GameObject target)
    {
        GameObject newDrone = inactiveDronePool[0];
        newDrone.SetActive(true);
        inactiveDronePool.Remove(newDrone);
        newDrone.transform.position = transform.position;
        DroneBehaviour newDroneBehaviour = newDrone.GetComponent<DroneBehaviour>();
        newDroneBehaviour.droneData = currentDrone;
        newDroneBehaviour.target = target;
    }

    public void DespawnPeople(GameObject people)
    {
        PeopleBehaviour personBehaviour = people.GetComponent<PeopleBehaviour>();
        totalInfluence += personBehaviour.peopleQuantity;
        currentInfluence += personBehaviour.peopleQuantity;

        people.SetActive(false);
        people.transform.position = this.transform.position;
        inactivePeoplePool.Add(people);
        personBehaviour.peopleQuantity = 0;
        personBehaviour.peopleVisuals = null;
        personBehaviour.peopleIsInfluenced = false;
    }

    public void DespawnDrone(GameObject drone)
    {
        DroneBehaviour droneBehaviour = drone.GetComponent<DroneBehaviour>();

        drone.SetActive(false);
        drone.transform.position = this.transform.position;
        inactiveDronePool.Add(drone);
        droneBehaviour.droneData = null;
    }

    public void SpawnIcon(HouseBehaviour house)
    {
        house.popUp.SetActive(true);
    }

    public void AddMeals(int quantity)
    {
        mealsCount += quantity;
    }

    public void AddDrone(int quantity)
    {
        droneCount += quantity;
    }

    public bool HasEnoughMeals(int quantity)
    {
        if (mealsCount - quantity < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SendDrone(int peopleAmount, GameObject people)
    {
        if (HasEnoughMeals(peopleAmount))
        {
            int remainingPeople = peopleAmount;
            int dronesNeeded = peopleAmount / currentDrone.C_Capacity;
            if (peopleAmount % currentDrone.C_Capacity != 0)
            {
                dronesNeeded++;
            }

            for (int i = 1; i <= dronesNeeded; i++)
            {
                int mealsToSend = Mathf.Min(currentDrone.C_Capacity, remainingPeople);
                remainingPeople -= mealsToSend;
                SpawnDrone(people);

                if (remainingPeople == 0)
                {
                    break;
                }
            }
        }
        else
        {
            print("You don't have enough meals to send!"); //Change to a visual representation (Ariel(visuals) Bruno(implementation))
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector3.zero, totalInfluence);
    }
}