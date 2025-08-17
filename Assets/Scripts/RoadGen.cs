using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour
{
    [Header("Segments (Scene Objects as Templates)")]
    public GameObject segment1;   
    public GameObject segment2;   
    public Transform spawnPoint;  

    [Header("Player Reference")]
    public Transform player;

    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    private bool segment2Spawned = false;

    void Start()
    {=
        for (int i = 0; i < 5; i++)
        {
            SpawnSegment(segment1);
        }

        Invoke("SpawnSegment2", 20f);
    }

    void SpawnSegment(GameObject template)
    {
        void SpawnSegment(GameObject template)
        {
            if (template == null) return;

            GameObject newSegment = Instantiate(template);
            newSegment.SetActive(true);

            float spawnY = template.transform.position.y;

            newSegment.transform.position = new Vector3(0f, spawnY, spawnPoint.position.z);

            activeSegments.Enqueue(newSegment);

            float segLength = GetSegmentLength(template);
            spawnPoint.position += Vector3.forward * segLength;

            Debug.Log($"Spawned {template.name} at {newSegment.transform.position}, length={segLength}");
        }

    }

    void SpawnSegment2()
    {
        if (!segment2Spawned)
        {
            SpawnSegment(segment2);
            segment2Spawned = true;
        }
    }

    public void OnPlayerEnterNewSegment()
    {
        if (segment2Spawned)
        {
            SpawnSegment(segment1);
        }

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

    float GetSegmentLength(GameObject segment)
    {
        Renderer rend = segment.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            return rend.bounds.size.z;
        }
        else
        {
            Debug.LogWarning($"No Renderer found on {segment.name}, defaulting length to 20");
            return 20f;
        }
    }
}
