using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlaySongNotes : MonoBehaviour
{
    int frameNum;
    List<Transform> pianoKeys = new List<Transform>();
    SongProviderV1 sp;
    KeyPress prevKp;
    int prevKeyIndex = -1;
    Color prevKeyColor;
    double startTS;
    bool playing;
    public Transform keyboardPlacingButton;

    // Start is called before the first frame update
    void Start()
    {
        setupPainoKeys();
        sp = new SongProviderV1(getCurrentSongFile());
        Debug.Log(pianoKeys[0].name);
    }

    public void songSelected()
    {
        if(GameState.Instance.keyboardPlaced == false)
        {
            makeUserPlaceKeyboard();
        } 
        else
        {
            startPlaying();
        }
    }

    void makeUserPlaceKeyboard()
    {
        KeyboardPlacing kp = (KeyboardPlacing) keyboardPlacingButton.gameObject.GetComponent(typeof(KeyboardPlacing));
        kp.userPlacingKeyboard();
    }

    public void startPlaying()
    {
        startTS = Time.time;
        playing = true;
    }

	// Update is called once per frame
	void Update()
	{
        if (!playing) return;

		double t = Time.time - startTS;

		KeyPress kp = sp.getPressedKeyAtTime(t);
		if (kp == null)
		{
            if(prevKeyIndex != -1)
            {
                pianoKeys[prevKeyIndex].gameObject.GetComponent<MeshRenderer>().material.color = prevKeyColor;
                prevKeyIndex = -1;
            }
            
            Debug.Log("ended");
			return;
		}

		Debug.Log(kp.key + " " + kp.finger);

		if (kp == prevKp) return;

        if(prevKeyIndex != -1)
		{
			pianoKeys[prevKeyIndex].gameObject.GetComponent<MeshRenderer>().material.color = prevKeyColor;
		}

        for(int i=0;i< pianoKeys.Capacity;i++)
		{
            if(pianoKeys[i].name.StartsWith(kp.key))
			{
				prevKeyColor = pianoKeys[i].gameObject.GetComponent<MeshRenderer>().material.color;

				Color newColor = new Color(1, 0, 0, 1);
				pianoKeys[i].gameObject.GetComponent<MeshRenderer>().material.color = newColor;
				prevKeyIndex = i;

				break;
			}
		}

		prevKp = kp;
	}

    
    void setupPainoKeys()
	{
		foreach (Transform child in transform)
		{
			pianoKeys.Add(child);
		}
	}
	

	string getCurrentSongFile()
	{
		return "Assets/song_notes/sample_song.txt";
	}
}

// Song provider with no concept of key timestamp
// Keys are played in sequence that they are
// present in input file
public class SongProviderV1
{
	public List<KeyPress> songSequence;

	public SongProviderV1(string filePath)
	{
		songSequence = new List<KeyPress>();
		readNotesFile(filePath);
	}

	void readNotesFile(string filePath)
	{
		//StreamReader reader = new StreamReader(filePath);
		string[] contents = File.ReadAllLines(filePath);
		Debug.Log("contents of file: " + contents);
        for(int i=0;i<contents.Length;i++)
		{
			string[] keyDetails = contents[i].Split(',');
			KeyPress kp = new KeyPress(keyDetails[0], keyDetails[1], i);
			songSequence.Add(kp);
		}
		//reader.Close();
	}

	public KeyPress getPressedKeyAtTime(double time)
	{
		Debug.Log(((int)time)  + " " + songSequence.Count);
		if (((int)time) >= songSequence.Count)
		{
			return null;
		}
		return songSequence[(int)time];
	}
}

public class KeyPress
{
	public string key;
	public string finger;
	public double startTime;
	public double endTime;

    public KeyPress(string key, string finger, int seqNum)
	{
		this.key = key;
		this.finger = finger;
		this.startTime = seqNum;
		this.endTime = seqNum + 1;
	}
}