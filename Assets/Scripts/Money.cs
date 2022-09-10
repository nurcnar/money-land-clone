using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public Vector3 groundPos;

    public static Money instance;
    private void Awake()
    {
        instance = this;
    }
}
