using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WoundManager : MonoBehaviour
{
    public GameObject[] woundPositions;
    public float BLOOD_FREQUENCY_SECONDS;
    List<Bandaidable> wounds;
    public int maxWounds, bleedMultiplier, totalBleedValue;
    public int minWoundInterval, maxWoundInterval;
    float woundInterval;
    float timeSinceLastWounded;

    public AudioClip[] hurtNoises, bandaidPop;
    public AudioClip[] curedNoises;
    public AudioClip healedSFX, allHealSFX;
           
    private AudioSource audioSource;
    public AudioSource healSFXSource;
    private float bleedUpdateTime;

    private int pos = 0;

    public int openWounds = 0;

    void Start()
    {
        SetWoundInterval();
        InitialiseWounds();
        bleedUpdateTime = 0;
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else Debug.Log("An AudioSource is missing from an patients wound which makes noises");
    }

    void FixedUpdate()
    {

        if (openWounds < maxWounds)
        {
            OpenWound();
        }

        if (maxWounds == openWounds) //did this so it doesn't immediately spawn a new wound when wound is cured. 
        {
            timeSinceLastWounded = 0;
        }
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
        while (wounds.Count < woundPositions.Length)
        {
            //int pos = Random.Range(0, woundPositions.Length);
            //Debug.Log(pos);

            
            if (!woundPositions[pos].activeSelf)
            {
                woundPositions[pos].SetActive(true);
                wounds.Add(woundPositions[pos].GetComponent<Bandaidable>());
                pos += 1;
            }
        }
    }

    public void OpenWound()
    {



        if (timeSinceLastWounded >= woundInterval)
        {
            SetWoundInterval();
            Bandaidable wound = GetClosedWound();
            if (wound != null)
            {
                wound.bleed.Invoke();
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(hurtNoises[Random.Range(0, hurtNoises.Length)]);
                    audioSource.PlayOneShot(bandaidPop[Random.Range(0, bandaidPop.Length)]);
                }
                else Debug.Log("You're missing an audio source on a patient");

                openWounds++;
            }
        }
    }

    public Bandaidable GetClosedWound()
    {
        List<Bandaidable> closedWounds = wounds.FindAll(wound => !wound.isBleeding);
        if(closedWounds.Count > 0) {
            int pos = Random.Range(0, closedWounds.Count);
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

        openWounds--;
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

                openWounds = 0; //turn this off and they'll be perma cured. 
            }
            //else Debug.Log("....but it doesn't affect Patient!");
        }
    }
}
