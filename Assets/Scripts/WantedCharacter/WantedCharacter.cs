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
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    //Define a function to set the UI image to the character assigned the Wanted character index
    public IEnumerator SetWantedCharacter()
    {
        image.enabled = !image.enabled;
        image.sprite = characters[wantedCharacterIndex];
        yield return new WaitForSeconds(5);
        image.enabled = !image.enabled;
    }
}