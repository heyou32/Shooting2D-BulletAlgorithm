using UnityEngine;

[System.Serializable]
public class Timer
{
    [Min(0f)]
    [Tooltip("Timer")]
    public float timer;
    [Tooltip("Interval")]
    public float interval = 0.15f;
    [Min(0f)]
    [Tooltip("Interval Range. Any value over zero triggers random  mode (random value is between interval and interval + intervalRange)")]
    public float intervalRange;
    [Min(-1)]
    [Tooltip("Counter. (-1 is endless)")]
    public int counter = -1;

    public bool counterReachedZero { get; set; }

    public bool Countdown()
    {
        if (counter == -1) return true;

        if (counter == 0) return false;

        counter -= 1;

        return true;
    }

    public bool GetCounterZeroState()
    {
        var state = counterReachedZero;
        counterReachedZero = false;
        return state;
    }
    // Update will return true when timer reaches zero and resets the timer.
    public bool Update(bool ignoreCounter = false)
    {
        if (ignoreCounter)
        {
            timer -= Time.deltaTime;

            if (timer >= 0) return false;

            if (counter != 0) timer = Random.Range(interval, interval + intervalRange);
        }
        else
        {
            if (counter == 0) return false;

            timer -= Time.deltaTime;

            if (timer >= 0) return false;

            if (counter > 0)
            {
                counter -= 1;
                if (counter == 0) counterReachedZero = true;
            }

            if (counter != 0) timer = Random.Range(interval, interval + intervalRange);
        }

        return true;
    }
}