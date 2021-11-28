//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;

public class Timer
{
    private float startTime = 0.0f;
    private float elapsedTime = 0.0f;
    private float delay = 0.0f;

    private bool isLoop = false;
    private bool isStopped = false;
    private bool isEnable = false;

    public float StartTime
    {
        get
        {
            return startTime;
        }

        set
        {
            startTime = value;
        }
    }

    public float ElapsedTime
    {
        get
        {
            return elapsedTime;
        }

        set
        {
            elapsedTime = value;
        }
    }

    public float Delay
    {
        get
        {
            return delay;
        }
    }

    public float NowTime
    {
        get
        {
            if (isStopped)
                startTime = Time.time - (elapsedTime - startTime);
            elapsedTime = Time.time;
            return elapsedTime - startTime;
        }
    }

    public bool IsLoop
    {
        get
        {
            return isLoop;
        }

        set
        {
            isLoop = value;
        }
    }

    public bool IsDone
    {
        get
        {
            if (NowTime > delay && isEnable)
            {
                if (isLoop) startTime = Time.time;
                return true;
            }
            return false;
        }
    }

    public bool IsStopped
    {
        get
        {
            return isStopped;
        }

        set
        {
            isStopped = value;
        }
    }

    public bool IsEnable
    {
        get
        {
            return isEnable;
        }

        set
        {
            startTime = Time.time;
            elapsedTime = Time.time;
            isEnable = value;
        }
    }

    public Timer(float tdelay, bool loop)
    {
        startTime = Time.time;
        elapsedTime = Time.time;
        delay = tdelay;
        isEnable = false;
        isLoop = loop;
    }
}