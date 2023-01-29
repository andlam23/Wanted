using UnityEngine;
public class CharacterMovementBouncy : CharacterMovement
{
    // define bounce
    private float bounce;
    void Start()
    {
        // set bounce amount
        bounce = 0.1f;
        // set actual boundaries for game area
        xBoundary = 8.5f;
        yBoundary = 4.5f;
        SetRandomCharacterSpeed();
        GetPrefabStartPosition();
    }
    void Update()
    {
        CalculateNewPosition();
        SetNewPosition();
        BounceOffGameArea();
    }
    void SetRandomCharacterSpeed()
    {
        // set random character speed
        float randomXValue = Random.Range(1.0f, 4.0f);
        int positiveOrNegativeXValue = Random.Range(0, 2) * 2 - 1;
        float randomYValue = Random.Range(1.0f, 4.0f);
        int positiveOrNegativeYValue = Random.Range(0, 2) * 2 - 1;
        characterSpeed = new Vector2(randomXValue * positiveOrNegativeXValue, randomYValue * positiveOrNegativeYValue);
    }
   void BounceOffGameArea()
   {
        // making character bounce off right side boundary and make it go left
        if (gameObject.transform.position.x >= xBoundary)
        {
            ReverseXDirection();
            characterXValue = characterXValue - bounce;
            SetNewPosition();
        }
        // making character bounce off top side boundary and make it go down
        else if (gameObject.transform.position.y >= yBoundary)
        {
            ReverseYDirection();
            characterYValue = characterYValue - bounce;
            SetNewPosition();
        }
        // making character bounce off left side boundary and make it go right
        else if (gameObject.transform.position.x <= -xBoundary)
        {
            ReverseXDirection();
            characterXValue = characterXValue + bounce;
            SetNewPosition();
        }
        // making character bounce off bottom side boundary and make it go up
        else if (gameObject.transform.position.y <= -yBoundary)
        {
            ReverseYDirection();
            characterYValue = characterYValue + bounce;
            SetNewPosition();
        }
   }
   void ReverseXDirection()
   {
        // reversing the x direction of the character
        characterSpeed.x = reverse * characterSpeed.x;
   }
   void ReverseYDirection()
   {
        // reversing the y direction of the character
        characterSpeed.y = reverse * characterSpeed.y;
   }    
}