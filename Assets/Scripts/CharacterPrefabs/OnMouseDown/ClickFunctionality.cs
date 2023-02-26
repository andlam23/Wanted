using System.Collections;
using UnityEngine;
public class ClickFunctionality : MonoBehaviour
{
    // define GameManager
    protected GameObject gameManager;
    // define game level
    public static float gameLevel;
    // define a bool for time buffer before the next level
    public static bool nextLevelBuffer;
    // define a bool for whether Right character was clicked
    public static bool isRightCharacterClicked;
    // define the right character gameobject
    public static GameObject rightCharacterGameObject;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRightCharacterClicked && TimeText.isGameActive)
        {
            Ray clickPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask rightCharacterLayer = LayerMask.GetMask("RightCharacter");
            RaycastHit2D rightHit = Physics2D.GetRayIntersection(clickPoint, Mathf.Infinity, rightCharacterLayer);
            if (rightHit.collider != null)
            {
                SetCharacterLayer rightCharacterSetCharacterLayer = rightHit.collider.transform.gameObject.GetComponent<SetCharacterLayer>();
                // start the display +5 coroutine
                StartCoroutine(rightCharacterSetCharacterLayer.DisplayTimeGainAndLoss("+5"));
                //
                gameLevel += 1;
                // if Right character is clicked with circle collider turned on, destroy all characters, add 1 to gameLevel, and attempt to load
                // next level
                StartCoroutine(DestroyCharactersAndPauseCountdownAndLoadLevel());
                //Add 5 seconds to time for completing previous level
                TimeText.time += 5;
                //Prevent time from exceeding 30 seconds
                if (TimeText.time > 30)
                {
                    TimeText.time = 30;
                }
            }
            LayerMask wrongCharacterLayer = LayerMask.GetMask("WrongCharacter");
            RaycastHit2D wrongHit = Physics2D.GetRayIntersection(clickPoint, Mathf.Infinity, wrongCharacterLayer);
            if (wrongHit.collider != null && !isRightCharacterClicked)
            {
                SetCharacterLayer wrongCharacterSetCharacterLayer = wrongHit.collider.transform.gameObject.GetComponent<SetCharacterLayer>();
                // start the display -10 coroutine
                StartCoroutine(wrongCharacterSetCharacterLayer.DisplayTimeGainAndLoss("-10"));
                //Subtract 5 seconds from time for clicking wrong character
                TimeText.time -= 10;
            }
        }
    }
    // Define coroutine to destroy characters, stop character movement, pause time countdown, and load the next level
    public IEnumerator DestroyCharactersAndPauseCountdownAndLoadLevel()
    {
        // set "is Right Character clicked" to true
        isRightCharacterClicked = true;
        // set buffer for next level to true
        nextLevelBuffer = true;
        // stores all characters in a var then destroys each of them if it is not the right character
        var characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (var character in characters)
        {
            if (character != rightCharacterGameObject)
            Destroy(character);
        }
        // deactivate the right character's movement scripts
        rightCharacterGameObject.GetComponent<CharacterMovement>().enabled = false;
        rightCharacterGameObject.GetComponent<CharacterMovementBouncy>().enabled = false;
        // wait for 2 seconds
        yield return new WaitForSeconds(2);
        // set buffer for next level to false
        nextLevelBuffer = false;
        // destroy the right character
        Destroy(rightCharacterGameObject);
        // load the next level
        LoadLevel();
        // set "is Right Character clicked" to false
        isRightCharacterClicked = false;
    }
    protected void LoadLevel()
    {

        // if gameLevel is 1, load level 2 by finding the GameManager, accessing the level 2 script, and enabling it
        if (gameLevel == 1)
        {
            gameManager = GameObject.Find("GameManager");
            Level2NonMovingCode level = gameManager.GetComponent<Level2NonMovingCode>();
            level.enabled = true;
        }
        // if gameLevel is 2, load level 3 by finding the GameManager, accessing the level 3 script, and enabling it
        else if (gameLevel == 2)
        {
            gameManager = GameObject.Find("GameManager");
            Level3NonMovingCode level = gameManager.GetComponent<Level3NonMovingCode>();
            level.enabled = true;
        }
        // if gameLevel is 3, load level 4 by finding the GameManager, accessing the NonMovingCode script, and enabling it
        else if (gameLevel == 3)
        {
            gameManager = GameObject.Find("GameManager");
            NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
            level.enabled = true;
        }
        // if the gameLevel is 4 through 9, load levels 5 through 10
        // Right character should be on bottom layer when gameLevel is 6, loading into level 7
        else if (gameLevel >= 4 && gameLevel < 10)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 10)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCode level = gameManager.GetComponent<MovingCode>();
            level.enabled = true;
        }
        else if (gameLevel == 11)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCodeWavy level = gameManager.GetComponent<MovingCodeWavy>();
            level.enabled = true;
        }
        else if (gameLevel == 12)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 13)
        {
            gameManager = GameObject.Find("GameManager");
            MovingCodeBouncy level = gameManager.GetComponent<MovingCodeBouncy>();
            level.enabled = true;
        }
        else if (gameLevel == 14)
        {
            LoadNonMovingCodeLevel();
        }
        else if (gameLevel == 15)
        {
            LoadMovingCodeLevel();
        }
        // THESE TWO LEVELS ARE USED TO TEST WAVY AND BOUNCY LEVEL FUNCTIONS
        else if (gameLevel == 16)
        {
            LoadMovingCodeWavyLevel();
        }
        else if (gameLevel == 17)
        {
            LoadMovingCodeBouncyLevel();
        }
        //gameLevel % 1 == 0 is true only if gameLevel is an integer, which solves for OnMouseDown() calling this function twice
        else if (gameLevel > 17 && gameLevel % 1 == 0)
        {
            int numberMovementPatterns = 4;
            int overload = 1;
            int level = Random.Range(1, numberMovementPatterns + overload);
            if (level == 1)
            {
                LoadNonMovingCodeLevel();
            }
            else if (level == 2)
            {
                LoadMovingCodeLevel();
            }
            else if (level == 3)
            {
                LoadMovingCodeWavyLevel();
            }
            else if (level == 4)
            {
                LoadMovingCodeBouncyLevel();
            }
        }
    }
    // finds the GameManager, accesses the NonMovingCode script, calls two functions to disable the CharacterMovementBouncy script on
    // character prefabs, then creates the characters
    protected void LoadNonMovingCodeLevel()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        gameManager = GameObject.Find("GameManager");
        NonMovingCode level = gameManager.GetComponent<NonMovingCode>();
        level.DisableCharacterMovementBouncyScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 0.3f;
        level.randomOffsetY = 0.3f;
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeLevel()
    {
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        gameManager = GameObject.Find("GameManager");
        MovingCode level = gameManager.GetComponent<MovingCode>();
        level.DisableCharacterMovementBouncyScript();
        level.EnableCharacterMovementScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.DetermineCharacterSpeeds();
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeWavyLevel()
    {
        gameManager = GameObject.Find("GameManager");
        MovingCodeWavy level = gameManager.GetComponent<MovingCodeWavy>();
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        level.DisableCharacterMovementBouncyScript();
        level.EnableCharacterMovementScript();
        level.spawnRangeX = 9;
        level.spawnRangeY = 5;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.DetermineCharacterSpeeds();
        level.CreateCharactersOnGrid();
    }
    protected void LoadMovingCodeBouncyLevel()
    {
        gameManager = GameObject.Find("GameManager");
        MovingCodeBouncy level = gameManager.GetComponent<MovingCodeBouncy>();
        //Disable function is before Enable and Determine functions because CharacterPrefabs are reloaded in the Disable function
        level.DisableCharacterMovementScript();
        level.EnableCharacterMovementBouncyScript();
        level.spawnRangeX = 8;
        level.spawnRangeY = 4;
        level.randomOffsetX = 1.0f;
        level.randomOffsetY = 1.0f;
        level.CreateCharactersOnGrid();
    }
}
