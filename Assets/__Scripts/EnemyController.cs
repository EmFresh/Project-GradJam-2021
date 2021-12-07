using UnityEngine;

public class EnemyController : MonoBehaviour
{
    static int count;

    [SerializeField]
    RhythmicActions map;

    int id;

    private void Start()
    {
        id = count++;
        count %= 1000;//max number of enemies
    }

    private void OnEnable()
    {
        map.onNewNoteAction.AddListener(delegate { shoot(); });

    }

    private void OnDisable()
    {
        map.onNewNoteAction.RemoveListener(delegate { shoot(); });
    }

    void shoot()
    {
        print("shoot!");
    }


    private void Update()
    {


        //  if (map)
        map.beatUpdate(Time.timeSinceLevelLoad, id);
    }


}