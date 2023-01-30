using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // define game manager gameobject
    public GameObject gameManager;
    public void loadNextLevel()
    {
        MonoBehaviour[] gameManagerScripts = gameManager.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in gameManagerScripts)
        {
            script.enabled = false;
        }
        int firstMovingCode = 0;
        int numberOfMovingCodes = gameManagerScripts.Length;
        int nextLevelMovingCode = Random.Range(firstMovingCode, numberOfMovingCodes);
        MonoBehaviour nextLevel = gameManagerScripts[nextLevelMovingCode];
        SceneManager.LoadScene("GameScene");
        nextLevel.enabled = true;
    }
}
