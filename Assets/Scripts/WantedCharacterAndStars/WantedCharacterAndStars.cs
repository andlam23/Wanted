using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WantedCharacterAndStars : MonoBehaviour
{
    //Define a Wanted character index for the game manager script to assign a value to
    public int wantedCharacterIndex;
    //Define a list of characters
    public List<Sprite> characters;
    //Define the image component as Image
    public Image image;
    //Define Wanted character background and poster gameobjects
    public GameObject wantedCharacterBackground;
    public GameObject wantedCharacterPoster;
    //Define Level text
    public TextMeshProUGUI levelText;
    // define list of star gameobjects
    public List<GameObject> stars;
    // define star overload number
    private int starOverload = 1;
    // define MultiStar gameobject
    private GameObject multiStar;
    // define MultiStar text
    public TextMeshProUGUI multiStarText;
    void Start()
    {
        //Get the image component
        image = gameObject.GetComponent<Image>();
        // Add star gameobjects to stars list
        AddStars();
    }
    //Define a coroutine to set the UI image to the character assigned the Wanted character index and also setting the 
    //Wanted character background and poster to active, show them for 3 seconds, then disable them
    public IEnumerator DisplayWantedCharacterAndGameLevelAndStar()
    {
        //Create star for completing previous level
        CreateStar();

        //Enable the Wanted character image and change the sprite to the Wanted character
        image.enabled = true;
        image.sprite = characters[wantedCharacterIndex];

        //Set the Wanted character background and poster active
        wantedCharacterBackground.SetActive(true);
        wantedCharacterPoster.SetActive(true);

        //Change text to Level and level number, gameLevel is 1 behind the current level so add an overload of 1, then enable the Level text
        float overload = 1.0f;
        levelText.text = "Level " + (ClickFunctionality.gameLevel + overload);
        levelText.enabled = true;

        //Wait for 1 seconds
        yield return new WaitForSeconds(1);

        //Disable the Level text
        levelText.enabled = false;

        //Disable the Wanted character image
        image.enabled = false;

        //Set the Wanted character background and poster to inactive
        wantedCharacterBackground.SetActive(false);
        wantedCharacterPoster.SetActive(false);
    }
    //Add star gameobjects to stars list
    protected void AddStars()
    {
        stars.AddRange(GameObject.FindGameObjectsWithTag("Star"));
    }
    //Create star for completing previous level
    protected void CreateStar()
    {
        //Show the first four stars
        if (ClickFunctionality.gameLevel >= 1 && ClickFunctionality.gameLevel < 5)
        {
            EnableStarImage((int)ClickFunctionality.gameLevel - starOverload);
        }
        //Disable the first four stars, enable the MultiStar, and update its score
        else if (ClickFunctionality.gameLevel == 5)
        {
            DisableStarImage(ClickFunctionality.gameLevel);
            EnableMultiStarImage();
            UpdateMultiStarScore(ClickFunctionality.gameLevel);
        }
        else if (ClickFunctionality.gameLevel >= 6)
        {
            //Return multiple of 5 based on game level: 5 for 6-10, 10 for 11-15, 15 for 16-20...
            int multipleOf5 = (int)Mathf.Floor((ClickFunctionality.gameLevel - 1) / 5) * 5;
            //Determine star number to enable by subtracting the multiple of 5 from the game Level
            int starNumber = (int)ClickFunctionality.gameLevel - multipleOf5;
            //Enable the corresponding star
            EnableStarImage(starNumber);
            //Disable the star images based on the game level
            DisableStarImage(ClickFunctionality.gameLevel);
            //Update the MultiStar score based on the game level
            UpdateMultiStarScore(ClickFunctionality.gameLevel);
        }
    }
    protected void EnableStarImage(int starNumber)
    {
        //Access the image component from the star in the list and enable it
        Image starImage = stars[starNumber].GetComponent<Image>();
        starImage.enabled = true;
    }
    protected void DisableStarImage(float gameLevel)
    {
        //Define a boolean that is true when the gamelevel has no remainder when divided by 5, and false when there is a remainder
        bool gameLevelIsDivisibleBy5 = gameLevel % 5 == 0;

        //If the boolean is true, disable the image component for each star in the star list
        if (gameLevelIsDivisibleBy5)
        {
            foreach (var star in stars)
            {
                star.GetComponent<Image>().enabled = false;
            }
        }
    }
    protected void UpdateMultiStarScore(float gameLevel)
    {
        //Define a boolean that is true when the gamelevel has no remainder when divided by 5, and false when there is a remainder
        bool gameLevelIsDivisibleBy5 = gameLevel % 5 == 0;

        //If the boolean is true, update the text to be the last multiple of 5 the game level was
        if (gameLevelIsDivisibleBy5)
        {
            multiStarText.text = "" + (ClickFunctionality.gameLevel);
        }
    }
    protected void EnableMultiStarImage()
    {
        //Find the MultiStarIcon gameobject, access the image component, and enable it
        multiStar = GameObject.Find("MultiStarIcon");
        Image multiStarImage = multiStar.GetComponent<Image>();
        multiStarImage.enabled = true;
    }
}