using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WantedCharacter : MonoBehaviour
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

        image.enabled = true;
        image.sprite = characters[wantedCharacterIndex];

        wantedCharacterBackground.SetActive(true);
        wantedCharacterPoster.SetActive(true);

        //Change text to Level and level number, gameLevel is 1 behind the current level so add an overload of 1, then enable the Level text
        float overload = 1.0f;
        levelText.text = "Level " + (CharacterMovement.gameLevel + overload);
        levelText.enabled = true;
        //

        yield return new WaitForSeconds(1);

        //Disable the Level text
        levelText.enabled = false;
        //

        image.enabled = false;

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
        if (CharacterMovement.gameLevel == 1)
        {
            EnableStarImage((int)CharacterMovement.gameLevel - starOverload);
        }
        if (CharacterMovement.gameLevel == 2)
        {
            EnableStarImage((int)CharacterMovement.gameLevel - starOverload);
        }
        if (CharacterMovement.gameLevel == 3)
        {
            EnableStarImage((int)CharacterMovement.gameLevel - starOverload);
        }
        if (CharacterMovement.gameLevel == 4)
        {
            EnableStarImage((int)CharacterMovement.gameLevel - starOverload);
        }
        if (CharacterMovement.gameLevel == 5)
        {
            DisableStarImage(CharacterMovement.gameLevel);
            EnableMultiStarImage();
        }
        if (CharacterMovement.gameLevel >= 6)
        {
            //Return multiple of 5 based on game level: 5 for 6-10, 10 for 11-15, 15 for 16-20...
            int multipleOf5 = (int)Mathf.Floor((CharacterMovement.gameLevel - 1) / 5) * 5;
            //Return star number to enable
            int starNumber = (int)CharacterMovement.gameLevel - multipleOf5;
            EnableStarImage(starNumber);
            DisableStarImage(CharacterMovement.gameLevel);
        }
    }
    protected void EnableStarImage(int starNumber)
    {
        Image starImage = stars[starNumber].GetComponent<Image>();
        starImage.enabled = true;
    }
    protected void DisableStarImage(float gameLevel)
    {
        bool gameLevelIsDivisibleBy5 = gameLevel % 5 == 0;

        if (gameLevelIsDivisibleBy5)
        {
            foreach (var star in stars)
            {
                star.GetComponent<Image>().enabled = false;
            }
        }
    }
    protected void EnableMultiStarImage()
    {
        multiStar = GameObject.Find("MultiStarIcon");
        Image multiStarImage = multiStar.GetComponent<Image>();
        multiStarImage.enabled = true;
    }
}