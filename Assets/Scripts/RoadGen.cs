using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    [Header("Segment Settings")]
    public GameObject segmentPrefab;        // Prefab of your segment
    public Transform spawnPoint;            // Where new segments spawn
    public float segmentLength = 20f;       // Distance between segments
    public float spawnInterval = 2f;        // Time between spawns (seconds)

    [Header("Cleanup Settings")]
    public float segmentLifetime = 10f;     // How long before old segments are deleted

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // Spawn segments at intervals
        if (timer >= spawnInterval)
        {
            SpawnSegment();
            timer = 0f;
        }
    }

    void SpawnSegment()
    {
        // Spawn new segment
        GameObject newSegment = Instantiate(segmentPrefab, spawnPoint.position, Quaternion.identity);

        // Add to queue for cleanup
        activeSegments.Enqueue(newSegment);

        // Move spawn point forward
        spawnPoint.position += Vector3.forward * segmentLength;

        // Schedule deletion after lifetime
        StartCoroutine(DeleteAfterTime(newSegment, segmentLifetime));
    }

    System.Collections.IEnumerator DeleteAfterTime(GameObject segment, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (segment != null)
        {
            activeSegments.Dequeue();
            Destroy(segment);
        }
    }
}
