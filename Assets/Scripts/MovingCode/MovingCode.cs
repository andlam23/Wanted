using System.Collections.Generic;
using UnityEngine;
public class MovingCode : NonMovingCode
{
    //Define a list of character speeds
    public List<Vector2> characterSpeeds;
    void Start()
    {
        DisableCharacterMovementBouncyScript();
        EnableCharacterMovementScript();
        randomOffsetX = 1.0f;
        randomOffsetY = 1.0f;
        DetermineCharacterSpeeds(); 
        CreateCharactersOnGrid();
    }
    protected new void CreateRightCharacter()
    {
        //Selecting a random right character and random spawn position with a random offset, instantiating it, and setting it to the bottom layer
        int rightCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
        //Setting the right character index as the Wanted character index
        WantedCharacter wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacter>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.SetWantedCharacter());
        //
        int gridIndex = Random.Range(firstGridIndex, grid.Count);
        Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
        Vector2 rightSpawnPosition = grid[gridIndex] + randomOffset;
        GameObject rightCharacter = (GameObject)Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        Renderer rightCharacterRenderer = rightCharacter.GetComponent<Renderer>();
        int behindOtherCharacters = -1;
        rightCharacterRenderer.sortingOrder = behindOtherCharacters;
        //Getting the character movement class from the instantiated right character and setting its speed to the predetermined character speed
        CharacterMovement rightCharacterMovement = rightCharacter.GetComponent<CharacterMovement>();
        rightCharacterMovement.characterSpeed = characterSpeeds[rightCharacterIndex];
        //Enable the Right character's collider
        EnableRightCharacterCollider(rightCharacter);
        //Removing the right character speed from the list of character speeds
        characterSpeeds.RemoveAt(rightCharacterIndex);
        //Removing the right character from the list of characters
        characterPrefabs.RemoveAt(rightCharacterIndex);
        //Removing the Vector2 from the grid list
        grid.RemoveAt(gridIndex);
    }
    protected new void CreateWrongCharacters()
    {
        //Selecting a random wrong character and random spawn position with a random offset, then instantiating it, and setting it to a random non-bottom layer
        for (int i = 0; i < numberWrongCharacters; i++)
        {
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            int gridIndex = Random.Range(firstGridIndex, grid.Count);
            Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
            Vector2 wrongSpawnPosition = grid[gridIndex] + randomOffset;
            GameObject wrongCharacter = (GameObject)Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            //Getting the character movement class from the instantiated wrong character and setting its speed to the predetermined character speed
            CharacterMovement wrongCharacterMovement = wrongCharacter.GetComponent<CharacterMovement>();
            wrongCharacterMovement.characterSpeed = characterSpeeds[wrongCharacterIndex];
            //
            Renderer wrongCharacterRenderer = wrongCharacter.GetComponent<Renderer>();
            int firstUniqueWrongCharacter = 0;
            int numberUniqueWrongCharacters = 3;
            int randomSortingOrder = Random.Range(firstUniqueWrongCharacter, numberUniqueWrongCharacters);
            wrongCharacterRenderer.sortingOrder = randomSortingOrder;
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    protected new void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        int positiveAndNegativeRange = 2;
        int zeroRange = 1;
        int numberRightCharacters = 1;
        int maxNumberWrongCharacters = ((spawnRangeX * positiveAndNegativeRange) + zeroRange) * ((spawnRangeY * positiveAndNegativeRange) + zeroRange) - numberRightCharacters;
        int minNumberWrongCharacters = 100;
        int overload = 1;
        //Randomizing how many wrong characters to instantiate
        numberWrongCharacters = Random.Range(minNumberWrongCharacters, maxNumberWrongCharacters + overload);
        CreateWrongCharacters();
    }
    protected void DetermineCharacterSpeeds()
    {
        //Determine character speeds
        for (int i = 0; i < characterPrefabs.Count; i++)
        {
            float randomXValue = Random.Range(1.0f, 4.0f);
            int positiveOrNegativeXValue = Random.Range(0, 2) * 2 - 1;
            float randomYValue = Random.Range(1.0f, 4.0f);
            int positiveOrNegativeYValue = Random.Range(0, 2) * 2 - 1;
            Vector2 speedVector2 = new Vector2(randomXValue * positiveOrNegativeXValue, randomYValue * positiveOrNegativeYValue);
            characterSpeeds.Add(speedVector2);
        }
    }
    protected void EnableCharacterMovementScript()
    {
        int numberOfCharacterPrefabs = 4;
        //Enabling the CharacterMovement script for each prefab
        for (int i = 0; i < numberOfCharacterPrefabs; i++)
        {
            CharacterMovement characterMovementScript = characterPrefabs[i].GetComponent<CharacterMovement>();
            characterMovementScript.enabled = true;
        }
    }
}