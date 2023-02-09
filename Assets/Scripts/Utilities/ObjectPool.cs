using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
