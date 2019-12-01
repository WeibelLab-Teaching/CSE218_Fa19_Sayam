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
    public String[] tempos = new String[4] { "0.5x", "0.75x", "1x", "1.25x" };

    [SerializeField]
    private String _currentSong;

    [SerializeField]
    private String _currentTempo;

    /* MR begin */
    public Boolean keyboardPlaced = false;

    /* MR end */
    public Boolean Playing = false;
    public Boolean Forward = false;
    public Boolean Backward = false;


    private void Start()
    {
        _currentSong = "";
        _currentTempo = "1x";
        if (onGameStateChange != null) {
            onGameStateChange.Invoke(_currentSong);
            onGameStateChange.Invoke(_currentTempo);
        }
    }

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

    public String currentTempo
    {
        set
        {
            if (value != _currentTempo)
            {
                _currentTempo = value;
                Debug.Log("GameState current song updated to ");
                Debug.Log(_currentTempo);
                if (onGameStateChange != null)
                    onGameStateChange.Invoke(_currentTempo);
            }
        }

        get
        {
            return _currentTempo;
        }


    }

    public String truncate(String input)
    {
        if (input.Length >= 12)
        {
            return input.Substring(0,12) + "...";
        }
        return input;

    }

}