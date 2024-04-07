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
    public bool hasSentDrone;
    public GameObject child;
    public float timer;
    public Material clickedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        clickable = false;
        hasSentDrone = false;
    }

    void FixedUpdate()
    {
        if (timer <= 0)
        {
            if (peopleIsInfluenced)
            {
                clickable = true;
                hasSentDrone = false;
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
            Renderer[] array = GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in array)
            {
                renderer.material = clickedMaterial;
            }

            if (!hasSentDrone)
            {
                SendDrone();
                hasSentDrone = true;
            }
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