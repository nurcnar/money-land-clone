using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMoney : MonoBehaviour
{

    public GameObject moneyHolder;

    public static CreateMoney instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SpawnMoney();
    }
    private void SpawnMoney()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject clone = Instantiate(moneyHolder, new Vector3(-9 + 2 * i, 0, -9 + 2* j), Quaternion.identity); // boş bir gameobject yarattık
                clone.GetComponent<MoneyHolder>().SpawnMoneyOnMe();
            }
        }
    }



}
