using UnityEngine;
using System.Collections;

public class me2 : MonoBehaviour {

	CharacterController controller; 
	Vector3 moveDirection; 
	public GameObject Teddyprefab;
	public GameObject Teddy;
	public bool check =true;
	public bool get = false;
	public int postCount;
	public float speed = 1000;
	float fSpeed = 53.0f; 
	void Start () {
		controller = GetComponent("CharacterController") as CharacterController; 
	}

	void Update () {

		Vector3 forward = GameObject.Find("Main Camera2").transform.TransformDirection( Vector3.forward ); 
		Vector3 right = GameObject.Find("Main Camera2").transform.TransformDirection( Vector3.right ); 
		moveDirection = Input.GetAxis("Horizontal2") * right + Input.GetAxis("Vertical2") * forward; 
		moveDirection *= fSpeed; 

		// 移動 
		controller.Move( moveDirection * Time.deltaTime ); 


		if (Input.GetKey ( KeyCode.F)) {
			transform.Rotate (0, -5, 0);
		}
		if (Input.GetKey ( KeyCode.H)) {
			transform.Rotate (0, 5, 0);
		}			

		raytest ();
		if (Input.GetKey (KeyCode.B) && get == false && check == false) {

			if (postCount > 0) {

				int Ca = postCount;
				Vector3 nw = GameObject.Find ("meteddy2").transform.position;
				nw.z += 3;
				Vector3 force;

				GameObject bullets = GameObject.Instantiate (Teddyprefab, nw, Quaternion.identity)as GameObject;

				force = this.gameObject.transform.forward * speed;
				// Rigidbodyに力を加えて発射
				bullets.GetComponent<Rigidbody>().AddForce (force);
				bullets.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
				postCount--;
				float ranX = Random.Range (-200, 200);
				float ranY = Random.Range (100, 150);
				float ranZ = Random.Range (-200, 200);

				Instantiate (Teddy, new Vector3 (ranX, ranY, ranZ), Quaternion.identity);
				if (Ca > postCount && postCount > 0) {
					check = true;
				}
			}
		} else {
			check = false;
		}
	}

	void raytest()
	{

		Ray ray = new Ray (transform.position, new Vector3 (0, 0, 1));
		RaycastHit hit;
		int distance = 6;

		Vector3 nw = GameObject.Find ("meteddy2").transform.position;
		nw.z += 3;
		if (Physics.Raycast (ray, out hit, distance)) {

			if (Input.GetKey (KeyCode.Space) && get == false && check == false && hit.collider.tag == "teddy") {
				Destroy (hit.collider.gameObject);
				postCount++;
				get = false;
				check = false;
			} else {
				get = false;
				check = true;
			}

		}
	}


}
