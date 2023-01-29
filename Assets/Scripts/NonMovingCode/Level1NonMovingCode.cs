using System.Collections.Generic;
using UnityEngine;
public class Level1NonMovingCode : NonMovingCode
{
    void Start()
    {
        CreateCharactersOnGrid();
    }
    protected new void CreateGrid()
    {
        //Creating a 2x2 grid of Vector2 and adding it to the grid list
                Vector2 grid1Vector2 = new Vector2(0.75f, 0.75f);
                grid.Add(grid1Vector2);
                Vector2 grid2Vector2 = new Vector2(0.75f, -0.75f);
                grid.Add(grid2Vector2);
                Vector2 grid3Vector2 = new Vector2(-0.75f, 0.75f);
                grid.Add(grid3Vector2);
                Vector2 grid4Vector2 = new Vector2(-0.75f, -0.75f);
                grid.Add(grid4Vector2);
    }
    protected new void CreateRightCharacter()
    {
        //Selecting a random right character
        int rightCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
        //Setting the right character index as the Wanted character index
        WantedCharacter wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacter>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.SetWantedCharacter());
        //Selecting a random spawn position then instantiating the right character
        int gridIndex = Random.Range(firstGridIndex, grid.Count);
        Vector2 rightSpawnPosition = grid[gridIndex];
        Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        //Removing the right character from the list of characters
        characterPrefabs.RemoveAt(rightCharacterIndex);
        //Removing the Vector2 from the grid list
        grid.RemoveAt(gridIndex);
    }
    protected new void CreateWrongCharacters()
    {
        //Selecting a random wrong character and random spawn position, then instantiating it
        for (int i = 0; i < numberWrongCharacters; i++)
        {
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            int gridIndex = Random.Range(firstGridIndex, grid.Count);
            Vector2 wrongSpawnPosition = grid[gridIndex];
            Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            //Removing the wrong character from the list of characters
            characterPrefabs.RemoveAt(wrongCharacterIndex);
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    protected new void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        //Setting how many wrong characters to instantiate
        numberWrongCharacters = 3;
        CreateWrongCharacters();
    }
}
