using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    public List<GameObject> points = new List<GameObject>();

    public List<GameObject> peoplePrefabs = new List<GameObject>();

    public static GameData instance;

    public int totalInfluence;
    public int currentInfluence;
    public int droneCount;
    public int mealsCount;
    public LayerMask FoodHouse;
    public InfluenceBehaviour influenceBehaviour;

    public int currentDroneLevel;
    public GameObject[] dronePrefabs;
    public GameObject currentDronePrefab;

    public ScriptableDrone currentDrone;

    [Header("Sons")]
    [SerializeField] private AudioSource background;
    [SerializeField] private AudioSource eating;

    [Header("Texts")]
    [SerializeField] private TMP_Text droneCountText;
    [SerializeField] private TMP_Text mealsCountText;
    [SerializeField] private TMP_Text droneCountTextShop;
    [SerializeField] private TMP_Text mealsCountTextShop;

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
        background.Play();

        currentDroneLevel = 0;
        currentDronePrefab = dronePrefabs[0];

        points = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();

        currentDrone = droneTypes[0];

        influenceBehaviour.SetInfluenceArea(totalInfluence);

        //Create pool for people
        for (int i = 0; i < 500; i++)
        {
            GameObject peopleInstance = Instantiate(peoplePrefabs[0], transform.position, Quaternion.identity);
            inactivePeoplePool.Add(peopleInstance);
            peopleInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject peopleInstance = Instantiate(peoplePrefabs[1], transform.position, Quaternion.identity);
            inactivePeoplePool.Add(peopleInstance);
            peopleInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject peopleInstance = Instantiate(peoplePrefabs[2], transform.position, Quaternion.identity);
            inactivePeoplePool.Add(peopleInstance);
            peopleInstance.SetActive(false);
        }

        //Create pool for drones
        for (int i = 0; i < 500; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefabs[0], transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefabs[1], transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefabs[2], transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefabs[3], transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }

        for (int i = 0; i < 500; i++)
        {
            GameObject droneInstance = Instantiate(dronePrefabs[4], transform.position, Quaternion.identity);
            inactiveDronePool.Add(droneInstance);
            droneInstance.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (inactivePeoplePool.Count.Equals(1500))
        {
            StartCoroutine(SpawnPeople());
        }

        VerifyFoodHouses();
        droneCountText.text = droneCount.ToString();
        mealsCountText.text = mealsCount.ToString();
        droneCountTextShop.text = droneCount.ToString();
        mealsCountTextShop.text = mealsCount.ToString();
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
        for (int i = 0; i < 15; i++)
        {
            SpawnPerson();
            yield return new WaitForSeconds(5f);
        }
        yield return new WaitForSeconds(15f);
    }


    //Do the opposite of this for "DespawnPeople"
    public void SpawnPerson()
    {
        int random = Random.Range(0, peopleTypes.Count);
        GameObject newPerson = inactivePeoplePool[random * 500];
        newPerson.SetActive(true);
        inactivePeoplePool.Remove(newPerson);

        Vector3 point = points.ElementAt(Random.Range(0, points.Count)).transform.position;
        newPerson.transform.position = point + new Vector3(0,6,0);

        PeopleBehaviour newPersonBehaviour = newPerson.GetComponent<PeopleBehaviour>();
        newPersonBehaviour.peopleQuantity = peopleTypes[random].P_Qty;
        newPersonBehaviour.peopleVisuals = peopleTypes[random].Visual;
        newPersonBehaviour.peopleIsInfluenced = peopleTypes[random].Is_influenced;
    }

    //Do the opposite of this for "DespawnDrone"
    public void SpawnDrone(GameObject target, int quantity)
    {
        GameObject newDrone = inactiveDronePool[(currentDroneLevel * 500) + 1];
        inactiveDronePool.Remove(newDrone);
        newDrone.transform.position = transform.position;
        DroneBehaviour newDroneBehaviour = newDrone.GetComponent<DroneBehaviour>();
        newDrone.SetActive(true);
        newDroneBehaviour.droneData = currentDrone;
        newDroneBehaviour.agent.SetDestination(target.transform.position);
        newDroneBehaviour.agent.acceleration = newDroneBehaviour.droneData.Speed;
        newDroneBehaviour.agent.speed = newDroneBehaviour.droneData.Speed;
        newDroneBehaviour.currentCarry = quantity;
        newDroneBehaviour.target = target;
        droneCount--;
    }

    public void DespawnPeople(GameObject people)
    {
        PeopleBehaviour personBehaviour = people.GetComponent<PeopleBehaviour>();
        totalInfluence += personBehaviour.peopleQuantity;
        currentInfluence += personBehaviour.peopleQuantity;

        eating.Play();
        people.SetActive(false);
        people.transform.position = this.transform.position;
        inactivePeoplePool.Add(people);
        personBehaviour.peopleQuantity = 0;
        personBehaviour.peopleVisuals = null;
        personBehaviour.peopleIsInfluenced = false;
        influenceBehaviour.SetInfluenceArea(totalInfluence);
    }

    public void DespawnDrone(GameObject drone)
    {
        DroneBehaviour droneBehaviour = drone.GetComponent<DroneBehaviour>();

        drone.SetActive(false);
        drone.transform.position = this.transform.position;
        inactiveDronePool.Add(drone);
        droneBehaviour.droneData = null;
        droneCount++;
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
                mealsCount -= mealsToSend;
                SpawnDrone(people, mealsToSend);

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