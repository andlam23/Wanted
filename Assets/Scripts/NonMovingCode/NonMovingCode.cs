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
    protected int spawnRangeX = 8;
    protected int spawnRangeY = 4;
    //Number of wrong characters to instantiate
    protected int numberWrongCharacters;
    //Define no offset for a character's position
    protected float noOffset = 0;
    //Define random X and Y offset for a character's position
    protected float randomOffsetX = 0.3f;
    protected float randomOffsetY = 0.3f;
    //Define a Wanted character gameobject
    public GameObject wantedCharacterImage;
    void Start()
    {
        DisableCharacterMovementBouncyScript();
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
        //Making Right character appear in front of wrong characters until level 7
        int behindOtherCharacters;
        if (CharacterMovement.gameLevel < 6)
        {
            behindOtherCharacters = 3;
        }
        else
        {
            behindOtherCharacters = -1;
        }
        rightCharacterRenderer.sortingOrder = behindOtherCharacters;
        //Enable the Right character's collider
        EnableRightCharacterCollider(rightCharacter);
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
            int wrongCharacterIndex = Random.Range(firstCharacterPrefab, characterPrefabs.Count);
            int gridIndex = Random.Range(firstGridIndex, grid.Count);
            Vector2 randomOffset = new Vector2(Random.Range(noOffset, randomOffsetX), Random.Range(noOffset, randomOffsetY));
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
    public void CreateCharactersOnGrid()
    {
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
    protected void EnableRightCharacterCollider(GameObject rightCharacterGameObject)
    {
        Collider2D rightCharacterCollider = rightCharacterGameObject.GetComponent<CircleCollider2D>();
        rightCharacterCollider.enabled = true;
    }
    protected void ReloadCharacterPrefabs()
    {
        //Removing the right character from the list of characters
        characterPrefabs.Clear();
        //Adding prefabs from CharacterPack1 folder from Resources folder to list of characters
        characterPrefabs = Resources.LoadAll<GameObject>("CharacterPack1").ToList();
    }
}
