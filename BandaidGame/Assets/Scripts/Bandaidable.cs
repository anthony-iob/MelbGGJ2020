using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bandaidable : MonoBehaviour
{
    public GameObject emitter, bandaid;
    public bool isBleeding;
    public UnityEvent repair, bleed;

    public void Bleed ()
    {
        Debug.Log("Bleeding");
        isBleeding = true;
    }

    public void Repair()
    {
        Debug.Log("Repairing");
        isBleeding = false;
    }
    
}
