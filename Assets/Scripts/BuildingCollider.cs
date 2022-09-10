using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingCollider : MonoBehaviour
{
    public TextMeshProUGUI moneyUI;

    public GameObject rightUp;
    public GameObject leftUp;
    public GameObject rightDown;
    public GameObject leftDown;

    public GameObject moneyHolderPrefab;

    public List<MoneyHolder> moneyHolders = new List<MoneyHolder>();

    public int target;

    public float x;
    public float z;

    public enum Mode
    {
        Locked,
        Unlocked
    }

    public enum Col
    {
        Shop,
        Bank,
        Donuts
    };

    public Mode currentMode;
    public Col col;

    public int current;

    private void Start()
    {
        //SpawnMoney();
    }

    public void AddMoney()
    {
        current++;
        switch (col)
        {
            case Col.Shop:
                moneyUI.text = current.ToString() + "/5 $";
                break;
            case Col.Bank:
                moneyUI.text = current.ToString() + "/10 $";
                break;
            case Col.Donuts:
                moneyUI.text = current.ToString() + "/15 $";
                break;
        }
    }

    public bool IsFull()
    {  
        if (target <= current)
        {
            currentMode = Mode.Unlocked;
            moneyUI.text = "1$/3second";
            return true;
        }
        return false;
    }

    public void SpawnMoney()
    {
         x = rightUp.transform.position.x - leftUp.transform.position.x;
         z = leftUp.transform.position.z - leftDown.transform.position.z;

        switch (col)
        {
            case Col.Shop:
                ShopMoney();
                break;
            case Col.Bank:
                BankMoney();
                break;
            case Col.Donuts:
                DonutsMoney();
                break;
        }

        StartCoroutine(SpawnMoneyRoutine());
    }

    public void ShopMoney()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                var _moneyHolder = Instantiate(moneyHolderPrefab, transform);
                _moneyHolder.transform.localPosition = new Vector3(i / 5.0f - .4f, -.5f, j / 2.0f - .75f);
                _moneyHolder.transform.localScale = new Vector3(_moneyHolder.transform.localScale.x / transform.parent.localScale.x, _moneyHolder.transform.localScale.y / transform.parent.localScale.y, _moneyHolder.transform.localScale.z / transform.parent.localScale.z);
                _moneyHolder.GetComponent<MoneyHolder>().isInBuilding = true;
                moneyHolders.Add(_moneyHolder.GetComponent<MoneyHolder>());
            }
        }
    }


    public void BankMoney()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                var _moneyHolder = Instantiate(moneyHolderPrefab, transform);
                _moneyHolder.transform.localPosition = new Vector3(i / 7.0f - .4f, -.5f, j / 7.0f);
                _moneyHolder.transform.localScale = new Vector3(_moneyHolder.transform.localScale.x / transform.parent.localScale.x, _moneyHolder.transform.localScale.y / transform.parent.localScale.y, _moneyHolder.transform.localScale.z / transform.parent.localScale.z);
                _moneyHolder.GetComponent<MoneyHolder>().isInBuilding = true;
                moneyHolders.Add(_moneyHolder.GetComponent<MoneyHolder>());
            }
        }
    }

    public void DonutsMoney()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                var _moneyHolder = Instantiate(moneyHolderPrefab, transform);
                _moneyHolder.transform.localPosition = new Vector3(i / 10.0f - .4f, -.5f, j / 10.0f );
                _moneyHolder.transform.localScale = new Vector3(_moneyHolder.transform.localScale.x / transform.parent.localScale.x, _moneyHolder.transform.localScale.y / transform.parent.localScale.y, _moneyHolder.transform.localScale.z / transform.parent.localScale.z);
                _moneyHolder.GetComponent<MoneyHolder>().isInBuilding = true;
                moneyHolders.Add(_moneyHolder.GetComponent<MoneyHolder>());
            }
        }
    }

    public IEnumerator SpawnMoneyRoutine()
    {
        int i = 0;

        while (true)
        {
            yield return new WaitForSeconds(3);
            int last_i = i;
            while (moneyHolders[i].moneyOnMe)
            {
                i++;
                if (i > moneyHolders.Count - 1)
                {
                    i = 0;
                }
                if (i == last_i && moneyHolders[i].moneyOnMe)
                {
                    break;
                }
            }
            if (!moneyHolders[i].moneyOnMe)
            {
                moneyHolders[i].SpawnMoneyOnMe();
            }
        }
    }
}
