using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingCodeWavy : MonoBehaviour
{
    //Define a list of characters
    public List<GameObject> characterPrefabs;
    //Define a list of Vector2's to create a grid
    public List<Vector2> grid;
    //Character spawn area
    protected int spawnRangeX = 9;
    protected int spawnRangeY = 5;
    //Number of wrong characters to instantiate
    protected int numberWrongCharacters;
    //Define a list of character speeds
    public List<Vector2> characterSpeeds;
    //Define a list of possible vertical and horizontal character speeds
    public List<Vector2> possibleCharacterSpeeds;

    //REUSEABLE: Define a Wanted character gameobject
    public GameObject wantedCharacterImage;

    void Start()
    {
        DetermineCharacterSpeeds(); 
        CreateCharactersOnGrid();
    }
    protected void CreateGrid()
    {
        //Creating a grid of Vector2's from -8,-4 to 8,4 and adding it to the grid list
        for (int i = -spawnRangeX; i < spawnRangeX + 1; i++)
        {
            for (int j = -spawnRangeY; j < spawnRangeY + 1; j++)
            {
                Vector2 gridVector2 = new Vector2(i, j);
                grid.Add(gridVector2);
            }
        }
    }
    protected void CreateRightCharacter()
    {
        //Selecting a random right character and random spawn position with a random offset, instantiating it, and setting it to the bottom layer
        int rightCharacterIndex = Random.Range(0, characterPrefabs.Count);

        //REUSABLE: Setting the right character index as the Wanted character index
        WantedCharacter wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacter>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.SetWantedCharacter());

        int gridIndex = Random.Range(0, grid.Count);
        float randomOffsetX = 1.0f;
        float randomOffsetY = 1.0f;
        Vector2 randomOffset = new Vector2(Random.Range(0, randomOffsetX), Random.Range(0, randomOffsetY));
        Vector2 rightSpawnPosition = grid[gridIndex] + randomOffset;
        GameObject rightCharacter = (GameObject)Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        Renderer rightCharacterRenderer = rightCharacter.GetComponent<Renderer>();
        rightCharacterRenderer.sortingOrder = -1;
        //Getting the character movement class from the instantiated right character and setting its speed to the predetermined character speed
        CharacterMovement rightCharacterMovement = rightCharacter.GetComponent<CharacterMovement>();
        rightCharacterMovement.characterSpeed = characterSpeeds[rightCharacterIndex];
        //Removing the right character speed from the list of character speeds
        characterSpeeds.RemoveAt(rightCharacterIndex);
        //Removing the right character from the list of characters
        characterPrefabs.RemoveAt(rightCharacterIndex);
        //Removing the Vector2 from the grid list
        grid.RemoveAt(gridIndex);
    }
    protected void CreateWrongCharacters()
    {
        //Selecting a random wrong character and random spawn position with a random offset, then instantiating it, and setting it to a random non-bottom layer
        for (int i = 0; i < numberWrongCharacters; i++)
        {
            int wrongCharacterIndex = Random.Range(0, characterPrefabs.Count);
            int gridIndex = Random.Range(0, grid.Count);
            float randomOffsetX = 1.0f;
            float randomOffsetY = 1.0f;
            Vector2 randomOffset = new Vector2(Random.Range(0, randomOffsetX), Random.Range(0, randomOffsetY));
            Vector2 wrongSpawnPosition = grid[gridIndex] + randomOffset;
            GameObject wrongCharacter = (GameObject)Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            //Getting the character movement class from the instantiated wrong character and setting its speed to the predetermined character speed
            CharacterMovement wrongCharacterMovement = wrongCharacter.GetComponent<CharacterMovement>();
            wrongCharacterMovement.characterSpeed = characterSpeeds[wrongCharacterIndex];
            Renderer wrongCharacterRenderer = wrongCharacter.GetComponent<Renderer>();
            int firstUniqueWrongCharacter = 0;
            int numberUniqueWrongCharacters = 3;
            int randomSortingOrder = Random.Range(firstUniqueWrongCharacter, numberUniqueWrongCharacters);
            wrongCharacterRenderer.sortingOrder = randomSortingOrder;
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    protected void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        //Randomizing how many wrong characters to instantiate
        numberWrongCharacters = Random.Range(170, 209 + 1);
        CreateWrongCharacters();
    }
    protected void DetermineCharacterSpeeds()
    {
        //Determine character speeds by randomly choosing between a randomly generated horizontal and vertical speed
        for (int i = 0; i < characterPrefabs.Count; i++)
        {
            Vector2 horizontalSpeedVector2 = new Vector2(Random.Range(1.0f, 2.0f) * (Random.Range(0, 2) * 2 - 1), 0);
            possibleCharacterSpeeds.Add(horizontalSpeedVector2);
            Vector2 verticalSpeedVector2 = new Vector2(0, Random.Range(3.0f, 4.0f) * (Random.Range(0, 2) * 2 - 1));
            possibleCharacterSpeeds.Add(verticalSpeedVector2);
            int possibleCharacterSpeedsIndex = Random.Range(0, 1 + 1);
            Vector2 speedVector2 = possibleCharacterSpeeds[possibleCharacterSpeedsIndex];
            characterSpeeds.Add(speedVector2);
            possibleCharacterSpeeds.RemoveAt(possibleCharacterSpeedsIndex);
            possibleCharacterSpeeds.RemoveAt(0);
        }
    }
}