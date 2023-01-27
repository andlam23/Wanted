using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Level2NonMovingCode : MonoBehaviour
{
    //Define a list of characters
    public List<GameObject> characterPrefabs;
    //Define a list of Vector2's to create a grid
    public List<Vector2> grid;
    //Character spawn area
    protected float spawnRangeX = 1.5f;
    protected float spawnRangeY = 1.5f;
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
        //Creating a 4x4 grid of Vector2's and adding it to the grid list
        for (float i = -spawnRangeX; i < spawnRangeX + 1; i++)
        {
            for (float j = -spawnRangeY; j < spawnRangeY + 1; j++)
            {
                Vector2 gridVector2 = new Vector2(1.5f*i, 1.5f*j);
                grid.Add(gridVector2);
            }
        }
    }
    protected void CreateRightCharacter()
    {
        //Selecting a random right character and random spawn position, then instantiating it
        int rightCharacterIndex = Random.Range(0, characterPrefabs.Count);

        //REUSABLE: Setting the right character index as the Wanted character index
        WantedCharacter wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacter>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.SetWantedCharacter());

        int gridIndex = Random.Range(0, grid.Count);
        Vector2 rightSpawnPosition = grid[gridIndex];
        Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
       //Removing the right character from the list of characters
        characterPrefabs.RemoveAt(rightCharacterIndex);
        //Removing the Vector2 from the grid list
        grid.RemoveAt(gridIndex);
    }
    protected void CreateWrongCharacters()
    {
        //Selecting a random wrong character and random spawn position, then instantiating it
        for (int i = 0; i < numberWrongCharacters; i++)
        {
            int wrongCharacterIndex = Random.Range(0, characterPrefabs.Count);
            int gridIndex = Random.Range(0, grid.Count);
            Vector2 wrongSpawnPosition = grid[gridIndex];
            Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    protected void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        //Setting how many wrong characters to instantiate
        numberWrongCharacters = 15;
        CreateWrongCharacters();
    }
}
