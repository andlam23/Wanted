using System.Collections.Generic;
using UnityEngine;
public class MovingCodeWavy : MovingCode
{
    //Define a list of possible vertical and horizontal character speeds
    public List<Vector2> possibleCharacterSpeeds;
    void Start()
    {
        DisableCharacterMovementBouncyScript();
        EnableCharacterMovementScript();
        spawnRangeX = 9;
        spawnRangeY = 5;
        randomOffsetX = 1.0f;
        randomOffsetY = 1.0f;
        DetermineCharacterSpeeds(); 
        CreateCharactersOnGrid();
    }
    protected new void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        int positiveAndNegativeRange = 2;
        int zeroRange = 1;
        int numberRightCharacters = 1;
        int maxNumberWrongCharacters = ((spawnRangeX * positiveAndNegativeRange) + zeroRange) * ((spawnRangeY * positiveAndNegativeRange) + zeroRange) - numberRightCharacters;
        int minNumberWrongCharacters = 170;
        int overload = 1;
        //Randomizing how many wrong characters to instantiate
        numberWrongCharacters = Random.Range(minNumberWrongCharacters, maxNumberWrongCharacters + overload);
        CreateWrongCharacters();
    }
    protected new void DetermineCharacterSpeeds()
    {
        //Determine character speeds by randomly choosing between a randomly generated horizontal and vertical speed
        for (int i = 0; i < characterPrefabs.Count; i++)
        {
            float randomXValue = Random.Range(1.0f, 2.0f);
            int positiveOrNegativeXValue = Random.Range(0, 2) * 2 - 1;
            int zeroYValue = 0;
            int zeroXValue = 0;
            float randomYValue = Random.Range(3.0f, 4.0f);
            int positiveOrNegativeYValue = Random.Range(0, 2) * 2 - 1;

            Vector2 horizontalSpeedVector2 = new Vector2(randomXValue * positiveOrNegativeXValue, zeroYValue);
            possibleCharacterSpeeds.Add(horizontalSpeedVector2);
            Vector2 verticalSpeedVector2 = new Vector2(zeroXValue, randomYValue * positiveOrNegativeYValue);
            possibleCharacterSpeeds.Add(verticalSpeedVector2);

            int selectHorizontalSpeedVector2 = 0;
            int selectVerticalSpeedVector2 = 1;
            int overload = 1;

            int possibleCharacterSpeedsIndex = Random.Range(selectHorizontalSpeedVector2, selectVerticalSpeedVector2 + overload);
            Vector2 speedVector2 = possibleCharacterSpeeds[possibleCharacterSpeedsIndex];
            characterSpeeds.Add(speedVector2);

            possibleCharacterSpeeds.RemoveAt(possibleCharacterSpeedsIndex);
            int remainingCharacterSpeedIndex = 0;
            possibleCharacterSpeeds.RemoveAt(remainingCharacterSpeedIndex);
        }
    }
}