using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHolder : MonoBehaviour
{
    public static MoneyHolder instance;
    private void Awake()
    {
        instance=this;
    }

    public bool isInBuilding;

    public Money moneyOnMe; //money script classı tipinde money değişkeni
    public GameObject money; //money prefabi

    public void SpawnMoneyOnMe()
    {
        moneyOnMe = Instantiate(money, transform.position, Quaternion.identity, transform).GetComponent<Money>(); //benim olduğum yerde benim çocuğum olan parayı yarat
        moneyOnMe.GetComponent<Money>().groundPos = transform.position;
        moneyOnMe.GetComponent<Money>().tag = "money";
    }

    public void RemoveMoneyOnMe()
    {
        moneyOnMe = null; //artık birey bile değil
        if (!isInBuilding)
        {
            StartCoroutine(RespawnTile());
        }
    }
    public IEnumerator RespawnTile()
    {
        yield return new WaitForSeconds(3);
        //moneyOnMe = Instantiate(money, transform.position, Quaternion.identity, transform).GetComponent<Money>(); // 3 saniye bekleyip yeni küp yarattık
        SpawnMoneyOnMe(); // yaratılan küpe özellik ekledik
    }
}
