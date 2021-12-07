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

    [HideInInspector]
    public UnityEvent<BeatMap.NoteData, int> onNewNoteAction = new UnityEvent<BeatMap.NoteData, int>();




    private void OnEnable()
    {
        foreach (var map in m_maps)
            map.onNextNote.AddListener(OnUpdate);
    }
    private void OnDisable()
    {
        foreach (var map in m_maps)
        {
            map.onNextNote.RemoveListener(OnUpdate);
        }

    }
    void OnUpdate(BeatMap.NoteData data, int id)
    {
        onNewNoteAction.Invoke(data, id);
    }

    public void beatUpdate(float time, int id)
    {
        foreach (var map in m_maps)
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
