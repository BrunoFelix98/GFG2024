using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public TextMeshProUGUI foodCostTxt;
    public TextMeshProUGUI droneCostTxt;
    public TextMeshProUGUI droneUpgradeCostTxt;
    public TextMeshProUGUI currentInfluence;

    public int foodCost;
    public int droneCost;
    public int droneUpgradeCost;

    public GameData data;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;

        foodCostTxt.text = foodCost.ToString();
        droneCostTxt.text = droneCost.ToString();
        droneUpgradeCostTxt.text = droneUpgradeCost.ToString();
    }

    private void Update()
    {
        currentInfluence.text = data.currentInfluence.ToString();
}

    public void BuyFood()
    {
        if (data.currentInfluence >= foodCost)
        {
            data.currentInfluence -= foodCost;
            data.AddMeals(1);
        }
        else
        {
            PrintOther(); //Replace to a visual feedback representation
        }
    }

    public void BuyDrone()
    {
        if (data.currentInfluence >= droneCost)
        {
            data.currentInfluence -= droneCost;
            data.AddDrone(1);
        }
        else
        {
            PrintOther(); //Replace to a visual feedback representation
        }
    }

    public void BuyDroneUpgrade()
    {
        if (data.currentInfluence >= droneUpgradeCost)
        {
            for (int i = 0; i < data.droneTypes.Count; i++)
            {
                if (data.currentDrone.Equals(data.droneTypes[i]))
                {
                    if (data.droneTypes[i+1] != null)
                    {
                        data.currentInfluence -= droneUpgradeCost;
                        data.currentDrone = data.droneTypes[i + 1];
                    }
                }
            }
        }
        else
        {
            PrintOther(); //Replace to a visual feedback representation
        }
    }

    public void PrintOther()
    {
        print("You dont have enough influence"); //Replace to a visual feedback representation
    }
}
