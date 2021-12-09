using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
[CreateAssetMenu(/* fileName = "BeatMap", */ menuName = "Scriptable Objects/BeatMap")]
public class BeatMap : ScriptableObject
{



    public BeatCounter beatCounter;

    public int bpm, beatOffset;
    public float startTime, endTime;
    [SerializeField]
    float timeLoop = 0;

    [SerializeField]
    private List<NoteData> beats = new List<NoteData>();


    public UnityEvent<NoteData, int> onNewNote { get; private set; } = new UnityEvent<NoteData, int>();

    public enum BEAT_TYPE
    {
        TYPE1,
        TYPE2,
        TYPE3,
        NONE,
    }

    [Serializable]
    public struct NoteData
    {
        public int beatNum;
        public BEAT_TYPE type;
        public int pitch;
        public float hold;
    }


    private void OnEnable()
    {
        timeLoop = 0;
        beatCounter.setBPM(bpm);
        beatCounter.onNewBeat.AddListener(onBeats);
    }
    private void OnDisable()
    {
        timeLoop = 0;
        beatCounter.setBPM(bpm);
        beatCounter.onNewBeat.RemoveListener(onBeats);
    }

    public void setBeatOffset(int offset)
    {
        beatOffset = offset;
    }

    /// <summary>
    /// how beats are found
    /// </summary>
    /// <param name="beatNum"></param>
    /// <param name="id"></param>
    private void onBeats(int beatNum, int id)
    {
        beatCounter.setBPM(bpm);
        beatNum += beatOffset;
        float currentTime = 0;

        if ((currentTime = beatCounter.getCurrentBeatTime(beatNum)) >= startTime && beatNum > beatCounter.getCurrentBeatCount(startTime))
        {
            if (beatCounter.getCurrentBeatTime(beatNum) < endTime || endTime <= startTime)
            {
                int finalbeat = 0;
                foreach (var beat in beats)
                {
                    if (beatNum - beatCounter.getCurrentBeatCount
                   (startTime + timeLoop) == beat.beatNum)
                        if (beat.type != BEAT_TYPE.NONE)
                            onNewNote.Invoke(beat, id);
                        else
                            timeLoop = (beatCounter.getCurrentBeatTime(beatNum) - startTime);

                    finalbeat = beat.beatNum > finalbeat ? beat.beatNum : finalbeat;
                }
                bool missedFinalBeat = (beatNum > beatCounter.getCurrentBeatCount
                (startTime + timeLoop + beatCounter.getCurrentBeatTime(finalbeat)));

                if (missedFinalBeat)
                    while (currentTime - (startTime + timeLoop) > 0)
                        timeLoop += (beatCounter.getCurrentBeatTime(finalbeat) - startTime);

            }
        }
        else
        {
            timeLoop = 0;
        }
    }


    public void noteUpdate(float time, int id)
    {
        beatCounter.beatUpdate(time, id);
    }


    //Not IMPLIMENTED
    public void readAudioMap(string file)
    {
        //read text file'n store data
        var lines = File.ReadAllLines(file);
        beats.Clear();

        //interperate
        foreach (var line in lines)
        {
            if (line.Substring(line.IndexOf("=") + 1).Contains("T "))//tempo (bpm)
            {

            }
        }
    }
}
