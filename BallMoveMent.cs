 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveMent : MonoBehaviour {

	public float speed;
	public float safespeed;
	public static float Damage;
	public GameObject ExplosionBuffEffect;
	public GameObject MultiBall;

	private Rigidbody2D rb;
	private Vector2 preset;
	private float xAxisSafetytimer;
	private bool xAxisSafety = false;

	public bool ExplosionActivated = false;
	private float EATimer = 0;
	private bool piercingActivated = false;
	private float PATimer = 0;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (Vector2.up * 3*speed);
		rb.AddForce (Vector2.left * 3*speed);
		ExplosionBuffEffect.SetActive(false);
		Damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 temp = rb.velocity;
		if (Mathf.Abs (rb.velocity.y) < 3f) {
			if (rb.velocity.y > 0) {
				rb.velocity = new Vector2 (temp.x, 3);
			} else if (rb.velocity.y < 0) {
				rb.velocity = new Vector2 (temp.x, -3);
			}
		}
		if (xAxisSafety) {
			xAxisSafetytimer += Time.deltaTime;
			if (xAxisSafetytimer > 0.1f) {
				rb.AddForce (Vector2.right * 2 * safespeed);
			}
		} else {
			xAxisSafetytimer = 0;
		}
		if (piercingActivated) {
			PATimer -= Time.deltaTime;
			if (PATimer <= 0) {
				piercingActivated = false;
				GetComponent<SpriteRenderer> ().color = Color.white;
			}
		}
		if (ExplosionActivated) {
			EATimer -= Time.deltaTime;
			if (EATimer <= 0) {
				ExplosionActivated = false;
				ExplosionBuffEffect.SetActive (false);
			}
		}

		if (transform.position.y < -6) {
			Destroy (this.gameObject);
		}
	}

	public void SetBuff(BuffList bufftype){
		if (bufftype == BuffList.Piercing) {
			piercingActivated = true;
			PATimer += 20;
			GetComponent<SpriteRenderer> ().color = Color.red;
		}
		if (bufftype == BuffList.Explosion) {
			ExplosionActivated = true;
			EATimer += 20;
			ExplosionBuffEffect.SetActive(true);
		}
		if (bufftype == BuffList.Multi) {
			if (GameObject.FindGameObjectsWithTag ("Ball").Length < 30) {
				for (int i = 0; i < 5; i++) {
					GameObject b = Instantiate (MultiBall, transform.position, Quaternion.identity);
					float randomdirectionX = (float)Random.Range (-8, 8);
					float randomdirectionY = (float)Random.Range (-8, 8);
					b.GetComponent<Rigidbody2D> ().AddForce (new Vector3 (randomdirectionX, randomdirectionY, 0));
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Wall") {
			xAxisSafety = true;
			if (col.transform.position.x < transform.position.x) {
				safespeed = 10;
			} else if (col.transform.position.x > transform.position.x) {
				safespeed = -10;
			}
		}
	}
	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Wall") {
			xAxisSafety = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Block"&&piercingActivated) {
			col.gameObject.GetComponent<Block> ().TakingDamage ();
		}
	}
}
