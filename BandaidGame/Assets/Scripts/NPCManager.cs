using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public GameObject[] maxNPCs;
    public GameObject npcBlueprint;
    public int npcSpawnInterval;
    public Transform[] spawnPoints;
    float sinceSpawn;
    List<WoundManager> activeNPCs;

    // Update is called once per frame
    public int UpdateBloodLevel()
    {
        int totalBloodLevel = 0;
        foreach (GameObject npc in maxNPCs)
        {
            var woundManager = npc.GetComponent<WoundManager>();
            totalBloodLevel += woundManager.GetBleedValue();
        }
        return totalBloodLevel;
    }

    public void Update() 
    {
        sinceSpawn += Time.deltaTime;
        if(sinceSpawn >= npcSpawnInterval) {
            for(int pos = 0; pos < maxNPCs.Length; pos++)
            {
                GameObject npc = maxNPCs[pos];
                if(npc == null) {
                    Transform spawnPoint = GetRandomSpawnPoint();
                    maxNPCs[pos] = Instantiate(npcBlueprint, spawnPoint.position, spawnPoint.rotation);
                    sinceSpawn = 0;
                }
            } 
        }
    }

    Transform GetRandomSpawnPoint() {
        int pos = Random.Range(0, spawnPoints.Length - 1);
        Transform spawnPoint = spawnPoints[pos];
        return spawnPoint;
    }
}
