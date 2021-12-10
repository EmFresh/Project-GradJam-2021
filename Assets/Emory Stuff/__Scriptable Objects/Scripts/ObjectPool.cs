using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Util/ObjectPool")]
public class ObjectPool : ScriptableObject
{

    public GameObject poolObj;
    public int cap = 1;
    [SerializeField]
    [Tooltip("Are objects are instantiated and hidden onload")]
    private bool instantiateOnLoad = false;

    //   [SerializeField]
    [SerializeField]
    List<GameObject> objects = new List<GameObject>();
    int m_index = 0;

    bool init = false;
    public void Init()
    {
        if (init) return;

        init = true;

        objects.Clear();
        if (instantiateOnLoad)
            for (int a = 0; a < cap; ++a)
            {
                objects.Add(GameObject.Instantiate<GameObject>(poolObj));
                objects[objects.Count - 1].SetActive(false);
            }
    }

    private void OnDisable()
    {
        init = false;
        objects.Clear();
    }

    //  void OnDisable() => objects.Clear();
    public GameObject getNewObject(Vector3 position, Vector3 rotation, Transform parent = null)
    {
        if (objects.Count < cap)
        {
            objects.Add(Instantiate(poolObj, position, Quaternion.Euler(rotation), parent));
            return objects[objects.Count - 1];
        }
        GameObject tmp = objects[(m_index = (m_index + 1) % (objects.Count))];
        for (int a = 1; a < objects.Count; ++a)
            if (!objects[(m_index + a) % (objects.Count)].activeSelf)
                tmp = objects[((m_index + a) % (objects.Count))];
        tmp.transform.localPosition = position;
        tmp.transform.localRotation = Quaternion.Euler(rotation);
        tmp.SetActive(true);
        ++m_index;
        return tmp;
    }

    public void removeObject(GameObject obj)
    {
        var index = objects.FindIndex(x => { return x == obj; });

        removeObject(index);
    }

    public void removeObject(int index)
    {
        try
        {

            if (objects[index] != null)
            {
                objects[index].SetActive(false);

                for (int a = 0; a < objects.Count; ++a)
                    if (objects[(m_index + a) % objects.Count].activeSelf)
                    {
                        swap(objects, index, (m_index + a) % objects.Count);

                        //   objects.Insert(index, objects[(m_index + a + 1) % objects.Count]);
                        break;
                    }
            }
        }
        catch
        { }

    }

    public static void swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
