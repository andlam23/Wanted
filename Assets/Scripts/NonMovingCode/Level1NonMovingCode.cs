using UnityEngine;
public class Level1NonMovingCode : NonMovingCode
{
    void Start()
    {
        //Currently, if the game ends on a Bouncy character level, it will make the game start with characters bouncing
        //so we need to disable it from level 1
        DisableCharacterMovementBouncyScript();
        CreateCharactersOnGrid();
    }
    protected new void CreateGrid()
    {
        //Creating a grid of Vector2's from -0.75,-0.75 to 0.75,0.75 and adding it to the grid list
        for (float i = -0.5f; i <= 0.5f; i++)
        {
            float x = i * 3.0f / 2.0f;
            for (float j = -0.5f; j <= 0.5f; j++)
            {
                float y = j * 3.0f / 2.0f;
                Vector2 gridVector2 = new Vector2(x, y);
                grid.Add(gridVector2);
            }
        }
    }
    protected new void CreateRightCharacter()
    {
        //Selecting a random character index to be the Right character index
        int rightCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
        //Setting the Right character index as the Wanted character index
        WantedCharacterAndStars wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacterAndStars>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.DisplayWantedCharacterAndGameLevelAndStar());
        //Selecting a random spawn position then instantiating the Right character
        int gridIndex = Random.Range(firstGridIndex, grid.Count);
        Vector2 rightSpawnPosition = grid[gridIndex];
        GameObject rightCharacter = (GameObject)Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        //Enabling the Right character's collider
        EnableRightCharacterCollider(rightCharacter);
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
            //Selecting a random character index to be the Wrong character index
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            int gridIndex = Random.Range(firstGridIndex, grid.Count);
            //Selecting a random spawn position then instantiating the Wrong character
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
