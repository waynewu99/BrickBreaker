using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2);
		transform.localScale = new Vector3 (0, 0, 0);
		GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp (transform.localScale,new Vector3 (5, 5, 5),0.05f);
	}
}
