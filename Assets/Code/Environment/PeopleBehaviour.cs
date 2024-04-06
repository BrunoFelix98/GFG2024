using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    public GameData data;
    public bool clickable;
    public ScriptablePeople peopleData;
    public GameObject child;
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
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag.Equals("People"))
                    {
                        if (peopleData.Is_influenced)
                        {
                            SendDrone();
                        }
                    }
                }
            }
        }
    }

    IEnumerator BecomeHungry()
    {
        clickable = true;
        yield return new WaitForSeconds(5.0f);
    }

    public void SendDrone()
    {
        data.SendDrone(peopleData.P_Qty, gameObject);

        clickable = false;
    }
}
