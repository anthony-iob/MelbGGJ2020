using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public GameObject[] existingNPCs;
    public GameObject[] npcBlueprints;
    public int npcSpawnInterval;
    public Transform[] spawnPoints;
    private GameObject[] currentNPCs;
    private int spawnedNPCs;
    float sinceSpawn;
    List<WoundManager> activeNPCs;

    public AudioClip[] patientSpawnSFX;
    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>(); 
        currentNPCs = new GameObject[npcBlueprints.Length + existingNPCs.Length];
        spawnedNPCs = 0;
        int count = 0;
        foreach (GameObject npc in existingNPCs)
        {
            currentNPCs[count] = npc;
            count++;
        }
    }

    // Update is called once per frame
    public int UpdateBloodLevel()
    {        
        int totalBloodLevel = 0;
        foreach (GameObject npc in currentNPCs)
        {
            if(npc != null) {
                var woundManager = npc.GetComponentInChildren<WoundManager>();
                totalBloodLevel += woundManager.GetBleedValue();
            }
        }
        return totalBloodLevel;
    }

    public void Update()
    {
        sinceSpawn += Time.deltaTime;
        if(sinceSpawn >= npcSpawnInterval) {
            for(int pos = 0; pos < currentNPCs.Length; pos++)
            {
                GameObject npc = currentNPCs[pos];
                if(npc == null) {
                    Transform spawnPoint = GetRandomSpawnPoint();
                    currentNPCs[pos] = Instantiate(npcBlueprints[spawnedNPCs], spawnPoint.position, spawnPoint.rotation);
                    if (audioSource != null) { audioSource.PlayOneShot(patientSpawnSFX[Random.Range(0, patientSpawnSFX.Length)]); }
                    else Debug.Log("You're missing an audio source on the NPC Manager");                 
                    spawnedNPCs += 1;
                    sinceSpawn = 0;

                    break;
                }
            } 
        }
        else
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].GetComponent<Animator>().SetBool("Spawning", false);
            }
        }
    }

    Transform GetRandomSpawnPoint() 
    {
        for (int i = 0; i < spawnPoints.Length ; i++)
        {
            spawnPoints[i].GetComponent<Animator>().SetBool("Spawning", false);
        }
        int pos = Random.Range(0, spawnPoints.Length); //Removed the (spawnPoints.Length - 1) line as it was never generating the max value for some odd reason
        Transform spawnPoint = spawnPoints[pos];
        spawnPoints[pos].GetComponent<Animator>().SetBool("Spawning", true);
        return spawnPoint;
    }
}
