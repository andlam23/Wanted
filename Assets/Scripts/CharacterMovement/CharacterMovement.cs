using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    // set character X and Y position values
    protected float characterXValue;
    protected float characterYValue;
    // set character speed for X and Y
    public Vector2 characterSpeed;
    // set boundaries for the game area
    protected float xBoundary;
    protected float yBoundary;
    // define frequency, amplitude, and waviness
    protected float frequency;
    protected float amplitude;
    protected float waviness;
    // define reversing a direction
    protected int reverse = -1;
    // define a space buffer
    protected float buffer = 0.1f;
    // define not moving
    protected int notMoving = 0;
    // define GameManager
    protected GameObject gameManager;
    // define game level
    public static float gameLevel;
    void Start()
    {
        // set actual boundaries for game area
        xBoundary = 9.625f;
        yBoundary = 5.625f;
        GetPrefabStartPosition();
    }
    void Update()
    {
        // define formula for waviness
        waviness = Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
        CalculateNewPositionAndWaviness();
        SetNewPosition();
        KeepInGameArea();
    }
    protected void GetPrefabStartPosition()
    {
        // getting the initial position where prefab is created
        characterXValue = gameObject.transform.position.x;
        characterYValue = gameObject.transform.position.y;
    }
    protected void CalculateNewPosition()
    {
        // adding speed value over time to the X and Y axis position value
        characterXValue += characterSpeed.x * Time.deltaTime;
        characterYValue += characterSpeed.y * Time.deltaTime;
    }
    void CalculateYPositionAndHorizontalWaviness()
    {
        // calculate y position using character y speed and x position using waviness given frequency and amplitude
        frequency = 5.0f;
        amplitude = 3.0f;
        characterXValue += waviness;
        characterYValue += characterSpeed.y * Time.deltaTime;
    }
    void CalculateXPositionAndVerticalWaviness()
    {
        // calculate x position using character x speed and y position using waviness given frequency and amplitude
        frequency = 2.0f;
        amplitude = 3.5f;
        characterXValue += characterSpeed.x * Time.deltaTime;
        characterYValue += waviness;
    }
    void CalculateNewPositionAndWaviness()
    {
        // adding waviness to the movement if x or y of characterSpeed is 0, and no waviness if neither is 0, then determine new X and Y values
        if (characterSpeed.x != notMoving && characterSpeed.y != notMoving)
        {
            CalculateNewPosition();
        }
        else if (characterSpeed.x == notMoving && characterSpeed.y != notMoving)
        {
            CalculateYPositionAndHorizontalWaviness();
        }
        else if (characterSpeed.y == notMoving && characterSpeed.x != notMoving)
        {
            CalculateXPositionAndVerticalWaviness();
        }
    }
    protected void SetNewPosition()
    {
        // setting new X and Y value to position
        gameObject.transform.position = new Vector2(characterXValue, characterYValue);
    }
    // resetting X and Y values if character goes out of bounds
    void KeepInGameArea()
    {
        // if character goes past the right side boundary, make it appear from the left side boundary with a space buffer
        if (characterXValue >= xBoundary)
        {
            characterXValue = (characterXValue - buffer) * reverse;
            SetNewPosition();
        }
        // if character goes past the top side boundary, make it appear from the bottom side boundary with a space buffer
        else if (characterYValue >= yBoundary)
        {
            characterYValue = (characterYValue - buffer) * reverse;
            SetNewPosition();
        }
        // if character goes past the left side boundary, make it appear from the right side boundary with a space buffer
        else if (characterXValue <= -xBoundary)
        {
            characterXValue = reverse * characterXValue - buffer;
            SetNewPosition();
        }
        // if character goes past the bottom side boundary, make it appear from the top side boundary with a space buffer
        else if (characterYValue <= -yBoundary)
        {
            characterYValue = reverse * characterYValue - buffer;
            SetNewPosition();
        }
    }
    protected void OnMouseDown()
    {
        // if Right character is clicked with circle collider turned on, destroy all characters, add 0.5f to gameLevel, and attempt to load
        // next level. OnMouseDown calls twice because this script and CharacterMovementBouncy (which inherits from this script) are attached
        // to the Right character. Adding 0.5f twice results in adding 1.0f to the gameLevel, so on the second call the next level can load
        DestroyCharacters();
        gameLevel += 0.5f;
        LoadLevel();
    }
    // stores all characters in a var then destroys each of them
    protected void DestroyCharacters()
    {
        var characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (var character in characters)
        {
            Destroy(character);
        }
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
    }
    // finds the GameManager, accesses the NonMovingCode script, calls two functions to disable the CharacterMovementBouncy script on
    // character prefabs, then creates the characters
    protected void LoadNonMovingCodeLevel()
    {
        gameManager = GameObject.Find("GameManager");
        NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
        level.DisableCharacterMovementBouncyScript();
        level.CreateCharactersOnGrid();
    }
}