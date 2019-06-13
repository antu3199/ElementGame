using UnityEngine;
using System.Collections;


//Every pooled object inherits from this class
public class PooledObject : MonoBehaviour {
	public int maxObjects;

	[System.NonSerialized]
	ObjectPool poolInstanceForPrefab;

	//Separates the type of prefab , and gets pool if there is one/creates it
	//Used as a replacement for instantiate
	public T GetPooledInstance<T> (Vector3 position) where T : PooledObject {
		if (!poolInstanceForPrefab) {
			poolInstanceForPrefab = ObjectPool.GetPool(this);
			poolInstanceForPrefab.maxObjects = maxObjects;
		}
    T newObject = (T)poolInstanceForPrefab.GetObject();
    newObject.transform.position = position;

		return newObject;
	}

	public ObjectPool Pool { get; set; }

	//Used when an object is supposed to be destroyed
	public void ReturnToPool () {
		if (Pool) {
			Pool.AddObject(this);
		}
		else {
			//Destroy(gameObject);
		}
	}

	public virtual void OnSpawn(){
		Debug.Log ("fail");
		//Override me!
	}

}
