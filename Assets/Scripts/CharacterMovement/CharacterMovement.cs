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
        DestroyCharacters();
        gameLevel += 0.5f;
        LoadLevel2();
        LoadLevel3();
        LoadLevel4();
        LoadLevel5();
        LoadLevel6();
        LoadLevel7();
        LoadLevel8();
        LoadLevel9();
        LoadLevel10();
    }
    protected void DestroyCharacters()
    {
        var characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (var character in characters)
        {
            Destroy(character);
        }
    }
    protected void LoadLevel2()
    {
        if (gameLevel == 1)
        {
            gameManager = GameObject.Find("GameManager");
            Level2NonMovingCode level = gameManager.GetComponent<Level2NonMovingCode>();
            level.enabled = true;
        }
    }
    protected void LoadLevel3()
    {
        if (gameLevel == 2)
        {
            gameManager = GameObject.Find("GameManager");
            Level3NonMovingCode level = gameManager.GetComponent<Level3NonMovingCode>();
            level.enabled = true;
        }
    }
    protected void LoadLevel4()
    {
        if (gameLevel == 3)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.enabled = true;
        }
    }
    protected void LoadLevel5()
    {
        if (gameLevel == 4)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
    protected void LoadLevel6()
    {
        if (gameLevel == 5)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
    // Right character should be on bottom layer now
    protected void LoadLevel7()
    {
        if (gameLevel == 6)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
    protected void LoadLevel8()
    {
        if (gameLevel == 7)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
    protected void LoadLevel9()
    {
        if (gameLevel == 8)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
    protected void LoadLevel10()
    {
        if (gameLevel == 9)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.DisableCharacterMovementBouncyScript();
            level.CreateCharactersOnGrid();
        }
    }
}