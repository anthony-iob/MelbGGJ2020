using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    public GameObject flood;
    public Transform minHeight;
    public Transform maxHeight;
    // Update is called once per frame
    void Update()
    {
        var minHeightPosition = minHeight.position;
        var maxHeightPosition = maxHeight.position;
        var percentage = GameManager.instance.GetFloodPercentage();
        Debug.Log(percentage);
        var newHeight = (maxHeightPosition.y - minHeightPosition.y) * percentage;
        var up = new Vector3(0, newHeight, 0);
        flood.transform.position = minHeightPosition + up;
    }
}
