using UnityEngine;
public class Level2NonMovingCode : Level1NonMovingCode
{
    //Character spawn area
    protected new float spawnRangeX = 1.5f;
    protected new float spawnRangeY = 1.5f;
    void Start()
    {
        CreateCharactersOnGrid();
    }
    protected new void CreateGrid()
    {
        int includePositiveSpawnRange = 1;
        float characterDiameter = 1.5f;
        //Creating a 4x4 grid of Vector2's and adding it to the grid list
        for (float i = -spawnRangeX; i < spawnRangeX + includePositiveSpawnRange; i++)
        {
            for (float j = -spawnRangeY; j < spawnRangeY + includePositiveSpawnRange; j++)
            {
                Vector2 gridVector2 = new Vector2(characterDiameter * i, characterDiameter * j);
                grid.Add(gridVector2);
            }
        }
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
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    protected new void CreateCharactersOnGrid()
    {
        CreateGrid();
        CreateRightCharacter();
        //Setting how many wrong characters to instantiate
        numberWrongCharacters = 15;
        CreateWrongCharacters();
    }
}
