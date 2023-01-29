using System.Collections.Generic;
using UnityEngine;
public class Level3NonMovingCode : Level2NonMovingCode
{
    void Start()
    {
        spawnRangeX = 5.5f;
        spawnRangeY = 3.0f;
        CreateCharactersOnGrid();
    }
    protected new void CreateCharactersOnGrid()
    {
        //Creating a Length x Width grid of Vector2's and adding it to the grid list
        CreateGrid();
        CreateRightCharacter();
        //Setting how many wrong characters to instantiate
        numberWrongCharacters = 83;
        CreateWrongCharacters();
    }
}
