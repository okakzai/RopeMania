﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpForce;
	public bool grounded;
	public bool candoublejump;

	public int jumps;
	public Font pixelFont;
	string scoreText;

	bool gameOver=false;

	public GameObject playerPrefab;

	public Sprite jumpSprite;
	public Sprite idleSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
		HandleJumpsText ();
	}

	void HandleInput()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump ();
		}
	}

	void SwitchIdleAnimation()
	{
		
		playerPrefab.GetComponent<SpriteRenderer> ().sprite =idleSprite;
	}

	void SwitchJumpAnimation()
	{
		playerPrefab.GetComponent<SpriteRenderer> ().sprite = jumpSprite;
	}

	//Handle ground contact
	void OnCollisionEnter2D(Collision2D coll)
	{
		//Contact with ground
		if (coll.gameObject.tag == "Ground")
		{
			grounded = true;
			SwitchIdleAnimation ();
		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Contact with row
		if (!gameOver) {
			if (other.gameObject.tag == "MovingRope" && grounded==true) {
				string layer =other.GetComponent<SpriteRenderer>().sortingLayerName;
				if (layer == "Front") {
					gameOver = true;
					playerPrefab.transform.Rotate (Vector3.forward * -90);
				}
			}
		}
	
	}

	void RestartGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}


	void Jump()
	{
		if (grounded && !gameOver) {
			SwitchJumpAnimation ();
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, 0);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			candoublejump = true;
			grounded = false;
			jumps++;
		}
		if (gameOver) {
			RestartGame ();
		}
	}

	public void HandleJumpsText()
	{
		scoreText = "Jumps: " + jumps;
	}

	void OnGUI()
	{

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;

		GUI.skin.font = pixelFont;
		style.normal.textColor = Color.green;
		GUI.Label(new Rect(20, 20, 100, 100), scoreText, style);

		if (gameOver) {
			style.normal.textColor = Color.red;
			Rect rect = new Rect((Screen.width)/2-70, (Screen.height)/2-50, 0, 0);
			GUI.Label(rect, "Game Over",style);
		}
	}
}
