using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public GameObject[] npcs;
    // Update is called once per frame
    public int UpdateBloodLevel()
    {
        return 10;
    }
}
