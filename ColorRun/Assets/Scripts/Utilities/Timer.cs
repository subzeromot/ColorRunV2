using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    #region FIELDS
    private float interval = -1f;
    private float runtime = -1f;
    private float remainingTime = -1f;
    private bool isFinish = false;
    private bool isPause = false;
    private Coroutine coroutine;
    private MonoBehaviour caller;
    #endregion
    #region CONSTRUCTORS
    /// <summary>
    /// Create a new Infinite timer which run at each interval.
    /// Infinite timer will not finish until call end
    /// </summary>
    /// <param name="interval">interval between run</param>
    public Timer(float interval)
    {
        this.interval = interval;
        this.runtime = float.NaN;
    }
    /// <summary>
    /// Create a new timer with runtime which run at each interval
    /// </summary>
    /// <param name="interval">interval between run</param>
    /// <param name="runtime">time until finish</param>
    public Timer(float interval, float runtime)
    {
        this.interval = interval;
        this.runtime = runtime;
    }
    #endregion
    #region PROPERTIES
    public float RemainingTime { get { return this.remainingTime; } }
    public float ElapsedTime
    {
        get
        {
            if(this.runtime != float.NaN)
            {
                return (this.runtime - this.remainingTime);
            }
            throw new NotSupportedException("Infinite timer can not have Elapsed Time");
        }
    }
    public float RunTime { get { return this.runtime; } }
    public float Interval { get { return this.interval; } }
    public bool IsFinish { get { return this.isFinish; } }
    public bool IsPause { get { return this.isPause; } }
    #endregion
    #region METHODS
    /// <summary>
    /// Begin running timer
    /// </summary>
    /// <param name="onInterval">method invoke at each interval</param>
    /// <param name="caller">monobehaviour call function on each interval</param>
    /// <param name="onFinish">method invoke when timer finish</param>
    public void Begin(Action onInterval, MonoBehaviour caller, Action onFinish = null)
    {
        this.isFinish = false;
        this.isPause = false;
        this.remainingTime = this.runtime;
        this.caller = caller;
        this.coroutine = caller.StartCoroutine(StartTimer(onInterval, onFinish));
    }
    /// <summary>
    /// Begin running timer
    /// </summary>
    /// <typeparam name="T">type of method argument</typeparam>
    /// <param name="argument">argument pass on method</param>
    /// <param name="onInterval">method invoke at each interval</param>
    /// <param name="caller">monobehaviour call function on each interval</param>
    /// <param name="onFinish">method invoke when timer finish</param>
    public void Begin<T>(T argument, Action<T> onInterval, MonoBehaviour caller, Action<T> onFinish = null)
    {
        this.isFinish = false;
        this.isPause = false;
        this.remainingTime = this.runtime;
        this.caller = caller;
        this.coroutine = caller.StartCoroutine(StartTimer(argument, onInterval, onFinish));
    }
    /// <summary>
    /// Begin running timer
    /// </summary>
    /// <typeparam name="T1">1st type of method argument</typeparam>
    /// <typeparam name="T2">2nd type of method argument</typeparam>
    /// <param name="argument1">1st argument pass on method</param>
    /// <param name="argument2">2nd argument pass on method</param>
    /// <param name="onInterval">method invoke at each interval</param>
    /// <param name="caller">monobehaviour call function on each interval</param>
    /// <param name="onFinish">method invoke when timer finish</param>
    public void Begin<T1, T2>(T1 argument1, T2 argument2, Action<T1, T2> onInterval, MonoBehaviour caller, Action<T1, T2> onFinish = null)
    {
        this.isFinish = false;
        this.isPause = false;
        this.remainingTime = this.runtime;
        this.caller = caller;
        this.coroutine = caller.StartCoroutine(StartTimer(argument1, argument2, onInterval, onFinish));
    }
    /// <summary>
    /// Begin running timer
    /// </summary>
    /// <typeparam name="T1">1st type of method argument</typeparam>
    /// <typeparam name="T2">2nd type of method argument</typeparam>
    /// <typeparam name="T3">3rd type of method argument</typeparam>
    /// <param name="argument1">1st argument pass on method</param>
    /// <param name="argument2">2nd argument pass on method</param>
    /// <param name="argument3">3rd argument pass on method</param>
    /// <param name="onInterval">method invoke at each interval</param>
    /// <param name="caller">monobehaviour call function on each interval</param>
    /// <param name="onFinish">method invoke when timer finish</param>
    public void Begin<T1, T2, T3>(T1 argument1, T2 argument2, T3 argument3, Action<T1, T2, T3> onInterval, MonoBehaviour caller, Action<T1, T2, T3> onFinish = null)
    {
        this.isFinish = false;
        this.isPause = false;
        this.remainingTime = this.runtime;
        this.caller = caller;
        this.coroutine = caller.StartCoroutine(StartTimer(argument1, argument2, argument3, onInterval, onFinish));
    }
    /// <summary>
    /// Begin running timer
    /// </summary>
    /// <typeparam name="T1">1st type of method argument</typeparam>
    /// <typeparam name="T2">2nd type of method argument</typeparam>
    /// <typeparam name="T3">3rd type of method argument</typeparam>
    /// <typeparam name="T4">4th type of method argument</typeparam>
    /// <param name="argument1">1st argument pass on method</param>
    /// <param name="argument2">2nd argument pass on method</param>
    /// <param name="argument3">3rd argument pass on method</param>>
    /// <param name="argument4">4th argument pass on method</param>
    /// <param name="onInterval">method invoke at each interval</param>
    /// <param name="caller">monobehaviour call function on each interval</param>
    /// <param name="onFinish">method invoke when timer finish</param>
    public void Begin<T1, T2, T3, T4>(T1 argument1, T2 argument2, T3 argument3, T4 argument4, Action<T1, T2, T3, T4> onInterval, MonoBehaviour caller, Action<T1, T2, T3, T4> onFinish = null)
    {
        this.isFinish = false;
        this.isPause = false;
        this.remainingTime = this.runtime;
        this.caller = caller;
        this.coroutine = caller.StartCoroutine(StartTimer(argument1, argument2, argument3, argument4, onInterval, onFinish));
    }
    /// <summary>
    /// Pause the timer until call resume
    /// </summary>
    public void Pause()
    {
        this.isPause = true;
    }
    /// <summary>
    /// Resume the timer after pause
    /// </summary>
    public void Resume()
    {
        this.isPause = false;
    }
    /// <summary>
    /// Extend runtime of timer by seconds
    /// </summary>
    /// <param name="time">extended time in seconds</param>
    public void Extend(float time)
    {
        if (this.remainingTime != float.NaN)
            this.remainingTime += time;
        else
        {
#if GAMO_DEBUG
            Debug.LogWarning("can not extend Infinite timer");
#endif
        }
    }
    /// <summary>
    /// Reduce runtime of time by seconds
    /// </summary>
    /// <param name="time">reduced time in seconds</param>
    public void Reduce(float time)
    {
        if (this.remainingTime != float.NaN)
        {
            this.remainingTime -= time;
            if (this.remainingTime < 0f)
                this.remainingTime = 0f;
        }
        else
        {

#if GAMO_DEBUG
            Debug.LogWarning("can not reduce Infinite timer");
#endif
        }
    }
    /// <summary>
    /// Reset timer as begin
    /// </summary>
    public void Reset()
    {
        this.isPause = false;
        this.remainingTime = this.runtime;
    }
    /// <summary>
    /// End timer right away
    /// </summary>
    public void End()
    {
        if (this.caller != null && this.coroutine != null)
        {
            this.caller.StopCoroutine(this.coroutine);
            this.caller = null;
            this.coroutine = null;
        }
        this.isFinish = true;
    }
    private IEnumerator StartTimer(Action onInterval, Action onFinish = null)
    {
        while (!this.isFinish)
        {
            if (!this.isPause)
                runAction(onInterval, onFinish);
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator StartTimer<T>(T argument, Action<T> onInterval, Action<T> onFinish = null)
    {
        while (!this.isFinish)
        {
            if (!this.isPause)
                runAction(argument, onInterval, onFinish);
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator StartTimer<T1, T2>(T1 argument1, T2 argument2, Action<T1, T2> onInterval, Action<T1, T2> onFinish = null)
    {
        while (!this.isFinish)
        {
            if (!this.isPause) runAction(argument1, argument2, onInterval, onFinish);
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator StartTimer<T1, T2, T3>(T1 argument1, T2 argument2, T3 argument3, Action<T1, T2, T3> onInterval, Action<T1, T2, T3> onFinish = null)
    {
        while (!this.isFinish)
        {
            if (!this.isPause) runAction(argument1, argument2, argument3, onInterval, onFinish);
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator StartTimer<T1, T2, T3, T4>(T1 argument1, T2 argument2, T3 argument3, T4 argument4, Action<T1, T2, T3, T4> onInterval, Action<T1, T2, T3, T4> onFinish = null)
    {
        while (!this.isFinish)
        {
            if (!this.isPause) runAction(argument1, argument2, argument3, argument4, onInterval, onFinish);
            yield return new WaitForSeconds(interval);
        }
    }
    private void runAction(Action onInterval, Action onFinish = null)
    {
        if (this.remainingTime <= 0f && this.remainingTime != float.NaN)
        {
            this.isFinish = true;
            if (onFinish != null) onFinish.Invoke();
            return;
        }
        if (this.remainingTime != float.NaN)
            this.remainingTime -= this.interval;
        if (onInterval != null)
            onInterval.Invoke();
    }
    private void runAction<T>(T argument, Action<T> onInterval, Action<T> onFinish = null)
    {
        if (this.remainingTime <= 0f && this.remainingTime != float.NaN)
        {
            this.isFinish = true;
            if (onFinish != null)
                onFinish.Invoke(argument);
            return;
        }
        if (this.remainingTime != float.NaN)
            this.remainingTime -= this.interval;
        if (onInterval != null)
            onInterval.Invoke(argument);
    }
    private void runAction<T1, T2>(T1 argument1, T2 argument2, Action<T1, T2> onInterval, Action<T1, T2> onFinish = null)
    {
        if (this.remainingTime <= 0f && this.remainingTime != float.NaN)
        {
            this.isFinish = true;
            if (onFinish != null)
                onFinish.Invoke(argument1, argument2);
            return;
        }
        if (this.remainingTime != float.NaN)
            this.remainingTime -= this.interval;
        if (onInterval != null)
            onInterval.Invoke(argument1, argument2);
    }
    private void runAction<T1, T2, T3>(T1 argument1, T2 argument2, T3 argument3, Action<T1, T2, T3> onInterval, Action<T1, T2, T3> onFinish = null)
    {
        if (this.remainingTime <= 0f && this.remainingTime != float.NaN)
        {
            this.isFinish = true;
            if (onFinish != null)
                onFinish.Invoke(argument1, argument2, argument3);
            return;
        }
        if (this.remainingTime != float.NaN)
            this.remainingTime -= this.interval;
        if (onInterval != null)
            onInterval.Invoke(argument1, argument2, argument3);
    }
    private void runAction<T1, T2, T3, T4>(T1 argument1, T2 argument2, T3 argument3, T4 argument4, Action<T1, T2, T3, T4> onInterval, Action<T1, T2, T3, T4> onFinish = null)
    {
        if (this.remainingTime <= 0f && this.remainingTime != float.NaN)
        {
            this.isFinish = true;
            if (onFinish != null)
                onFinish.Invoke(argument1, argument2, argument3, argument4);
            return;
        }
        if (this.remainingTime != float.NaN)
            this.remainingTime -= this.interval;
        if (onInterval != null)
            onInterval.Invoke(argument1, argument2, argument3, argument4);
    }

    public static DateTime ConvertLongToDateTime(long time)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dateTimeFinish = start.AddMilliseconds(time).ToLocalTime();

        return dateTimeFinish;
    }
    #endregion
    #region STRUCTS
    #endregion
    #region CLASSES
    #endregion
}
