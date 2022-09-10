using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    Coroutine cooldownCoroutine;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    private void Start()
    {
        timerIsRunning = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {

            cooldownCoroutine = StartCoroutine(CoolDown());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            StopCoroutine(cooldownCoroutine);
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3.0f);
        //Ne yapmasını istiyorsan
        //Ekran açsın
        print("yes");
    }

}
