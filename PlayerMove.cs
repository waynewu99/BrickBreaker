using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffList{
	nothing = 0,
	Piercing = 1,
	Explosion = 2,
	Multi = 3
}

public class PlayerMove : MonoBehaviour {
	public static PlayerMove instance;
	public float speed;
	public static int BallsLeft;
	public GameObject Ball;
	public Transform SpawnPos;
	// Use this for initialization
	void Start () {
		BlockManager.GameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		transform.position += new Vector3 (h*Time.deltaTime*speed, 0, 0);
		if (Input.GetMouseButtonDown(0)&&GameObject.FindGameObjectWithTag("Ball") == null&& MouseEnterAndOut.MouseOnButton == false && BallsLeft > 0) {
			Instantiate (Ball, SpawnPos.position, SpawnPos.rotation);
			BallsLeft--;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ball") {
			float a = transform.position.x - col.transform.position.x;
			Vector2 temp = col.gameObject.GetComponent<Rigidbody2D> ().velocity;
			temp = new Vector2 (-a*3,temp.y);
			col.gameObject.GetComponent<Rigidbody2D> ().velocity = temp;
		}
	}


}
