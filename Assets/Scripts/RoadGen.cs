using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    [System.Serializable]
    public class SegmentSet
    {
        public GameObject segmentPrefab;   
        public float duration = 10f;       
    }

    [Header("Segment Settings")]
    public Transform spawnPoint;             
    public float segmentLength = 20f;        
    public float spawnInterval = 2f;         

    [Header("Cleanup Settings")]
    public float segmentLifetime = 10f;      

    [Header("Sequence Settings")]
    public SegmentSet[] segmentSequence;     

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private float spawnTimer;
    private float setTimer;
    private int currentSetIndex = 0;

    void Update()
    {
        if (segmentSequence.Length == 0) return;

        spawnTimer += Time.deltaTime;
        setTimer += Time.deltaTime;

        // Spawn segments at intervals
        if (spawnTimer >= spawnInterval)
        {
            SpawnSegment(segmentSequence[currentSetIndex].segmentPrefab);
            spawnTimer = 0f;
        }

        // Move to next set after its duration
        if (setTimer >= segmentSequence[currentSetIndex].duration)
        {
            currentSetIndex++;
            if (currentSetIndex >= segmentSequence.Length)
                currentSetIndex = 0; 

            setTimer = 0f;
        }
    }

    void SpawnSegment(GameObject prefab)
    {
        GameObject newSegment = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

       
        activeSegments.Enqueue(newSegment);
  
        spawnPoint.position += Vector3.forward * segmentLength;

        StartCoroutine(DeleteAfterTime(newSegment, segmentLifetime));
    }

    IEnumerator DeleteAfterTime(GameObject segment, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (segment != null)
        {
            activeSegments.Dequeue();
            Destroy(segment);
        }
    }
}
