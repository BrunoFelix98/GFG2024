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
        }
    }

    IEnumerator GiveFood()
    {
        clickable = true;
        yield return new WaitForSeconds(3.0f);
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            //Handle getting meals
        }
    }
}
