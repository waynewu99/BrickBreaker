using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour {
	public GameObject[] Blocks;
	public static int CurrentLevel = 0;
	public static int BlockLeft;
	private int totallevel = 5;
	public static bool win;
	public static bool GameOver;
	private bool paused;
	// Use this for initialization
	public Text THEText;
	public Text BallLeftText;
	public GameObject THEPanel;

	void Start () {
		win = true;
		paused = false;
		THEText.text = "Brick Break";
	}
	
	// Update is called once per frame
	void Update () {
		if (BlockLeft == 0) {
			win = true;
			Time.timeScale = 0;
		}
		if (GameObject.FindGameObjectWithTag ("Ball") == null && PlayerMove.BallsLeft == 0 && BlockManager.CurrentLevel>0) {
			SetGameOver ();
		}
		if (win && CurrentLevel>0) {
			SetWin ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)&&win==false&&GameOver==false&&paused == false) {
			Debug.Log ("Work pls");
			Time.timeScale = 0;
			THEText.text = "Game Paused\nPress ESC again to restart";
			THEText.color = Color.black;
			paused = true;
		}else if (Input.GetKeyDown (KeyCode.Escape) && paused == true) {
			Time.timeScale = 1;
			THEText.color = Color.clear;
			paused = false;
		}
		BallLeftText.text = "Balls:" + PlayerMove.BallsLeft;
	}

	 void SetLevel(int level){
		GameObject[] destroyBall = GameObject.FindGameObjectsWithTag ("Ball");
		GameObject[] destroyBuff = GameObject.FindGameObjectsWithTag ("Buff");
		GameObject[] Exp = GameObject.FindGameObjectsWithTag ("Explosion");
		for (int i = 0; i < destroyBall.Length; i++) {
			Destroy (destroyBall [i]);
		}
		for (int i = 0; i < destroyBuff.Length; i++) {
			Destroy (destroyBuff [i]);
		}
		for (int i = 0; i < Exp.Length; i++) {
			Destroy (Exp [i]);
		}
		for (int i = 0; i <=127; i++) {
			Blocks [i].GetComponent<Block> ().Health = 0;
			Blocks [i].GetComponent<Block> ().BuffType = BuffList.nothing;
			Blocks [i].GetComponent<Block> ().SetColor ();
		}
		if (level == 1) {
			BlockLeft = 0;
			for (int i = 0; i < 32; i++) {
				if (i % 2 == 0) {
					Blocks [i].GetComponent<Block> ().Health = 2;
				} else {
					Blocks [i].GetComponent<Block> ().Health = 1;
				}
				if (i == 31||i==29) {
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Piercing;
				}
				if (i == 25) {
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Multi;
				}
				if (i == 27) {
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Explosion;
				}

				Blocks [i].GetComponent<Block> ().ActiveBC ();
				Blocks [i].GetComponent<Block> ().SetColor ();
			}
			BlockLeft = 32;
		} else if (level == 2) {
			BlockLeft = 0;
			for (int i = 0; i < 64; i++) {
				if (i < 8 || (i >= 56 && i <= 64)) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				if (i % 8 == 0 || i % 8 == 7) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				Blocks [i].GetComponent<Block> ().Health += 1;
				if (Blocks [i].GetComponent<Block> ().Health == 3) {
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Piercing;
				}
				Blocks [i].GetComponent<Block> ().ActiveBC ();
				Blocks [i].GetComponent<Block> ().SetColor ();
			}
			BlockLeft = 64;
		} else if (level == 3) {
			BlockLeft = 0;
			int r = 0;
			int b = 0;
			int y = 0;
			int c = 0;
			int w = 0;
			for (int i = 0; i < 128; i++) {
				Blocks [i].GetComponent<Block> ().Health += 1;
				if (i < 80) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				if (i < 56) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				if (i < 32) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				if (i < 16) {
					Blocks [i].GetComponent<Block> ().Health += 1;
				}
				if (Blocks [i].GetComponent<Block> ().Health == 5) {
					r++;
				} else if (Blocks [i].GetComponent<Block> ().Health == 4) {
					y++;
				} else if (Blocks [i].GetComponent<Block> ().Health == 3) {
					b++;
				} else if (Blocks [i].GetComponent<Block> ().Health == 2) {
					c++;
				} else if (Blocks [i].GetComponent<Block> ().Health == 1) {
					w++;
				}
				if ((r < 9 && Blocks [i].GetComponent<Block> ().Health == 5) ||
					(y > 24 && Blocks [i].GetComponent<Block> ().Health == 4&&i%3==0) ||
					(b > 16 && Blocks [i].GetComponent<Block> ().Health == 3&&i%3==1) ||
					(c > 16 && Blocks [i].GetComponent<Block> ().Health == 2&&i%3==0) ||
					(w > 32 && Blocks [i].GetComponent<Block> ().Health == 1&&i%3==1))
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Explosion;

				Blocks [i].GetComponent<Block> ().ActiveBC ();
				Blocks [i].GetComponent<Block> ().SetColor ();
			}
			BlockLeft = 128;
		} else if (level == 4) {
			BlockLeft = 0;
			for (int i = 0; i < 128; i++) {
				int p = (int)Mathf.Abs (Mathf.Ceil (Mathf.Sin (i) * 2));
				int q = (int)Mathf.Abs (Mathf.Ceil (Mathf.Cos (i) * 2));
				int t = (int)Mathf.Ceil (Mathf.Tan (i) * 2);
				Blocks [i].GetComponent<Block> ().Health = p + q + t;
				if (Blocks [i].GetComponent<Block> ().Health > 5) {
					Blocks [i].GetComponent<Block> ().Health = 5;
				}
				if (Blocks [i].GetComponent<Block> ().Health == 1) {
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Multi;
				}
				Blocks [i].GetComponent<Block> ().ActiveBC ();
				Blocks [i].GetComponent<Block> ().SetColor ();				
			}
			BlockLeft = 98;
		} else if (level == 5) {
			BlockLeft = 0;
			for (int i = 0; i < 128; i++) {
				float p = Mathf.PingPong (i, 5);
				Blocks [i].GetComponent<Block> ().Health = (int)Mathf.Ceil (p);
				if(i%7==1&&Blocks [i].GetComponent<Block> ().Health ==1){
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Piercing;
				}else if(i%11==1&&Blocks [i].GetComponent<Block> ().Health ==2){
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Explosion;
				}else if(i%13==1&&Blocks [i].GetComponent<Block> ().Health ==3){
					Blocks [i].GetComponent<Block> ().BuffType = BuffList.Multi;
				}
				Blocks [i].GetComponent<Block> ().ActiveBC ();
				Blocks [i].GetComponent<Block> ().SetColor ();				
			}
			BlockLeft = 115;
		}
		if (CurrentLevel > totallevel) {
			THEPanel.SetActive(true);
		}
	}

	public void StartButton(){
		if (CurrentLevel <= totallevel) {
			if (win == true) {
				THEText.color = Color.clear;
				THEPanel.SetActive (false);
				Destroy (GameObject.FindGameObjectWithTag ("Ball"));
				PlayerMove.BallsLeft = 3;
				CurrentLevel++;
				SetLevel (CurrentLevel);
				win = false;
				Time.timeScale = 1;
			}
			if (GameOver||CurrentLevel>totallevel) {
				SceneManager.LoadScene (0);
				CurrentLevel = 0;
			}
		}
	}
	public void SetWin(){
		if (CurrentLevel < totallevel) {
			THEText.color = Color.yellow;
			THEText.text = "Victory\nPress the Button to Proceed";
		} else if (CurrentLevel >= totallevel) {
			THEText.color = Color.yellow;
			THEText.text = "Victory\nPress the Button to Restart";
		}
	}

	public void SetGameOver(){
		BlockManager.CurrentLevel -= 1;
		GameOver = true;
		THEText.color = Color.blue;
		THEText.text = "GAME OVER\nPress the Button to Restart";
	}
}
