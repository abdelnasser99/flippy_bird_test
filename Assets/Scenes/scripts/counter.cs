using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    float currenttime=0f;
    float startingtime=3f;
    public Text countdown;
    private void Start()
    {
        currenttime = startingtime;
    }
    private void Update()
    {
        currenttime -= Time.deltaTime;
        countdown.text = currenttime.ToString("0");
        if (currenttime == 0)
        {

        }
    }
}
