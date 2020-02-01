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

    void Start() 
    {
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
                    spawnedNPCs += 1;
                    sinceSpawn = 0;
                }
            } 
        }
    }

    Transform GetRandomSpawnPoint() 
    {
        int pos = Random.Range(0, spawnPoints.Length - 1);
        Transform spawnPoint = spawnPoints[pos];
        return spawnPoint;
    }
}
