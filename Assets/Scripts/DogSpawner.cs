﻿using UnityEngine;
using System.Collections;

public class DogSpawner : MonoBehaviour {

	public GameObject[] dogs;
	public bool spawnToLeft;
	public float speed;
	float respawnTime;
	public float startTime;
	GameObject thePlayer;
	PlayerController objPlayer;
	bool thereIsADog=false;
	int lowestNumber=20;
	int maxNumber=60;

	void Start () {
		respawnTime = Random.Range (lowestNumber, maxNumber);
		InvokeRepeating("InstanceDog", startTime, respawnTime);
		thePlayer = GameObject.Find("Player");
		objPlayer = thePlayer.GetComponent<PlayerController>();
	}
		
	void Update () {

	}

	void InstanceDog()
	{
		if (!objPlayer.getGameOver ()) {
			GameObject[] dogs = GameObject.FindGameObjectsWithTag("dog");
			if (dogs.Length <= 1) {
				if(!thereIsADog)
					SpawnDog ();
			}
		}

	}

	public void setThereIsADog(bool isThere)
	{
		thereIsADog = isThere;
	}

	void SpawnDog()
	{
		Vector2 forceVector = spawnToLeft ? Vector2.left : Vector2.right;
		Vector3 firePosition = new Vector3(transform.position.x, -3.2f, 0);
		int index = Random.Range (0, dogs.Length-1);
		GameObject randomDog = dogs [index];
		GameObject bPrefab = Instantiate(randomDog, firePosition, Quaternion.identity) as GameObject;
		if (!spawnToLeft) {
			bPrefab.GetComponent<SpriteRenderer> ().flipX = true;
			bPrefab.GetComponent<SpriteRenderer> ().sortingOrder =- 0;
		} else {
			bPrefab.GetComponent<SpriteRenderer> ().sortingOrder =- 0;
		}
		bPrefab.GetComponent<Rigidbody2D>().AddForce(forceVector * speed);
		thereIsADog = true;
	}
}
