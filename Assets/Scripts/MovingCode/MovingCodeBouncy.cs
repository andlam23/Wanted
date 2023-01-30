public class MovingCodeBouncy : NonMovingCode
{
    void Start()
    {
        DisableCharacterMovementScript();
        EnableCharacterMovementBouncyScript();
        randomOffsetX = 1.0f;
        randomOffsetY = 1.0f;
        CreateCharactersOnGrid();
    }
    protected new void CreateCharactersOnGrid()
    {
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
    protected void DisableCharacterMovementScript()
    {
        int numberOfCharacterPrefabs = 4;
        //Disabling the CharacterMovement script for each prefab
        for (int i = 0; i < numberOfCharacterPrefabs; i++)
        {
            CharacterMovement characterMovementScript = characterPrefabs[i].GetComponent<CharacterMovement>();
            characterMovementScript.enabled = false;
        }
    }
    protected void EnableCharacterMovementBouncyScript()
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