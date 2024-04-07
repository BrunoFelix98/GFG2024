using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    public GameData data;
    public bool clickable;
    public int foodQuantity;
    public bool isInfluenced;
    public GameObject popUp;
    public float timer;
    public float minTimer, maxTimer;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        clickable = false;

        popUp.SetActive(false);

        if (insideInfluenceArea())
        {
            isInfluenced = true;
        }
        else
        {
            isInfluenced = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= 0)
        {
            if (isInfluenced)
            {
                clickable = true;
                timer = Random.Range(minTimer, maxTimer);
            }
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }


        if (clickable)
        {
            data.SpawnIcon(this);
        }
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            GetMeals();
            popUp.SetActive(false);
            clickable = false;
        }
    }

    public void GetMeals()
    {
        data.AddMeals(foodQuantity);
    }

    public bool insideInfluenceArea()
    {
        //Check if the house is inside the area of influence
        return false; //Temporary
    }
}