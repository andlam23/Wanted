using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetCharacterLayer : MonoBehaviour
{
    // define a bool for Right character or not
    public bool isRightCharacter;
    private void Start()
    {
        // set character to appropriate layer
        if (isRightCharacter)
        {
            gameObject.layer = LayerMask.NameToLayer("RightCharacter");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("WrongCharacter");
        }
    }
    public IEnumerator DisplayTimeGainAndLoss(string gainOrLossGameObject)
    {
        //Find the +5 or -10 gameobject, access the RectTransform component
        GameObject gainOrLoss = GameObject.Find(gainOrLossGameObject);
        RectTransform rectTransform = gainOrLoss.GetComponent<RectTransform>();
        //Find the Canvas gameobject, access the RectTransform component
        GameObject canvas = GameObject.Find("Canvas");
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        //Convert transform.position of character to an anchored position
        Vector2 anchoredTransformPosition = Camera.main.WorldToScreenPoint(transform.position);
        //Define canvas offset
        Vector2 offset = canvasRectTransform.sizeDelta / 2;
        //Set anchored position of +5 or -10 gameobject to anchored position of character minus canvas offset
        rectTransform.anchoredPosition = anchoredTransformPosition - offset;
        //Access the +5 or -10 gameobject image component
        Image image = gainOrLoss.GetComponent<Image>();
        //Enable the image
        image.enabled = true;
        //Wait for 1 seconds
        yield return new WaitForSeconds(2);
        //Disable image component
        image.enabled = false;
    }
}
