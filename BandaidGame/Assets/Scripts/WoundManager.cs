using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WoundManager : MonoBehaviour
{
    public GameObject[] woundPositions;
    List<Bandaidable> wounds;
    public int maxWounds, bleedMultiplier, totalBleedValue, minWoundInterval, maxWoundInterval;
    int woundInterval;
    float timeSinceLastWounded;
    void Start()
    {
        SetWoundInterval();
        InitialiseWounds();
    }

    void FixedUpdate()
    {
        UpdateBleedValue();
        OpenWound();
    }

    void Update() {
        timeSinceLastWounded += Time.deltaTime;
    }

    void SetWoundInterval()
    {
        timeSinceLastWounded = 0;
        woundInterval = Random.Range(minWoundInterval, maxWoundInterval);
    }

    void InitialiseWounds()
    {
        wounds = new List<Bandaidable>();
        while (wounds.Count < maxWounds)
        {
            int pos = Random.Range(0, woundPositions.Length);
            Debug.Log(pos);
            if (!woundPositions[pos].activeSelf)
            {
                woundPositions[pos].SetActive(true);
                wounds.Add(woundPositions[pos].GetComponent<Bandaidable>());
            }
        }
    }

    public void OpenWound()
    {
        if (timeSinceLastWounded >= woundInterval)
        {
            SetWoundInterval();
            Bandaidable wound = GetClosedWound();
            wound.bleed.Invoke();
        }
    }

    public Bandaidable GetClosedWound()
    {
        List<Bandaidable> closedWounds = wounds.FindAll(wound => !wound.isBleeding);
        int pos = Random.Range(0, closedWounds.Count);
        return closedWounds[pos];
    }

    public void UpdateBleedValue()
    {
        foreach(Bandaidable wound in wounds)
        {
            if (wound.isBleeding)
            {
                totalBleedValue += bleedMultiplier;
            }
        }
    }

    public int GetBleedValue()
    {
        int tempValue = totalBleedValue;
        totalBleedValue = 0;
        return tempValue;
    }
}
