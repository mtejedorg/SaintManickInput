using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float totalAllowedErrorTimeSec = 1;
    public int initialBPM = 120;

    private int hitsOnTime = 0;
    private float dt;
    private int count = 0;
    private int allowedErrorCount;

    private float deltaBeats;
    private float bpm2count;
    private int firstAllowedCount;
    private int centerCount;
    private int lastAllowedCount;

	// Use this for initialization
	void Start () {
        dt = Time.fixedDeltaTime;
        int totalAllowedErrorCount = (int)Mathf.Round(totalAllowedErrorTimeSec / dt);
        allowedErrorCount = totalAllowedErrorCount / 2;

        getAllowedCountsByBPM(initialBPM);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        count++;

        if (Input.anyKeyDown)
        {
            if (hitsOnTime == 0) {
                hitsOnTime = 1;
            } else {
                //solo suma si es a tiempo, si no reinicia
                if (onTime()) {
                    hitsOnTime++;
                } else {
                    hitsOnTime = 0;
                }
            }
        }

        if (hitsOnTime == 3)
        {
            print("Eres el puto amo");
        }
	}

    void getAllowedCountsByBPM(int bpm)
    {
        int bps = bpm / 60;
        deltaBeats = bps * dt; //beats/sec * sec/fixedUpdates = beats/fixedUpdates where FU = count
        centerCount = (int)(bpm / deltaBeats);
        firstAllowedCount = centerCount - allowedErrorCount;
        lastAllowedCount = centerCount + allowedErrorCount;
    }

    bool onTime()
    {
        return count >= firstAllowedCount && count <= lastAllowedCount;
    }
}
