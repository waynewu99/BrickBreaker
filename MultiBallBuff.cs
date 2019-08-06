﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallBuff : MonoBehaviour {

	private BuffList buff = BuffList.Multi;
	private float speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.down * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			GameObject[] Balls = GameObject.FindGameObjectsWithTag ("Ball");
			for (int i = 0; i < Balls.Length; i++) {
				Balls [i].GetComponent<BallMoveMent> ().SetBuff (buff);
			}
			Destroy (this.gameObject);
		}
	}
}
