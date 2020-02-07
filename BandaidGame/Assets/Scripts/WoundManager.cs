using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WoundManager : MonoBehaviour
{
    public GameObject[] woundPositions;
    public float BLOOD_FREQUENCY_SECONDS;
    List<Bandaidable> wounds;
    public int maxWounds, bleedMultiplier, totalBleedValue, minWoundInterval, maxWoundInterval;
    int woundInterval;
    float timeSinceLastWounded;

    public AudioClip[] hurtNoises, bandaidPop;
    public AudioClip[] curedNoises;
    public AudioClip healedSFX, allHealSFX;
           
    private AudioSource audioSource;
    public AudioSource healSFXSource;
    private float bleedUpdateTime;


    void Start()
    {
        SetWoundInterval();
        InitialiseWounds();
        bleedUpdateTime = 0;
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else Debug.Log("An AudioSource is missing from an object which makes noises");
    }

    void FixedUpdate()
    {
        OpenWound();
    }

    void Update() {
        timeSinceLastWounded += Time.deltaTime;
        bleedUpdateTime += Time.deltaTime;
        if (bleedUpdateTime >= BLOOD_FREQUENCY_SECONDS) {
            UpdateBleedValue();
            bleedUpdateTime = 0;
        }
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
            //Debug.Log(pos);
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
            if(wound != null) {
                wound.bleed.Invoke();
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(hurtNoises[Random.Range(0, hurtNoises.Length)]);
                    audioSource.PlayOneShot(bandaidPop[Random.Range(0, bandaidPop.Length)]);

                }
                else Debug.Log("You're missing an audio source on a patient");
            }
        }
    }

    public Bandaidable GetClosedWound()
    {
        List<Bandaidable> closedWounds = wounds.FindAll(wound => !wound.isBleeding);
        if(closedWounds.Count > 0) {
            int pos = Random.Range(0, closedWounds.Count - 1);
            return closedWounds[pos];
        }
        return null;
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

    public void CuredNoisePlay()
    {
        if (audioSource != null) { audioSource.PlayOneShot(curedNoises[Random.Range(0, curedNoises.Length)]); }
        if (healSFXSource != null) { healSFXSource.PlayOneShot(healedSFX); }
        else Debug.Log("You're missing an audio source on a patient");
        //Debug.Log("NOISES FOR CURE");
    }

    public void RepairAllWounds()
    {
        foreach(Bandaidable wound in wounds)
        {
            if (wound.isBleeding)
            {
                wound.repair.Invoke();
                if (audioSource != null) { audioSource.PlayOneShot(curedNoises[Random.Range(0, curedNoises.Length)]); }
                else Debug.Log("You're missing an audio source on a patient");

                if (healSFXSource != null) { healSFXSource.PlayOneShot(allHealSFX); }
            }
            else
                Debug.Log("....but it doesn't affect Patient!");
        }
    }
}
