using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class TimeText : MonoBehaviour
{
    //Define TextMeshProUGUI time text
    private TextMeshProUGUI timeText;
    //Define starting time
    public static float time = 10;
    //Define GameOver gameobject
    private GameObject gameOver;
    //Define whether the game is active
    public static bool isGameActive;
    void Start()
    {
        isGameActive = true;
        //Get the TextMeshProUGUI component
        timeText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        //If time is greater than or equal to 0 and next level buffer is false
        if (time >= 0 && !ClickFunctionality.nextLevelBuffer)
        {
            //Update the time on every frame
            UpdateTime();
        }
        //If time is less than 0, set time to 0 and start the game over sequence coroutine
        if (time < 0)
        {
            time = 0;
            StartCoroutine(StartGameOverSequence());
        }
    }
    //Define a coroutine to start the game over sequence
    public IEnumerator StartGameOverSequence()
    {
        // Set game to not active
        isGameActive = false;
        //Set the right character game object to the default layer
        ClickFunctionality.rightCharacterGameObject.layer = 0;
        //Wait for 1.05 seconds
        yield return new WaitForSeconds(1.05f);
        //Destroy the wrong characters
        ClickFunctionality.DestroyWrongCharacters();
        //Wait for 2 seconds
        yield return new WaitForSeconds(2);
        //Destroy the right character
        Destroy(ClickFunctionality.rightCharacterGameObject);
        //GameOver image will appear
        EnableGameOverImage();
    }
    private void EnableGameOverImage()
    {
        //Find the GameOver gameobject, access the image component, and enable it
        gameOver = GameObject.Find("GameOver");
        Image gameOverImage = gameOver.GetComponent<Image>();
        gameOverImage.enabled = true;
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
