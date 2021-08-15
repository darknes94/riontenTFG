using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class DoorLever : MonoBehaviour
{
    [SerializeField]
    int code;

    void Awake()
    {
        code = 0;
    }

    public int GetCode()
    {
        return code;
    }

    public void Open()
    {
        Debug.Log("Abro puerta");
    }

    public void Close()
    {
        Debug.Log("Cierro puerta");
    }
}
