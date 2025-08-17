using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPass : MonoBehaviour
{
    private Transform player;
    private bool hasScored = false;

    void Start()
    {
        // Automatically find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player has the tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check if player has passed this car on Z-axis
        if (!hasScored && player.position.z > transform.position.z + 5f)
        {
            hasScored = true;
            ScoreManager.instance.AddScore(1);
        }
    }
}
