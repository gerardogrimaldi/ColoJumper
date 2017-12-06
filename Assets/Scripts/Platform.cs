using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public BoxCollider2D playerBoxCollider2d;

	public PlatformSpawner platformSpawnerScript;

	public Transform cameraTrans;

	public GameManager gameManagerScript;

	public int platformType;

	BoxCollider2D myBoxCollider2d;

	Rigidbody2D myRigidBody2d;

	AudioSource myAudioSource;

	float cameraHalfHeight;


	void Awake () {

		myBoxCollider2d = GetComponent<BoxCollider2D> ();
		if (platformType == 1 || platformType == 2) {
			myRigidBody2d = GetComponent<Rigidbody2D> ();
			myAudioSource = GetComponent<AudioSource> ();
		}
	}

	void Start(){

		cameraHalfHeight = Camera.main.orthographicSize + .5f;

	}
	
	// Update is called once per frame
	void Update () {
		switch(platformType){
		case 0: 
			#region Platform Normal
			if (gameManagerScript.gameRunning) {
				if (playerBoxCollider2d.bounds.min.y < myBoxCollider2d.bounds.min.y && myBoxCollider2d.isTrigger == false)
					myBoxCollider2d.isTrigger = true;
				else if (playerBoxCollider2d.bounds.min.y > myBoxCollider2d.bounds.max.y && myBoxCollider2d.isTrigger == true)
					myBoxCollider2d.isTrigger = false;
			}
			#endregion
			break;
		case 1:
		case 2:
			#region Platform Break
			if (gameManagerScript.gameRunning && myRigidBody2d.isKinematic) {
				if (playerBoxCollider2d.bounds.min.y < myBoxCollider2d.bounds.min.y && myBoxCollider2d.isTrigger == false)
					myBoxCollider2d.isTrigger = true;
				else if (playerBoxCollider2d.bounds.min.y > myBoxCollider2d.bounds.max.y && myBoxCollider2d.isTrigger == true)
					myBoxCollider2d.isTrigger = false;
			}
			#endregion
			break;
		}
		if (transform.position.y <= cameraTrans.position.y - cameraHalfHeight)
			ImDone ();


	}

	void OnCollisionEnter2D(Collision2D col){
		if (platformType == 1 || platformType == 2) {
			if (col.gameObject.CompareTag ("Player")) {
				myRigidBody2d.isKinematic = false;
				myAudioSource.Play ();
				myBoxCollider2d.isTrigger = true;
			}
		}
	}

	void ImDone(){

		platformSpawnerScript.topPlatformYPos += platformSpawnerScript.distBetween;
		platformSpawnerScript.SpawnNext ();

		if (platformType == 1 || platformType == 2)
			myRigidBody2d.isKinematic = true;

		platformSpawnerScript.SendToBin (transform,platformType);

	}
}
