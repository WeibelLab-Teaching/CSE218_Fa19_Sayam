using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class play_song : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        readNotesFile(getCurrentSongFile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void readNotesFile(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        string contents = reader.ReadToEnd();
        Debug.Log("sontents of file: " + contents);
        reader.Close();
    }

    string getCurrentSongFile()
    {
        return "Assets/song_notes/sample_song.txt";
    }
}
