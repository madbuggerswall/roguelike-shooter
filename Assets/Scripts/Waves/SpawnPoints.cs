using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves {
	public class SpawnPoints : MonoBehaviour {
		Transform[] points;
		Vector2[] positions;

		void Awake() {
			points = GetComponentsInChildren<Transform>()[System.Range.StartAt(1)];
			initializeSpawnPositions();
		}

		void initializeSpawnPositions() {
			positions = new Vector2[points.Length];
			for (int i = 0; i < points.Length; i++) {
				positions[i] = points[i].position;
			}
		}

		public Vector2[] getPositions() { return positions; }
	}
}
