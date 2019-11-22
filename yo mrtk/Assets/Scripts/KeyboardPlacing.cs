using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlacing : MonoBehaviour
{
    public Transform keyboard;
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
            gameObject.SetActive(true);
        }
    }

    public void OnAirTap()
    {
        if(currentlyPlacingKeyboard)
        {
            Debug.Log("kEYBOARD PLACEMENT dONE");
            gameObject.SetActive(false);
            GameState.Instance.keyboardPlaced = true;

            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.startPlaying();
        }
    }
}
