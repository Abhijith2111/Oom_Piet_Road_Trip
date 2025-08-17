using UnityEngine;

public class SegmentTrigger : MonoBehaviour
{
    private RoadGen roadGen;

    void Start()
    {
        roadGen = FindObjectOfType<RoadGen>();

        if (roadGen == null)
        {
            Debug.LogError("RoadGen not found in the scene!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roadGen.OnPlayerEnterNewSegment();
        }
    }
}
