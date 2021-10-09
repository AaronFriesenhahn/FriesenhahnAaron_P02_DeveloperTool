using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNG : MonoBehaviour
{
    int rngNumber;
    public int Percentage;
    public int StatIncrease;
    int stat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rngNumber = Random.Range(0, 101);   
            Debug.Log("Level up!");
            stat += 1;
            Debug.Log("Stat increased. " + stat);
            Debug.Log(Percentage + "% that stat will increase by 1 again.");
            Debug.Log("Number chosen for rngNumber: " + rngNumber);
            if (rngNumber < Percentage)
            {
                stat += 1;
                Debug.Log("Stat has  increased again! " + stat);
            }

        }
    }
}
