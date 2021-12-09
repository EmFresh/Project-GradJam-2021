using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//oden (in unity asset store)
[CreateAssetMenu(/* fileName = "RhythmicShooting", */ menuName = "Scriptable Objects/RhythmicActions")]
public class RhythmicActions : ScriptableObject
{
    [SerializeField]
    private List<BeatMap> m_maps = new List<BeatMap>();
    private List<BeatMap> mapInstance = new List<BeatMap>();


    public UnityEvent<BeatMap.NoteData, int> onNewNote { get; private set; } = new UnityEvent<BeatMap.NoteData, int>();
    public UnityEvent<int, int> onNewBeatCount { get; private set; } = new UnityEvent<int, int>();




    private void OnEnable()
    {
        if (mapInstance.Count == 0)
            foreach (var map in m_maps)
            {
                mapInstance.Add(Instantiate(map));
                mapInstance[mapInstance.Count - 1].onNewNote.AddListener(OnNewNote);
                mapInstance[mapInstance.Count - 1].beatCounter.onNewBeat.AddListener(OnNewBeat);
            }
    }

    private void OnDisable()
    {
        //    foreach (var map in mapInstance)
        //    {
        //        map.onNewNote.RemoveListener(OnNewNote);
        //        map.beatCounter.onNewBeat.AddListener(OnNewBeat);
        //    }
        //    mapInstance.Clear();

    }
    void OnNewNote(BeatMap.NoteData data, int id)
    {
        onNewNote.Invoke(data, id);
    }

    void OnNewBeat(int beatCount, int id)
    {
        onNewBeatCount.Invoke(beatCount, id);
    }
    public void setBeatOffset(int offset)
    {
        foreach (var map in mapInstance)
            map.beatOffset = offset;
    }
    public void beatUpdate(float time, int offset, int id)
    {
        setBeatOffset(offset);
        foreach (var map in mapInstance)
            map.noteUpdate(time, id);

    }

    // This Update is for TESTING ONLY
    string TestUpdate()
    {

        float songTime = Time.timeSinceLevelLoad;
        string tmp = null;

        //   if (quarter.isNewBeat(songTime))
        //       tmp = tmp != null ? tmp + "quarter+ " : "quarter+ ";
        //
        //   if (eighth.isNewBeat(songTime))
        //       tmp = tmp != null ? tmp + "eigth+ " : "eight+ ";
        //   if (half.isNewBeat(songTime))
        //       tmp = tmp != null ? tmp + "half+ " : "half+ ";

        return tmp;

        // print(songTime);

    }
}
