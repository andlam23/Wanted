using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WantedCharacter : MonoBehaviour
{
    //Define a Wanted character index for the game manager script to assign a value to
    public int wantedCharacterIndex;
    //Define a list of characters
    public List<Sprite> characters;
    //Define the image component as Image
    public Image image;
    //Define Wanted character background and poster gameobjects
    public GameObject wantedCharacterBackground;
    public GameObject wantedCharacterPoster;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    //Define a coroutine to set the UI image to the character assigned the Wanted character index and also setting the 
    //Wanted character background and poster to active, show them for 3 seconds, then disable them
    public IEnumerator SetWantedCharacter()
    {
        image.enabled = !image.enabled;
        image.sprite = characters[wantedCharacterIndex];

        wantedCharacterBackground.SetActive(true);
        wantedCharacterPoster.SetActive(true);

        yield return new WaitForSeconds(3);

        image.enabled = !image.enabled;

        wantedCharacterBackground.SetActive(false);
        wantedCharacterPoster.SetActive(false);
    }
}