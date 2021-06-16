using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float currentTime;
    public float startinTime;
    [SerializeField] Text countdownText; 
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startinTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if(currentTime <= 0)
        {
            currentTime = 0;
        }
        countdownText.text = currentTime.ToString();
    }
}
