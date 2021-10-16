using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;
    public Text UITextCurrentLap;
    public Text UITextCurrentTime;
    public Text UITextLastLapTime;
    public Text UITextBestLapTime;

    public Player UpdateUIForPlayer;

    private int currentLap = -1;
    private float currentTime;
    private float lastLapTime;
    private float bestLapTime = Mathf.Infinity;

    // Update is called once per frame
    void Update()
    {
        if (UpdateUIForPlayer == null)
		{
            return;
		}
        updateCurrentLap();
        updateCurrentTime();
        updateBestLapTime();
        updateLastLapTime();
    }

    int getTimeInMinutes (float time)
	{
        return (int)time / 60;
	}

    float getTimeInSeconds (float time)
	{
        return time % 60;
	}

    string getTimeFormated (float time)
	{
        return $"{getTimeInMinutes(time)}:{getTimeInSeconds(time):00.000} ";
	}

    void updateCurrentLap ()
	{
        if (UpdateUIForPlayer.CurrentLap != currentLap)
		{
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"LAP: {currentLap}";
		}
	}

    void updateCurrentTime ()
	{
        if (UpdateUIForPlayer.CurrrentLapTime != currentTime)
		{
            currentTime = UpdateUIForPlayer.CurrrentLapTime;
            UITextCurrentTime.text = $"TIME: {getTimeFormated(currentTime)}";
        }
    }

    void updateLastLapTime ()
	{
        if (UpdateUIForPlayer.LastLapTime != lastLapTime)
		{
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            UITextLastLapTime.text = $"LAST: {getTimeFormated(lastLapTime)}";
        }
    }

    void updateBestLapTime ()
	{
        if (UpdateUIForPlayer.BestLapTime != bestLapTime)
		{
            bestLapTime = UpdateUIForPlayer.BestLapTime;
            UITextBestLapTime.text = $"BEST: {getTimeFormated(bestLapTime)}";
        }
    }
}
