using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public ObjectPool pool;
    public GameObject target;
    public GameObject player;
    public float speed = 0;
    public float lifetime = 3;

    Vector3 corse = Vector3.zero;

    private void OnEnable()
    {
        corse = Vector3.zero;
        //  lifetime = 3;
    }

    public void reset(float time, GameObject player, GameObject target)
    {
        this.player = player;
        lifetime = time;
        this.target = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (corse.sqrMagnitude == 0)
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        else
            transform.position += corse * speed * Time.deltaTime;

        if (Vector3.Magnitude(transform.position - target.transform.position) <= speed && corse.sqrMagnitude == 0)
            corse = (target.transform.position - transform.position).normalized;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            pool.removeObject(gameObject);
    }
}
