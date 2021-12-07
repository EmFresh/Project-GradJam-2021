using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject poolObj;
    public int cap = 1;

    [SerializeField]
    List<GameObject> objects = new List<GameObject>();
    int index = 0;

    public GameObject getNewObject(Vector3 position, Vector3 rotation)
    {
        if (objects.Count < cap)
        {
            objects.Add(Instantiate(poolObj, position, Quaternion.Euler(rotation), transform));
            return objects[objects.Count - 1];
        }

        var tmp = objects[(index = (index + 1) % (objects.Count))];
        tmp.transform.localPosition = position;
        tmp.transform.localRotation = Quaternion.Euler(rotation);
        tmp.SetActive(true);
        return tmp;
    }

    public void removeObject(GameObject obj)
    {
        try
        {
            var thing = objects.FindIndex(x => { return x == obj; });

            if (objects[thing] != null)
            {
                objects[thing].SetActive(false);

                for (int a = 1; a < objects.Count; ++a)
                    if (objects[(index + a) % objects.Count].activeSelf)
                    {
                        swap<GameObject>(objects, thing, (index + a) % objects.Count);
                        break;
                    }
            }
        }
        catch
        { print("oops"); }

    }

    public static void swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objects.Count < cap)
                getNewObject(transform.position, Vector3.zero);
            else
                getNewObject(new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10)), Vector3.zero);
        }
        if (Input.GetMouseButtonDown(1))
        {

            removeObject(objects[objects.Count - 1]);
        }
    }
}
