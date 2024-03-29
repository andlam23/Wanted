using System.Collections.Generic;
using UnityEngine;
public class MovingCode : NonMovingCode
{
    //Define a list of character speeds
    public List<Vector2> characterSpeeds;
    void Start()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        DisableCharacterMovementBouncyScript();
        EnableCharacterMovementScript();
        spawnRangeX = 8;
        spawnRangeY = 4;
        randomOffsetX = 1.0f;
        randomOffsetY = 1.0f;
        DetermineCharacterSpeeds(); 
        CreateCharactersOnGrid();
    }
    protected void SetWantedCharacterIndex(int characterIndex)
    {
        //Setting the right character index as the Wanted character index
        WantedCharacterAndStars wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacterAndStars>();
        wantedCharacter.wantedCharacterIndex = characterIndex;
        StartCoroutine(wantedCharacter.DisplayWantedCharacterAndGameLevelAndStar());
    }
    protected int DetermineCharacterGridIndex()
    {
        int characterGridIndex = Random.Range(firstGridIndex, grid.Count);
        return characterGridIndex;
    }
    protected Vector2 DetermineCharacterSpawnPosition(int gridIndex)
    {
        
        //Determine the spawn position for a character
        Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
        Vector2 spawnPosition = grid[gridIndex] + randomOffset;
        return spawnPosition;
    }
    protected void SetToBottomLayer(GameObject rightCharacterGameObject)
    {
        //Set the sorting order of the Right character to the bottom layer
        Renderer rightCharacterRenderer = rightCharacterGameObject.GetComponent<Renderer>();
        int behindOtherCharacters = -1;
        rightCharacterRenderer.sortingOrder = behindOtherCharacters;
    }
    protected void SetCharacterSpeed(GameObject characterGameObject, int characterIndex)
    {
        //Getting the character movement class from the instantiated character and setting its speed to the predetermined character speed
        CharacterMovement characterMovement = characterGameObject.GetComponent<CharacterMovement>();
        characterMovement.characterSpeed = characterSpeeds[characterIndex];
    }
    protected void RemoveFromLists(int characterIndex, int characterGridIndex)
    {
        //Removing the right character speed from the list of character speeds
        characterSpeeds.RemoveAt(characterIndex);
        //Removing the right character from the list of characters
        characterPrefabs.RemoveAt(characterIndex);
        //Removing the Vector2 from the grid list
        grid.RemoveAt(characterGridIndex);
    }
    protected new void CreateRightCharacter()
    {
        //Selecting a random right character and random spawn position with a random offset, instantiating it, and setting it to the bottom layer
        int rightCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
        SetWantedCharacterIndex(rightCharacterIndex);
        int rightCharacterGridIndex = DetermineCharacterGridIndex();
        Vector2 rightSpawnPosition = DetermineCharacterSpawnPosition(rightCharacterGridIndex);
        GameObject rightCharacter = (GameObject)Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        //Setting right character gameobject to right character
        ClickFunctionality.rightCharacterGameObject = rightCharacter;
        //Accessing the Right character's SetCharacterLayer script and setting isRightCharacter bool to true
        SetCharacterLayer rightCharacterSetCharacterLayer = rightCharacter.GetComponent<SetCharacterLayer>();
        rightCharacterSetCharacterLayer.isRightCharacter = true;
        SetToBottomLayer(rightCharacter);
        SetCharacterSpeed(rightCharacter, rightCharacterIndex);
        RemoveFromLists(rightCharacterIndex, rightCharacterGridIndex);
    }
    protected void SetToRandomNonBottomLayer(GameObject wrongCharacterGameObject)
    {
        Renderer wrongCharacterRenderer = wrongCharacterGameObject.GetComponent<Renderer>();
        int firstUniqueWrongCharacter = 0;
        int numberUniqueWrongCharacters = 3;
        int randomSortingOrder = Random.Range(firstUniqueWrongCharacter, numberUniqueWrongCharacters);
        wrongCharacterRenderer.sortingOrder = randomSortingOrder;
    }
    protected new void CreateWrongCharacters()
    {
        //Selecting a random wrong character and random spawn position with a random offset, then instantiating it, and setting it to a random non-bottom layer
        for (int i = 0; i < numberWrongCharacters; i++)
        {
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            int wrongCharacterGridIndex = DetermineCharacterGridIndex();
            Vector2 wrongSpawnPosition = DetermineCharacterSpawnPosition(wrongCharacterGridIndex);
            GameObject wrongCharacter = (GameObject)Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            SetToRandomNonBottomLayer(wrongCharacter);
            SetCharacterSpeed(wrongCharacter, wrongCharacterIndex);
            //Removing the Vector2 from the grid list
            grid.RemoveAt(wrongCharacterGridIndex);
        }
    }
    public new void CreateCharactersOnGrid()
    {
        //Removing all grid spaces from the grid before recreating the grid spaces (to prevent duplicate grid spaces)
        grid.Clear();
        //
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
    public void DetermineCharacterSpeeds()
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
    public void EnableCharacterMovementScript()
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