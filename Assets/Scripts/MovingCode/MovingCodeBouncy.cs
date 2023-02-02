public class MovingCodeBouncy : NonMovingCode
{
    void Start()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        DisableCharacterMovementScript();
        EnableCharacterMovementBouncyScript();
        spawnRangeX = 8;
        spawnRangeY = 4;
        randomOffsetX = 1.0f;
        randomOffsetY = 1.0f;
        CreateCharactersOnGrid();
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
        //Setting how many wrong characters to instantiate
        numberWrongCharacters = maxNumberWrongCharacters;
        CreateWrongCharacters();
    }
    public void DisableCharacterMovementScript()
    {
        //Reload character prefabs first because the for loop requires it
        ReloadCharacterPrefabs();
        int numberOfCharacterPrefabs = 4;
        //Disabling the CharacterMovement script for each prefab
        for (int i = 0; i < numberOfCharacterPrefabs; i++)
        {
            CharacterMovement characterMovementScript = characterPrefabs[i].GetComponent<CharacterMovement>();
            characterMovementScript.enabled = false;
        }
    }
    public void EnableCharacterMovementBouncyScript()
    {
        int numberOfCharacterPrefabs = 4;
        //Enabling the CharacterMovementBouncy script for each prefab
        for (int i = 0; i < numberOfCharacterPrefabs; i++)
        {
            CharacterMovementBouncy characterMovementBouncyScript = characterPrefabs[i].GetComponent<CharacterMovementBouncy>();
            characterMovementBouncyScript.enabled = true;
        }
    }
}