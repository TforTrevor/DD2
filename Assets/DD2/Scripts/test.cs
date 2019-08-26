using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartData.SmartEvent;

public class test : MonoBehaviour
{
    [SerializeField] EventListener listener;

    public void DoThing()
    {
        Debug.Log("Hey");
    }
}
