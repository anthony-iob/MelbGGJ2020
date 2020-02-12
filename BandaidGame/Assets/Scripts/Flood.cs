using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    public GameObject flood;
    public Transform minHeight;
    public Transform maxHeight;
    public float endgameFloodRate = 0.0008f;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentBloodLevel < 10000)
        {
            var minHeightPosition = minHeight.position;
            var maxHeightPosition = maxHeight.position;
            var percentage = GameManager.instance.GetFloodPercentage();
            var newHeight = (maxHeightPosition.y - minHeightPosition.y) * percentage;
            var up = new Vector3(0, newHeight, 0);
            flood.transform.position = minHeightPosition + up;
        }
        else
        {
            flood.transform.position += new Vector3(0, endgameFloodRate, 0);
        }      
    }
}
