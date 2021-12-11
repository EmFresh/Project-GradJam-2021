using System;
using FMODUnity;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public
    StudioEventEmitter bgm;

    [SerializeField]
    private GameObject player;
    // [SerializeField]
    List<GameObject> noteTargets = new List<GameObject>();

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

        var tmp = player.GetComponentInChildren<PlayerMov2>();


        var rand = new System.Random();
        beatOffset = rand.Next(0, 8);
        //noteTargets.

        noteTargets.Add(tmp._parry1Obj);
        noteTargets.Add(tmp._parry2Obj);
        noteTargets.Add(tmp._parry3Obj);
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
        if (Vector3.Distance(player.transform.GetChild(0).position, transform.position) > 50)
            return;

        print("shoot!");

        var sfx = GetComponents<StudioEventEmitter>()[(short)a.type];
        sfx.EventInstance.setPitch(1 + a.pitch * 0.05f);
        sfx.EventInstance.setVolume(.5f);
        sfx.Play();

        var tmp = bulletPool.getNewObject(transform.position, Vector3.zero);
        Color[] colours = { new Color(1, 0, 0), new Color(0, 0, 1), new Color(1, 1, 0) };
        tmp.GetComponent<Renderer>().material.color = colours[(short)a.type];
        tmp.GetComponent<BulletControl>().pool = bulletPool;
        tmp.GetComponent<BulletControl>().reset(3, player, noteTargets[(short)a.type]);
        //  tmp.GetComponent<BulletControl>().lifetime = 3;
        //  tmp.GetComponent<BulletControl>().target = noteTargets[(short)a.type];

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