using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class NonMovingCode : MonoBehaviour
{
    //Define a list of characters
    public List<GameObject> characterPrefabs;
    //Define first character prefab
    protected int firstCharacterPrefab;
    //Define a list of Vector2's to create a grid
    public List<Vector2> grid;
    //Define first grid index
    protected int firstGridIndex = 0;
    //Character spawn area
    public int spawnRangeX;
    public int spawnRangeY;
    //Number of wrong characters to instantiate
    protected int numberWrongCharacters;
    //Define no offset for a character's position
    protected float noOffset = 0;
    //Define random X and Y offset for a character's position
    public float randomOffsetX;
    public float randomOffsetY;
    //Define a Wanted character gameobject
    public GameObject wantedCharacterImage;
    void Start()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        DisableCharacterMovementBouncyScript();
        spawnRangeX = 8;
        spawnRangeY = 4;
        randomOffsetX = 0.3f;
        randomOffsetY = 0.3f;
        CreateCharactersOnGrid();
    }
    protected void CreateGrid()
    {
        int includePositiveSpawnRange = 1;
        //Creating a grid of Vector2's from -8,-4 to 8,4 and adding it to the grid list
        for (int i = -spawnRangeX; i < spawnRangeX + includePositiveSpawnRange; i++)
        {
            for (int j = -spawnRangeY; j < spawnRangeY + includePositiveSpawnRange; j++)
            {
                Vector2 gridVector2 = new Vector2(i, j);
                grid.Add(gridVector2);
            }
        }
    }
    protected void CreateRightCharacter()
    {
        //Selecting a random character index to be the right character index
        int rightCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
        //Setting the right character index as the Wanted character index
        WantedCharacterAndStars wantedCharacter = wantedCharacterImage.GetComponent<WantedCharacterAndStars>();
        wantedCharacter.wantedCharacterIndex = rightCharacterIndex;
        StartCoroutine(wantedCharacter.DisplayWantedCharacterAndGameLevelAndStar());
        //Determining spawn position for the Right character
        int gridIndex = Random.Range(firstGridIndex, grid.Count);
        Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
        Vector2 rightSpawnPosition = grid[gridIndex] + randomOffset;
        //Instantiating the right character
        GameObject rightCharacter = (GameObject)Instantiate(characterPrefabs[rightCharacterIndex], rightSpawnPosition, Quaternion.identity);
        //Setting right character gameobject to right character
        ClickFunctionality.rightCharacterGameObject = rightCharacter;
        //Accessing the Right character's SetCharacterLayer script and setting isRightCharacter bool to true
        SetCharacterLayer rightCharacterSetCharacterLayer = rightCharacter.GetComponent<SetCharacterLayer>();
        rightCharacterSetCharacterLayer.isRightCharacter = true;
        //Making Right character appear in front of wrong characters until level 7, when it will start to appear behind wrong characters
        Renderer rightCharacterRenderer = rightCharacter.GetComponent<Renderer>();
        int behindOtherCharacters;
        if (ClickFunctionality.gameLevel < 6)
        {
            int inFrontOtherCharacters = 3;
            behindOtherCharacters = inFrontOtherCharacters;
        }
        else
        {
            behindOtherCharacters = -1;
        }
        rightCharacterRenderer.sortingOrder = behindOtherCharacters;
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
            //Selecting a random character index to be the wrong character index
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            //Determining spawn position for the wrong character
            int gridIndex = Random.Range(firstGridIndex, grid.Count);
            Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
            Vector2 wrongSpawnPosition = grid[gridIndex] + randomOffset;
            //Instantiating the wrong character
            GameObject wrongCharacter = (GameObject)Instantiate(characterPrefabs[wrongCharacterIndex], wrongSpawnPosition, Quaternion.identity);
            //Randomly setting the sorting order of the wrong character
            Renderer wrongCharacterRenderer = wrongCharacter.GetComponent<Renderer>();
            int firstUniqueWrongCharacter = 0;
            int numberUniqueWrongCharacters = 3;
            int randomSortingOrder = Random.Range(firstUniqueWrongCharacter, numberUniqueWrongCharacters);
            wrongCharacterRenderer.sortingOrder = randomSortingOrder;
            //Removing the Vector2 from the grid list
            grid.RemoveAt(gridIndex);
        }
    }
    public void CreateCharactersOnGrid()
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
        int minNumberWrongCharacters = 140;
        int overload = 1;
        //Randomizing how many wrong characters to instantiate
        numberWrongCharacters = Random.Range(minNumberWrongCharacters, maxNumberWrongCharacters + overload);
        CreateWrongCharacters();
    }
    public void DisableCharacterMovementBouncyScript()
    {
        //Reload character prefabs first because the for loop requires it
        ReloadCharacterPrefabs();
        int numberOfCharacterPrefabs = 4;
        //Disabling the CharacterMovementBouncy script for each prefab
        for (int i = 0; i < numberOfCharacterPrefabs; i++)
        {
            CharacterMovementBouncy characterMovementBouncyScript = characterPrefabs[i].GetComponent<CharacterMovementBouncy>();
            characterMovementBouncyScript.enabled = false;
        }
    }
    protected void ReloadCharacterPrefabs()
    {
        //Removing all characters from the list of characters
        characterPrefabs.Clear();
        //Adding prefabs from CharacterPack1 folder from Resources folder to list of characters
        characterPrefabs = Resources.LoadAll<GameObject>("CharacterPack1").ToList();
    }
}
