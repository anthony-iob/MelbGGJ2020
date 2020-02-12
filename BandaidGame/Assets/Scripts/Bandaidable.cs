using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bandaidable : MonoBehaviour
{
    public GameObject emitter, bandaid;
    public bool isBleeding;
    public UnityEvent repair, bleed;
    public Sprite[] bandaidDesign;

    public void Awake() {

        emitter.SetActive(false);
        bandaid.SetActive(false);
        isBleeding = false;
    }

    public void Bleed ()
    {
        isBleeding = true;
    }

    public void Repair()
    {
        isBleeding = false;
    }  

    public void SetNewDesign()
    {
        int designNo = Random.Range(0, bandaidDesign.Length);
        bandaid.GetComponent<SpriteRenderer>().sprite = bandaidDesign[designNo];
    }
    
}
