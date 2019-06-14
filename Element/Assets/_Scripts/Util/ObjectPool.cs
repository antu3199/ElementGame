using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Model for object pools
public class ObjectPool : MonoBehaviour
{

  PooledObject prefab;

  public int maxObjects;

  //List of one type of object (prefab)
  List<PooledObject> availableObjects = new List<PooledObject>();

  //Instantiaes a new pool: Attaches Object pool script
  public static ObjectPool GetPool(PooledObject prefab)
  {
    GameObject obj;
    ObjectPool pool;

    //Just to avoid a Unity hiccup
    if (Application.isEditor)
    {
      obj = GameObject.Find(prefab.name + " Pool");
      if (obj)
      {
        pool = obj.GetComponent<ObjectPool>();
        if (pool)
        {
          return pool;
        }
      }
    }
    obj = new GameObject(prefab.name + " Pool");
    pool = obj.AddComponent<ObjectPool>();
    pool.prefab = prefab;
    return pool;
  }

  //Returns a reference to an available object (i.e) last of list 
  //Note it is last for efficiency - Don't have to shift list as much
  public PooledObject GetObject()
  {
    PooledObject obj;
    int len = availableObjects.Count;
    if (availableObjects.Count > maxObjects - 1 && len != 0)
    {
      obj = availableObjects[0];
      availableObjects.RemoveAt(0);
      obj.gameObject.SetActive(true);
    }
    else if (maxObjects == 0 && len != 0)
    {
      obj = availableObjects[len - 1];
      availableObjects.RemoveAt(len - 1);
      obj.gameObject.SetActive(true);
    }
    else
    {
      obj = Instantiate<PooledObject>(prefab);
      obj.transform.SetParent(transform, false);
      obj.Pool = this;
      //availableObjects.Add(obj);

    }

    return obj;
  }

  //Adds object to available prefab list
  public void AddObject(PooledObject obj)
  {
    obj.gameObject.SetActive(false);
    if (!availableObjects.Contains(obj))
    {
      availableObjects.Add(obj);
    }
  }

}

