using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 mousePos1;
    private Vector2 mousePos2;
    private float r;
    private float deltaMousePos2ToRadius;
    private Vector2 deltaMousePositions;
    private Vector3 deltaMousePositions3;

    public List<Money> earning = new List<Money>();
    public int lastY;
    public float movementSpeed;

    public BuildingCollider currentBuildingCollider;

    Coroutine sendMoneyCoroutine;

    public int capacity=15;

    public static PlayerMovement instance;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePos2 = Input.mousePosition;

            r = Screen.height * 0.125f;
            deltaMousePositions = mousePos2 - mousePos1;
            if (deltaMousePositions.magnitude > r)
            {
                deltaMousePos2ToRadius = deltaMousePositions.magnitude - r;
                mousePos1 += deltaMousePositions.normalized * deltaMousePos2ToRadius;
            }

            deltaMousePositions = Vector2.ClampMagnitude(deltaMousePositions, r); //
            deltaMousePositions3 = new Vector3(deltaMousePositions.x, 0, deltaMousePositions.y);

            transform.position += deltaMousePositions3 * Time.deltaTime * movementSpeed;


        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "money")
        {
            if (earning.Count >= capacity)
            {
                print("FULLL");
                return;
            }

            other.transform.parent.GetComponent<MoneyHolder>().RemoveMoneyOnMe(); // 3 saniye sonra yenisini yaratması için
            earning.Add(other.GetComponent<Money>());

            lastY++;
            //Mathf.Clamp(lastY, 1, 100);

            other.transform.parent = this.transform;
            other.transform.localPosition = Vector3.up * lastY;
        }
        
        if (other.tag == "building")
        {
            currentBuildingCollider = other.GetComponent<BuildingCollider>();
            if (currentBuildingCollider.currentMode == BuildingCollider.Mode.Locked)
            {
                if (earning.Count >= 1)
                {
                    sendMoneyCoroutine = StartCoroutine(SendMoneyToBuilding());
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "building")
        {
            currentBuildingCollider = null;
            if (sendMoneyCoroutine != null)
            {
                StopCoroutine(sendMoneyCoroutine);
            }
        }
    }

    private IEnumerator SendMoneyToBuilding()
    {
        float sendMoneyTime = .5f;

        for (int i = earning.Count - 1; i >= 0; i--)
        {
            lastY--;
            Mathf.Clamp(lastY, 1, 100);
            var del = earning.Last();
            earning.Remove(del);

            del.transform.DOMove(currentBuildingCollider.transform.position, sendMoneyTime).OnComplete(() => Destroy(del.gameObject));

            yield return new WaitForSeconds(sendMoneyTime);

            currentBuildingCollider.AddMoney();

            if (currentBuildingCollider.IsFull())
            {
                currentBuildingCollider.SpawnMoney();
                yield break;
            }
        }
    }
}
