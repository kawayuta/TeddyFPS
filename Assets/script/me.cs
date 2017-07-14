using UnityEngine;
using System.Collections;

public class me : MonoBehaviour {

	CharacterController controller; 
	Vector3 moveDirection; 
	public GameObject Teddyprefab;
	public bool check =true;
	public bool get = false;
	public int postCount;
	public float speed = 1000;
	float fSpeed = 53.0f; 
	void Start () {
		controller = GetComponent("CharacterController") as CharacterController; 
	}
	
	void Update () {

		Vector3 forward = GameObject.Find("Main Camera").transform.TransformDirection( Vector3.forward ); 
		Vector3 right = GameObject.Find("Main Camera").transform.TransformDirection( Vector3.right ); 
		moveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward; 
		moveDirection *= fSpeed; 

		// 移動 
		controller.Move( moveDirection * Time.deltaTime ); 


		if (Input.GetKey ( KeyCode.F)) {
			transform.Rotate (0, -1, 0);
		}
		if (Input.GetKey ( KeyCode.H)) {
			transform.Rotate (0, 1, 0);
		}			

		raytest ();
		if (Input.GetKey (KeyCode.B) && get == false && check == false) {

			if (postCount > 0) {
				
				int Ca = postCount;
				Vector3 nw = GameObject.Find ("meteddy").transform.position;
				nw.z += 3;
				Vector3 force;

				GameObject bullets = GameObject.Instantiate (Teddyprefab, nw, Quaternion.identity)as GameObject;

				force = this.gameObject.transform.forward * speed;
				// Rigidbodyに力を加えて発射
				bullets.GetComponent<Rigidbody>().AddForce (force);
				postCount--;
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

		Vector3 nw = GameObject.Find ("meteddy").transform.position;
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
