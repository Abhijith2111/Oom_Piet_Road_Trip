using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    public static int CarCount = 0;
    [SerializeField] GameObject carsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carsDisplay.GetComponent<TMPro.TMP_Text>().text = "CARS DODGED: " + CarCount;
    }
}
