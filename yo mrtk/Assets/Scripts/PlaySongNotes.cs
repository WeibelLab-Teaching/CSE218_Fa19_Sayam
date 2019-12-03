using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlaySongNotes : MonoBehaviour
{
    //int frameNum;
    List<Transform> pianoKeys = new List<Transform>();
    SongPlayer sp;

    // Prev state
    KeyPress prevKp;
    int prevKeyIndex = -1;
    Color prevKeyColor;

    double curTS;
    public Transform keyboardPlacingButton;
    public Transform playButton;
    public Transform pauseButton;

    // 3..2..1 button parent
    public Transform getReadyButton;
    double getReadyStartTS;
    bool showingGetReady = false;

    // Start is called before the first frame update
    void Start()
    {
        // Add piano keys gameobjects to a list
        setupPainoKeys();

        // Init song Provider
        sp = new SongPlayer(getCurrentSongFile());
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
            sp = new SongPlayer(getCurrentSongFile());
            startGetReadyCountDown();
        }

    }

    public void startGetReadyCountDown()
    {
        showingGetReady = true;
        getReadyStartTS = Time.time;
        getReadyButton.gameObject.SetActive(true);
    }
    
    void getReadyDone()
    {
        showingGetReady = false;
        getReadyButton.gameObject.SetActive(false);
        startPlayingFromBeginning();
    }

    void makeUserPlaceKeyboard()
    {
        KeyboardPlacing kp = (KeyboardPlacing) keyboardPlacingButton.gameObject.GetComponent(typeof(KeyboardPlacing));
        kp.userPlacingKeyboard();
    }

    void startPlayingFromBeginning()
    {
        curTS = Time.time;
        GameState.Instance.Playing = true;
        sp.beginSongFromStart(curTS);

        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    // Called by button callback
    public void playPauseSong()
    {
        Debug.Log(sp);
        sp.playPauseSong(Time.time);
    }

    // Called by button callback
    public void forwardSong()
    {
        sp.forwardSong();
    }

    // Called by button callback
    public void rewindSong()
    {
        sp.rewindSong();
    }

    // Update is called once per frame
    void Update()
	{
        /*if (!GameState.Instance.Playing)
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
        Debug.Log(t);*/

        Debug.Log("Keyboard scale: ");
        Debug.Log(this.gameObject.transform.localScale);
        Debug.Log(this.gameObject.transform.lossyScale);

        double curTS = Time.time;

        if(showingGetReady)
        {
            double elapsed = curTS - getReadyStartTS;
            double mul = 1.2;
            if (elapsed < 0.5*mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "3.";
            else if (elapsed < 1 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "3 .";
            else if (elapsed < 1.5 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "2.";
            else if (elapsed < 2 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "2 .";
            else if (elapsed < 2.5 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "1.";
            else if (elapsed < 3 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "1 .";
            else if (elapsed < 3.3 * mul) getReadyButton.gameObject.GetComponent<TextMesh>().text = "GO";
            else getReadyDone();
            return;
        }

		if (sp.getPressedKeyAtTime(curTS) == null)
        {
            if(prevKeyIndex != -1)
            {
                pianoKeys[prevKeyIndex].gameObject.GetComponent<MeshRenderer>().material.color = prevKeyColor;
                prevKeyIndex = -1;
            }
            
            Debug.Log("not playing");
			return;
		}

        KeyPress kp = sp.getPressedKeyAtTime(curTS);
        //Debug.Log(kp.key + " " + kp.finger);

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
        string curSong = GameState.Instance.currentSong;
        if (curSong.Equals("Twinkle Twinkle"))
        {
            return "Assets/song_notes/sample_song.txt";
        }
        return "Assets/song_notes/sample_song_2.txt";
    }
}

public class SongPlayer
{
    double startTS = -1;
    double pauseTS;
    bool paused;
    double prevTempo = 1, tempo = 1;

    SongProviderV1 sp;

    public SongPlayer(string filePath)
    {
        sp = new SongProviderV1(filePath);
        
    }

    public void beginSongFromStart(double curTS)
    {
        Debug.Log("beginSongFromStart");
        startTS = curTS;
        paused = false;

        string tempoString = GameState.Instance.currentTempo;
        tempoString = tempoString.Substring(0, tempoString.Length - 1);
        tempo = double.Parse(tempoString);
        prevTempo = tempo;
    }

    public void playPauseSong(double curTS)
    {
        if (!paused)
        {
            paused = true;
            pauseTS = curTS;
        }
        else
        {
            paused = false;
            startTS += (curTS - pauseTS);
        }
    }

    public void rewindSong()
    {
        startTS += 3/tempo;
    }

    public void forwardSong()
    {
        startTS -= 3/tempo;
    }

    public KeyPress getPressedKeyAtTime(double curTS)
    {
        if (startTS == -1) return null;

        //Debug.Log("tempo " + GameState.Instance.currentTempo);
        string tempoString = GameState.Instance.currentTempo;
        tempoString = tempoString.Substring(0, tempoString.Length - 1);
        tempo = double.Parse(tempoString);

        if(paused) return sp.getPressedKeyAtTime((pauseTS - startTS) * tempo);

        if (tempo != prevTempo)
        {
            Debug.Log("Changed tempo");
            double dist = (curTS - startTS) * prevTempo / tempo;
            startTS = curTS - dist;
            prevTempo = tempo;
        }
        return sp.getPressedKeyAtTime((curTS - startTS) * tempo);
    }
}

// Song provider with no concept of key timestamp
// Keys are played in sequence that they are
// present in input file
public class SongProviderV1
{
	private List<KeyPress> songSequence;

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

	public KeyPress getPressedKeyAtTime(double time)
	{
		//Debug.Log(((int)time)  + " " + songSequence.Count);
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