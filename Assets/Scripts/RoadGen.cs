using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    [Header("Segments")]
    public GameObject segment1Prefab;   // Segment1 prefab
    public GameObject segment2Prefab;   // Segment2 prefab
    public Transform spawnPoint;        // Where new segments spawn
    public float segmentLength = 20f;   // Distance between segments

    [Header("Player Reference")]
    public Transform player; // Used for cleanup timing

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private bool segment2Spawned = false;

    void Start()
    {
        // Spawn 5 Segment1s at the start
        for (int i = 0; i < 5; i++)
        {
            SpawnSegment(segment1Prefab);
        }

        // Schedule Segment2 after 20s
        Invoke("SpawnSegment2", 20f);
    }

    void SpawnSegment(GameObject prefab)
    {
        GameObject newSegment = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        activeSegments.Enqueue(newSegment);

        // Move spawn point forward for next segment
        spawnPoint.position += Vector3.forward * segmentLength;
    }

    void SpawnSegment2()
    {
        if (!segment2Spawned)
        {
            SpawnSegment(segment2Prefab);
            segment2Spawned = true;
        }
    }

    public void OnPlayerEnterNewSegment()
    {
        // Spawn next segment (Segment1 unless Segment2 hasn’t been spawned yet and timer triggered)
        if (segment2Spawned)
        {
            SpawnSegment(segment1Prefab);
        }

        // Delete oldest segment after 3 seconds
        if (activeSegments.Count > 0)
        {
            GameObject oldSegment = activeSegments.Dequeue();
            StartCoroutine(DeleteAfterDelay(oldSegment, 3f));
        }
    }

    IEnumerator DeleteAfterDelay(GameObject segment, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (segment != null)
        {
            Destroy(segment);
        }
    }
}
