using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(/* fileName = "BeatCounter",  */menuName = "Scriptable Objects/BeatCounter")]
public class BeatCounter : ScriptableObject
{

    public string notename;

    [SerializeField]
    protected float bps = 0;//beats per second
    [SerializeField]
    protected float npb = 1;//notes per beat


    //public UnityAction<int, int> onNextBeat
    [HideInInspector] public UnityEvent<int, int> onNextBeat = new UnityEvent<int, int>();

    public void beatUpdate(float time, int id)
    {

        if (onNextBeat != null)
            if (isNewBeat(time))
                onNextBeat.Invoke(getCurrentBeatCount(time), id);
    }


    public void setBPM(int bpm) => bps = 60 / (float)bpm;
    public void setNPB(float npb) => this.npb = npb;

    public int getCurrentBeatCount(float time) => (int)(time / (bps * npb));
    public float getCurrentBeatTime(int beat) => beat * (bps * npb);

    int lastBeat = 0;
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
