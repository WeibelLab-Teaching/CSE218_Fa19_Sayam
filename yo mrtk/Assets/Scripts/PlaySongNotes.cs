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
    public Transform PlayButton;
    int prev_t = -1;
    int return_time = -1;

    // Start is called before the first frame update
    void Start()
    {
        setupPainoKeys();
        sp = new SongProviderV1(getCurrentSongFile());
        //Time.timeScale = ;
        //Time.fixedDeltaTime = 2e-10f * Time.timeScale;
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
        GameState.Instance.Playing = true;

    }




    // Update is called once per frame
    void Update()
	{
        if (!GameState.Instance.Playing)
        {   
            startTS = Time.time;
            return_time = -1;
            return;
        }
        
        if(return_time == (int)(Time.time - startTS))
        {
            return;
        }
        if (GameState.Instance.Forward)
        {
            prev_t = prev_t + 1;
            GameState.Instance.Forward = false;
        }
        if (GameState.Instance.Backward)
        {
            prev_t = prev_t - 3;
            GameState.Instance.Backward = false;
        }
        return_time = (int)(Time.time - startTS);
        int t = prev_t + 1;
        prev_t = t;
        Debug.Log(t);

		if (t >= sp.songSequence.Count)

        {
            if(prevKeyIndex != -1)
            {
                pianoKeys[prevKeyIndex].gameObject.GetComponent<MeshRenderer>().material.color = prevKeyColor;
                prevKeyIndex = -1;
            }
            
            Debug.Log("ended");
			return;
		}

        KeyPress kp = sp.songSequence[t];
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

                Color newColor;
                if (kp.key.StartsWith("B_"))
                {
                    newColor = new Color(1, 0, 0, 1);
                }
                else
                {
                    newColor = new Color(0, 1, 0, 1);
                }
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
        for (int j = 0; j < 200; j++)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                string[] keyDetails = contents[i].Split(',');
                KeyPress kp = new KeyPress(keyDetails[0], keyDetails[1], i);
                songSequence.Add(kp);
            }
        }
		//reader.Close();
	}

	public KeyPress getPressedKeyAtTime(double time,int prev_t)
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