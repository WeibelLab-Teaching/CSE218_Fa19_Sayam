using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class GameStateChange : UnityEvent<String> { }
public class GameState : Singleton<GameState>
{

    public GameStateChange onGameStateChange;


    public String[] songs = new String[2] { "Twinkle Twinkle", "Happy Birthday" };

    /* MR begin */
    public Boolean keyboardPlaced = false;

    /* MR end */
    public Boolean Playing = false;
    public Boolean Forward = false;
    public Boolean Backward = false;


    private void Start()
    {
        _currentSong = "";
        if (onGameStateChange != null)
            onGameStateChange.Invoke(_currentSong);
    }

    [SerializeField]
    private String _currentSong;

    public String currentSong
    {
        set
        {
            if (value != _currentSong)
            {
                _currentSong = value;
                Debug.Log("GameState current song updated to ");
                Debug.Log(_currentSong);
                if (onGameStateChange != null)
                    onGameStateChange.Invoke(_currentSong);
            }
        }

        get
        {
            return _currentSong;
        }


    }

}