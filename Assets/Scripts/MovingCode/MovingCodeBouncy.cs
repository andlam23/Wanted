using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingCodeBouncy : MonoBehaviour
{
    //Define a list of characters
    public List<GameObject> characterPrefabs;
    //Define a list of Vector2's to create a grid
    public List<Vector2> grid;
    //Character spawn area
    protected int spawnRangeX = 8;
    protected int spawnRangeY = 4;
    //Number of wrong characters to instantiate
    protected int numberWrongCharacters;

    //REUSEABLE: Define a Wanted character gameobject
    public GameObject wantedCharacterImage;

    void Start()
    {
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
        numberWrongCharacters = Random.Range(151, 152 + 1);
        CreateWrongCharacters();
    }
}