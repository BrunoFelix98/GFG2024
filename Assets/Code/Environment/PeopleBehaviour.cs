using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    public GameData data;
    public bool clickable;
    public ScriptablePeople peopleData;
    // Start is called before the first frame update
    void Start()
    {
        data = GameData.instance;
        clickable = false;

        StartCoroutine(BecomeHungry());
    }

    void FixedUpdate()
    {
        if (clickable)
        {
            if (peopleData.Is_influenced)
            {
                data.DespawnPeople(this.gameObject);
            }
        }
    }

    IEnumerator BecomeHungry()
    {
        clickable = true;
        yield return new WaitForSeconds(5.0f);
    }

    private void OnMouseDown()
    {
        if (clickable)
        {
            SendDrone();
        }
    }

    public void SendDrone()
    {
        data.SendDrone(this.transform.position, peopleData.P_Qty);

        clickable = false;
    }
}
