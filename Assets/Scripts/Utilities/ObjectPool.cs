using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
	void reset();
	void returnToPool();
}

public class ObjectPool : MonoBehaviour {
	[SerializeField] List<GameObject> pool = new List<GameObject>();

	GameObject addObject(GameObject prefab) {
		prefab.SetActive(false);

		GameObject pooledObject = GameObject.Instantiate(prefab);
		pooledObject.name = prefab.name;
		pooledObject.transform.SetParent(transform);
		pool.Add(pooledObject);
		return pooledObject;
	}

	// Returns a disabled pooled object. Creates one if there's none.
	GameObject getObject(GameObject prefab) {
		foreach (GameObject pooledObject in pool) {
			if (prefab.name == pooledObject.name && !pooledObject.activeInHierarchy) {
				return pooledObject;
			}
		}
		return addObject(prefab);
	}

	// Spawns a pooled object at position with rotation
	public GameObject spawn(GameObject prefab, Vector3 position, Vector3 eulerAngles) {
		GameObject spawnedObject = getObject(prefab);
		spawnedObject.transform.position = position;
		spawnedObject.transform.eulerAngles = eulerAngles;
		spawnedObject.SetActive(true);
		return spawnedObject;
	}

	// Spawns a pooled object at position
	public GameObject spawn(GameObject prefab, Vector3 position) {
		GameObject spawnedObject = getObject(prefab);
		spawnedObject.transform.position = position;
		spawnedObject.transform.eulerAngles = Vector3.zero;
		spawnedObject.SetActive(true);
		return spawnedObject;
	}

	// Spawns a pooled object at world center
	public GameObject spawn(GameObject prefab) {
		GameObject spawnedObject = getObject(prefab);
		spawnedObject.transform.position = Vector3.zero;
		spawnedObject.transform.eulerAngles = Vector3.zero;
		spawnedObject.SetActive(true);
		return spawnedObject;
	}

	// Generic
	T addObject<T>(T prefab) where T : MonoBehaviour {
		prefab.gameObject.SetActive(false);

		T pooledObject = GameObject.Instantiate(prefab);
		pooledObject.name = prefab.name;
		pooledObject.transform.SetParent(transform);
		pool.Add(pooledObject.gameObject);
		return pooledObject;
	}

	T getObject<T>(T prefab) where T : MonoBehaviour {
		foreach (GameObject pooledObject in pool) {
			if (prefab.gameObject.name == pooledObject.name && !pooledObject.activeInHierarchy) {
				return pooledObject.GetComponent<T>();
			}
		}
		return addObject(prefab);
	}

	public T spawn<T>(T prefab) where T : MonoBehaviour {
		T spawnedObject = getObject(prefab);
		spawnedObject.transform.position = Vector3.zero;
		spawnedObject.transform.eulerAngles = Vector3.zero;
		spawnedObject.gameObject.SetActive(true);
		return spawnedObject;
	}
	
	public T spawn<T>(T prefab, Vector3 position) where T : MonoBehaviour {
		T spawnedObject = getObject(prefab);
		spawnedObject.transform.position = position;
		spawnedObject.transform.eulerAngles = Vector3.zero;
		spawnedObject.gameObject.SetActive(true);
		return spawnedObject;
	}
}
