public class MovingCodeBouncy : NonMovingCode
{
    void Start()
    {
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
}