using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
        public static ScoreManager instance;

        public TMP_Text scoreText;
        private int score = 0;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        public void AddScore(int amount)
        {
            score += amount;
            scoreText.text = "CARS DODGED: 0" + score;
        }
    

}
