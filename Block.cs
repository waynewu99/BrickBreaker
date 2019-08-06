using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public float Health;
	private SpriteRenderer SR;
	private BoxCollider2D bc;
	public BuffList BuffType;
	public GameObject PiercingBuff;
	public GameObject ExplosionBuff;
	public GameObject MultiBallBuff;
	public GameObject Explosion;
	public Color temp;
	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer> ();
		bc = GetComponent<BoxCollider2D> ();
		Health = 0;
		SR.color = Color.clear;
		DisableBC ();
		BuffType = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (BuffType != BuffList.nothing) {
			GetComponent<SpriteRenderer> ().color = Color.LerpUnclamped (temp, Color.clear,Mathf.Sin( 3 * Time.time));
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ball") {
			TakingDamage ();
			if (col.gameObject.GetComponent<BallMoveMent> ().ExplosionActivated) {
				Instantiate (Explosion, transform.position, Quaternion.identity);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Explosion") {
			TakingDamage ();
		}
	}

	public void ActiveBC(){
		bc.enabled = true;
	}
	public void DisableBC(){
		bc.enabled = false;
	}

	public void SetColor(){
		
		if (Health >= 5) {
			SR.color = Color.red;
		} else if (Health == 4) {
			SR.color = Color.yellow;
		} else if (Health == 3) {
			SR.color = Color.blue;
		} else if (Health == 2) {
			SR.color = Color.cyan;
		} else if (Health == 1) {
			SR.color = Color.white;
		} else {
			SR.color = Color.clear;
			DisableBC ();
			if (BuffType == BuffList.Piercing) {
				Instantiate (PiercingBuff, transform.position, Quaternion.identity);
			} else if (BuffType == BuffList.Explosion) {
				Instantiate (ExplosionBuff, transform.position, Quaternion.identity);
			} else if (BuffType == BuffList.Multi) {
				Instantiate (MultiBallBuff, transform.position, Quaternion.identity);
			}
			BlockManager.BlockLeft--;
		}
		temp = GetComponent<SpriteRenderer> ().color;
	}

	public void TakingDamage(){
		GetComponent<AudioSource> ().PlayOneShot (GetComponent<AudioSource> ().clip);
		Health -= BallMoveMent.Damage;
		SetColor ();
	}
}
