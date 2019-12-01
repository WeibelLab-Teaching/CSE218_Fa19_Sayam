using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlacing : MonoBehaviour
{
    public Transform keyboard;
    public Transform instructionText;
    public Transform PlayButton;
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

    public void OnAirTap()
    {
        if(currentlyPlacingKeyboard)
        {
            Debug.Log("kEYBOARD PLACEMENT dONE");
            gameObject.SetActive(false);
            instructionText.gameObject.SetActive(false);
            gameRoot.gameObject.GetComponent<ManipulationHandler>().enabled = false;

            PlayButton.gameObject.SetActive(true);
            ForwardButton.gameObject.SetActive(true);
            BackwardButton.gameObject.SetActive(true);
            GameState.Instance.keyboardPlaced = true;
            fullMenu.gameObject.SetActive(true);

            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.startPlaying();
        }
    }
}
