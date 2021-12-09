using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(/* fileName = "BeatCounter",  */menuName = "Scriptable Objects/BeatCounter")]
public class BeatCounter : ScriptableObject
{

    public string notename;

    [SerializeField]
    protected float spb = 0;//seconds per beat
    [SerializeField]
    protected float npb = 1;//notes per beat


    /// <summary>
    /// event for every beat counted
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="int"></typeparam>
    /// <returns></returns>
    public UnityEvent<int, int> onNewBeat { get; private set; } = new UnityEvent<int, int>();

    /// <summary>
    /// Must be called in update loop for events to trigger
    /// </summary>
    /// <param name="time"></param>
    /// <param name="id"></param>
    public void beatUpdate(float time, int id)
    {
        if (onNewBeat != null)
            if (isNewBeat(time))
                onNewBeat.Invoke(getCurrentBeatCount(time), id);
    }

    /// <summary>
    /// sets beats per minute (a.k.a seconds per beat)
    /// </summary>
    /// <param name="bpm"></param>
    public void setBPM(int bpm) => spb = 60.0f / (float)bpm;
    /// <summary>
    /// sets notes per beat
    /// </summary>
    /// <param name="npb"></param>
    public void setNPB(float npb) => this.npb = npb;

    /// <summary>
    /// determines which note beat the song is on 
    /// at a given time in seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns>beat count</returns>
    public int getCurrentBeatCount(float time) => Mathf.FloorToInt(time / (spb * npb));
    /// <summary>
    /// determines what time a beat was made
    /// given the beat number
    /// </summary>
    /// <param name="beat"></param>
    /// <returns>time in seconds</returns>
    public float getCurrentBeatTime(int beat) => (float)beat * (float)(spb * npb);

    /// <summary>
    /// determines which note beat the song is on 
    /// at a given time in seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns>beat count</returns>
    public static int getCurrentBeatCount(float time, float bpm, float npb) => Mathf.FloorToInt(time / (float)(60.0f / bpm * npb));
    /// <summary>
    /// determines what time a beat was made
    /// given the beat number
    /// </summary>
    /// <param name="beat"></param>
    /// <returns>time in seconds</returns> 
    public static float getCurrentBeatTime(int beat, float bpm, float npb) => (float)beat * (float)(60.0f / bpm * npb);

    int lastBeat = 0;
    /// <summary>
    /// checks if a new beat was made since the last time checked
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool isNewBeat(float time)
    {
        int tmp = getCurrentBeatCount(time);
        if (lastBeat != tmp)
        {
            lastBeat = tmp;
            return true;
        }
        return false;
    }
}
