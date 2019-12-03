using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlacing : MonoBehaviour
{
    public Transform keyboard;
    public Transform instructionText;
    public Transform PlayButton;
    public Transform PauseButton;
    public Transform ForwardButton;
    public Transform BackwardButton;
    public Transform fullMenu;
    public Transform gameRoot;
    bool currentlyPlacingKeyboard = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void userPlacingKeyboard()
    {
        if(!currentlyPlacingKeyboard)
        {
            currentlyPlacingKeyboard = true;
            keyboard.gameObject.SetActive(true);
            fullMenu.gameObject.SetActive(false);
            gameObject.SetActive(true);

            gameRoot.gameObject.GetComponent<ManipulationHandler>().enabled = true;

            instructionText.gameObject.SetActive(true);
        }
    }


    public void makePianoTransparent()
    {

        foreach (Transform key in keyboard)
        {
            Color prevKeyColor = key.gameObject.GetComponent<MeshRenderer>().material.color;
            Debug.Log("doing a key " + prevKeyColor.r + " " + prevKeyColor.g + " " + prevKeyColor.b);
            Color newColor = new Color(prevKeyColor.r, prevKeyColor.g, prevKeyColor.b, 0.15f);
            key.gameObject.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public void OnAirTap()
    {
        if(currentlyPlacingKeyboard)
        {
            Debug.Log("kEYBOARD PLACEMENT dONE");
            gameObject.SetActive(false);
            instructionText.gameObject.SetActive(false);
            gameRoot.gameObject.GetComponent<ManipulationHandler>().enabled = false;

            makePianoTransparent();

            //PlayButton.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(true);
            ForwardButton.gameObject.SetActive(true);
            BackwardButton.gameObject.SetActive(true);
            GameState.Instance.keyboardPlaced = true;
            fullMenu.gameObject.SetActive(true);

            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.startGetReadyCountDown();
        }
    }
}
