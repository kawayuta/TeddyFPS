using UnityEngine;
using System.Collections;

public class TeddyRandom2 : MonoBehaviour {

	public GameObject Teddyprefab;
	float time = 3;
	Vector3	endPosition;
	private float startTime;
	private Vector3 startPosition;

	// Use this for initialization

	void OnEnable ()
	{
		if (time <= 0) {
			GameObject.Find ("Main Camera2").transform.position = endPosition;
			enabled = false;
			return;
		}

		startTime = Time.timeSinceLevelLoad;
		startPosition = GameObject.Find ("Main Camera2").transform.position;
	}

	void Start () {

		float ranXp = Random.Range (-200, 200);
		float ranZp = Random.Range (-200, 200);
		GameObject.Find ("meteddy2").transform.position = new Vector3 (ranXp, 2.6f, ranZp);
		for (int i = 0; i < 200; i++) {

			float ranX = Random.Range (-200, 200);
			float ranY = Random.Range (30, 60);
			float ranZ = Random.Range (-200, 200);

			Instantiate (Teddyprefab, new Vector3 (ranX, ranY, ranZ), Quaternion.identity);

		}
		StartCoroutine ("rote");

	}

	private IEnumerator rote() {
		yield return new WaitForSeconds (2.5f);

		for (float i = 90; i > 0; i--) {
			GameObject.Find ("Main Camera2").transform.rotation = Quaternion.Euler(i,0,0);
			yield return null;  
		}

	}

	void Update ()
	{
		Vector3 enda = GameObject.Find ("meteddy2").transform.position;
		var diff = Time.timeSinceLevelLoad - startTime;
		if (diff > time) {
			GameObject.Find ("Main Camera2").transform.position = endPosition;
			enabled = false;
		}

		var rate = diff / time;


		GameObject.Find ("Main Camera2").transform.position = Vector3.Lerp (startPosition, enda, rate);
	}


	void OnDrawGizmosSelected ()
	{
		#if UNITY_EDITOR

		if( !UnityEditor.EditorApplication.isPlaying || enabled == false ){
			startPosition = transform.position;
		}

		UnityEditor.Handles.Label(endPosition, endPosition.ToString());
		UnityEditor.Handles.Label(startPosition, startPosition.ToString());
		#endif
		Gizmos.DrawSphere (endPosition, 0.1f);
		Gizmos.DrawSphere (startPosition, 0.1f);

		Gizmos.DrawLine (startPosition, endPosition);
	}
}
