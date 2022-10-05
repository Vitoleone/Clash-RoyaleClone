using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : MonoBehaviour
{
    float poolingTime = 0.5f;
    float time;
    Text fpsText;
    int frameCount;
    // Update is called once per frame
    private void Awake()
    {
        fpsText = GetComponent<Text>();
    }
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= poolingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = "Fps:" + frameRate.ToString();
            time -= poolingTime;
            frameCount = 0;
        }
    }
}
