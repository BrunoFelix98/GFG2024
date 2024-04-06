using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    public GameData data;
    public bool clickable;
    public int peopleQuantity;
    public GameObject peopleVisuals;
    public bool peopleIsInfluenced;
    public GameObject child;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        clickable = false;
    }

    void FixedUpdate()
    {
        if (timer <= 0)
        {
            if (peopleIsInfluenced)
            {
                clickable = true;
                timer = 3;
            }
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }

        if (peopleQuantity < 1)
        {
            data.DespawnPeople(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            clickable = false;
        }
    }

    public void SendDrone()
    {
        if (data.droneCount > 0)
        {
            data.SendDrone(peopleQuantity, gameObject);

            clickable = false;
        }
    }
}