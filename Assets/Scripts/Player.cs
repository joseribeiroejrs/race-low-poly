using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, IA };
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0;
    public float CurrrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimer;
    private int lastCheckpointPassed = 0;
    private Transform checkpointParent;
	private int checkpointCount;
	private int checkpointLayer;
	private Car carController;

	private void Awake()
	{
        checkpointParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        carController = GetComponent<Car>();
	}

	// Update is called once per frame
	void Update()
    {
        if (controlType == ControlType.HumanInput)
		{
            carController.Steer = GameManager.Instance.inputController.SteerInput;
            carController.Throttle = GameManager.Instance.inputController.ThrottelInput;
        }
        updateCurrentLapTime();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != checkpointLayer)
		{
            return;
		}

        if (other.gameObject.name == "1")
		{
            if (lastCheckpointPassed == checkpointCount)
			{
                EndLap();
			}

            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
			{
                StartLap();
			}
            return;
		}

        if (other.gameObject.name == (lastCheckpointPassed + 1).ToString())
		{
            lastCheckpointPassed++;
		}
	}

	void StartLap()
    {
        Debug.Log("Start lap!");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimer = Time.time;
    }

    void EndLap()
	{
        LastLapTime = Time.time - lapTimer;
        BestLapTime = Mathf.Min(BestLapTime, LastLapTime);
        Debug.Log("End lap: LapTime: " + LastLapTime);
        Debug.Log("Your best lap is " + BestLapTime);
    }

    void updateCurrentLapTime ()
	{
        CurrrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;
    }
}
