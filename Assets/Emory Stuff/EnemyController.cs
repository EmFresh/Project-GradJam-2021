using System;
using FMODUnity;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    StudioEventEmitter bgm;

    [SerializeField]
    ObjectPool bulletPool;

    [SerializeField]
    RhythmicActions map;

    [SerializeField]
    int beatOffset;

    private void Awake()
    {
        map = Instantiate(map);

        map.onNewBeatCount.AddListener(timeing);
        bulletPool.Init();


        //id = count++;
        //count %= 1000;//max number of enemies
    }

    //public UnityEvent<BeatMap.NoteData,int> bm;
    private void OnEnable()
    {
        print("ID: " + GetInstanceID());
        map.onNewNote.AddListener(shoot);
    }

    private void OnDisable()
    {
        map.onNewNote.RemoveListener(shoot);
    }

    void shoot(BeatMap.NoteData a, int b)
    {
        print("shoot!");

        var sfx = GetComponents<StudioEventEmitter>()[(short)a.type];
        sfx.EventInstance.setPitch(1 + a.pitch * 0.05f);
        sfx.Play();
        // // sfx.EventInstance.
        // RuntimeManager.PlayOneShot(sfx1);
    }

    void timeing(int a, int b)
    {
        print("ID: " + GetInstanceID());
        // source.Play();
    }

    private void Update()
    {
        //  if (map)
        int time;
        bgm.EventInstance.getTimelinePosition(out time);//time in Milliseconds

        bgm.EventInstance.setVolume(.3f);

        map.beatUpdate(time * 0.001f, beatOffset, GetInstanceID());
    }


}