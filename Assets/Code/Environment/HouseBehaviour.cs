using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    public GameData data;
    public bool clickable;
    public ScriptableHouse houseData;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        clickable = false;

        if(insideInfluenceArea())
        {
            houseData.Is_influenced = true;
        }
        else
        {
            houseData.Is_influenced = false;
        }

        StartCoroutine(GiveFood());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clickable)
        {
            if(houseData.Is_influenced)
            {
                data.SpawnIcon(this.transform.position);
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag.Equals("People"))
                    {
                        if (houseData.Is_influenced)
                        {
                            GetMeals();
                            clickable = false;
                        }
                    }
                }
            }
        }
    }

    IEnumerator GiveFood()
    {
        if (houseData.Is_influenced)
        {
            clickable = true;
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void GetMeals()
    {
        data.AddMeals(houseData.Food_Quantity);
    }

    public bool insideInfluenceArea()
    {
        //Check if the house is inside the area of influence
        return false; //Temporary
    }
}