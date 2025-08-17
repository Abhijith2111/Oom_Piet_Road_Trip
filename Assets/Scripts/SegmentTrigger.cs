using UnityEngine;

public class SegmentTrigger : MonoBehaviour
{
    private RoadGen roadGen;

    void Start()
    {
        roadGen = FindObjectOfType<RoadGen>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roadGen.OnPlayerEnterNewSegment();
        }
    }
}
