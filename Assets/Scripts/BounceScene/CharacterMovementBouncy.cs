using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovementBouncy : MonoBehaviour
{
    // set character X and Y position values
    protected float characterXValue;
    protected float characterYValue;
    // set character speed for X and Y
    public Vector2 characterSpeed;
    // set boundaries for the game area
    protected float xBoundary = 8.5f;
    protected float yBoundary = 4.5f;
    // define waviness frequency and amplitude
    protected float frequency;
    protected float amplitude;
    void Start()
    {
        // set random character speed
        characterSpeed = new Vector2(Random.Range(1.0f, 4.0f) * (Random.Range(0, 2) * 2 - 1), Random.Range(1.0f, 4.0f) * (Random.Range(0, 2) * 2 - 1));
        // getting the initial position where prefab is created
        characterXValue = gameObject.transform.position.x;
        characterYValue = gameObject.transform.position.y;
    }
    void Update()
    {
        // adding speed value over time to the X and Y axis position value
        characterXValue += characterSpeed.x * Time.deltaTime;
        characterYValue += characterSpeed.y * Time.deltaTime;
        // setting new X and Y value to position
        gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        // making character bounce off boundaries
        if (gameObject.transform.position.x >= xBoundary)
        {
            characterSpeed.x = -characterSpeed.x;
            characterXValue = characterXValue - 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.y >= yBoundary)
        {
            characterSpeed.y = -characterSpeed.y;
            characterYValue = characterYValue - 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.x <= -xBoundary)
        {
            characterSpeed.x = -characterSpeed.x;
            characterXValue = characterXValue + 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.y <= -yBoundary)
        {
            characterSpeed.y = -characterSpeed.y;
            characterYValue = characterYValue + 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
    }
}