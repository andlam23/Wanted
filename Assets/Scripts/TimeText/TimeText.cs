using UnityEngine;
using TMPro;
using System;
public class TimeText : MonoBehaviour
{
    //Define TextMeshProUGUI time text
    private TextMeshProUGUI timeText;
    //Define starting time
    public static float time = 10;
    void Start()
    {
        //Get the TextMeshProUGUI component
        timeText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        //If time is greater than oe equal to 0 and next level buffer is false
        if (time >= 0 && !ClickFunctionality.nextLevelBuffer)
        {
            //Update the time on every frame
            UpdateTime();
        }
    }
    private void UpdateTime()
    {
        // If time is greater than 0,
        if (time > 0)
        {
            //Subtract the time between the current and previous frame from time
            time -= Time.deltaTime;
        }
        //Define a new line in the text box
        string newLine = Environment.NewLine;
        //Change time text to the new time remaining
        timeText.text = "Time" + newLine + Mathf.Round(time);
    }
}
