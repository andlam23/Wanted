using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    // set character X and Y position values
    protected float characterXValue;
    protected float characterYValue;
    // set character speed for X and Y
    public Vector2 characterSpeed;
    // set boundaries for the game area
    protected float xBoundary = 9.625f;
    protected float yBoundary = 5.625f;
    // define waviness frequency and amplitude
    protected float frequency;
    protected float amplitude;
    void Start()
    {
        // getting the initial position where prefab is created
        characterXValue = gameObject.transform.position.x;
        characterYValue = gameObject.transform.position.y;
    }
    void Update()
    {
        // adding waviness to the movement if x or y of characterSpeed is 0, and no waviness if neither is 0
        if (characterSpeed.x != 0 && characterSpeed.y != 0)
        {
            // adding speed value over time to the X and Y axis position value
            characterXValue += characterSpeed.x * Time.deltaTime;
            characterYValue += characterSpeed.y * Time.deltaTime;
        }
        else if (characterSpeed.x == 0 && characterSpeed.y != 0)
        {
            frequency = 5.0f;
            amplitude = 3.0f;
            characterXValue = gameObject.transform.position.x + Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
            characterYValue += characterSpeed.y * Time.deltaTime;
        }
        else if (characterSpeed.y == 0 && characterSpeed.x != 0)
        {
            frequency = 2.0f;
            amplitude = 3.5f;
            characterXValue += characterSpeed.x * Time.deltaTime;
            characterYValue = gameObject.transform.position.y + Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
        }
            // setting new X and Y value to position
        gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        // resetting X and Y values if character goes out of bounds
        if (gameObject.transform.position.x >= xBoundary)
        {
            characterXValue = (characterXValue - 0.1f) * -1;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.y >= yBoundary)
        {
            characterYValue = (characterYValue - 0.1f) * -1;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.x <= -xBoundary)
        {
            characterXValue = -characterXValue - 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
        else if (gameObject.transform.position.y <= -yBoundary)
        {
            characterYValue = -characterYValue - 0.1f;
            gameObject.transform.position = new Vector2(characterXValue, characterYValue);
        }
    }
}