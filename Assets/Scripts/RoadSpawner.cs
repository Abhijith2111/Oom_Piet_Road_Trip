using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [Header("Segment Settings")]
    public GameObject segmentPrefab;      
    public Transform player;              
    public int maxSegments = 3;           
    public float segmentLength = 840f;    

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private Vector3 nextSpawnPosition;

    void Start()
    {
        nextSpawnPosition = segmentPrefab.transform.position;

        for (int i = 0; i < maxSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.z > nextSpawnPosition.z - (segmentLength * (maxSegments - 1)))
        {
            SpawnSegment();
            DeleteOldSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(segmentPrefab, nextSpawnPosition, Quaternion.identity);
        activeSegments.Enqueue(newSegment);

        nextSpawnPosition += new Vector3(0, 0, segmentLength);
    }

    void DeleteOldSegment()
    {
        if (activeSegments.Count > maxSegments)
        {
            GameObject oldSegment = activeSegments.Dequeue();
            Destroy(oldSegment);
        }
    }
}
