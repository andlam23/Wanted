using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickFunctionality : MonoBehaviour
{
    // define GameManager
    protected GameObject gameManager;
    // define game level
    public static float gameLevel;
    // define time buffer before the next level
    public static bool nextLevelBuffer;
    protected void OnMouseDown()
    {
        // start the display +5 coroutine
        StartCoroutine(DisplayPlus5());
        //
        gameLevel += 1;
        // if Right character is clicked with circle collider turned on, destroy all characters, add 1 to gameLevel, and attempt to load
        // next level
        StartCoroutine(DestroyCharactersAndPauseCountdownAndLoadLevel());
        //Add 5 seconds to time for completing previous level
        TimeText.time += 5;
        //Clamps the time between 0 and 20 seconds
        TimeText.time = Mathf.Clamp(TimeText.time, 0, 20);
    }
    public IEnumerator DisplayPlus5()
    {
        //Find the +5 gameobject, access the RectTransform component
        GameObject plus5 = GameObject.Find("+5");
        RectTransform rectTransform = plus5.GetComponent<RectTransform>();
        //Find the Cavnvas gameobject, access the RectTransform component
        GameObject canvas = GameObject.Find("Canvas");
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        //Convert transform.position of Right character to an anchored position
        Vector2 anchoredTransformPosition = Camera.main.WorldToScreenPoint(transform.position);
        //Define canvas offset
        Vector2 offset = canvasRectTransform.sizeDelta / 2;
        //Set anchored position of +5 gameobject to anchored position of Right character minus canvas offset
        rectTransform.anchoredPosition = anchoredTransformPosition - offset;
        //Access the +5 gameobject image component
        Image image = plus5.GetComponent<Image>();
        //Enable the image
        image.enabled = true;
        //Wait for 1 seconds
        yield return new WaitForSeconds(2);
        //Disable image component
        image.enabled = false;
    }
    // stores all characters in a var then destroys each of them
    public IEnumerator DestroyCharactersAndPauseCountdownAndLoadLevel()
    {
        // set buffer for next level to true
        nextLevelBuffer = true;
        //
        yield return new WaitForSeconds(2);
        //set buffer for next level to false
        nextLevelBuffer = false;
        //
        var characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (var character in characters)
        {
            Destroy(character);
        }
        LoadLevel();
    }
    protected void LoadLevel()
    {

        // if gameLevel is 1, load level 2 by finding the GameManager, accessing the level 2 script, and enabling it
        if (gameLevel == 1)
        {
            gameManager = GameObject.Find("GameManager");
            Level2NonMovingCode level = gameManager.GetComponent<Level2NonMovingCode>();
            level.enabled = true;
        }
        // if gameLevel is 2, load level 3 by finding the GameManager, accessing the level 3 script, and enabling it
        else if (gameLevel == 2)
        {
            gameManager = GameObject.Find("GameManager");
            Level3NonMovingCode level = gameManager.GetComponent<Level3NonMovingCode>();
            level.enabled = true;
        }
        // if gameLevel is 3, load level 4 by finding the GameManager, accessing the NonMovingCode script, and enabling it
        else if (gameLevel == 3)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.enabled = true;
        }
        // if the gameLevel is 4 through 9, load levels 5 through 10
        // Right character should be on bottom layer when gameLevel is 6, loading into level 7
        else if (gameLevel >= 4 && gameLevel < 10)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 10)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCode level = gameManager.GetComponent<MovingCode>();
            level.enabled = true;
        }
        else if (gameLevel == 11)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCodeWavy level = gameManager.GetComponent<MovingCodeWavy>();
            level.enabled = true;
        }
        else if (gameLevel == 12)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 13)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCodeBouncy level = gameManager.GetComponent<MovingCodeBouncy>();
            level.enabled = true;
        }
        else if (gameLevel == 14)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 15)
        {
            LoadMovingCodeLevel();
        }
        // THESE TWO LEVELS ARE USED TO TEST WAVY AND BOUNCY LEVEL FUNCTIONS
        else if (gameLevel == 16)
        {
            LoadMovingCodeWavyLevel();
        }
        else if (gameLevel == 17)
        {
            LoadMovingCodeBouncyLevel();
        }
        //gameLevel % 1 == 0 is true only if gameLevel is an integer, which solves for OnMouseDown() calling this function twice
        else if (gameLevel > 17 && gameLevel % 1 == 0)
        {
            int numberMovementPatterns = 4;
            int overload = 1;
            int level = Random.Range(1, numberMovementPatterns + overload);
            if (level == 1)
            {
                LoadNonMovingCodeLevel();
            }
            else if (level == 2)
            {
                LoadMovingCodeLevel();
            }
            else if (level == 3)
            {
                LoadMovingCodeWavyLevel();
            }
            else if (level == 4)
            {
                LoadMovingCodeBouncyLevel();
            }
        }
    }
    // finds the GameManager, accesses the NonMovingCode script, calls two functions to disable the CharacterMovementBouncy script on
    // character prefabs, then creates the characters
    protected void LoadNonMovingCodeLevel()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        gameManager = GameObject.Find("GameManager");
        NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
        level.DisableCharacterMovementBouncyScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 0.3f;
        level.randomOffsetY = 0.3f;
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeLevel()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        gameManager = GameObject.Find("GameManager");
        MovingCode level = gameManager.GetComponent<MovingCode>();
        level.DisableCharacterMovementBouncyScript();
        level.EnableCharacterMovementScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.DetermineCharacterSpeeds();
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeWavyLevel()
    {
        gameManager = GameObject.Find("GameManager");
        MovingCodeWavy level = gameManager.GetComponent<MovingCodeWavy>();
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        level.DisableCharacterMovementBouncyScript();
        level.EnableCharacterMovementScript();
        level.spawnRangeX = 9;
        level.spawnRangeY = 5;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.DetermineCharacterSpeeds();
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeBouncyLevel()
    {
        gameManager = GameObject.Find("GameManager");
        MovingCodeBouncy level = gameManager.GetComponent<MovingCodeBouncy>();
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        level.DisableCharacterMovementScript();
        level.EnableCharacterMovementBouncyScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.CreateCharactersOnGrid();
    }
}
