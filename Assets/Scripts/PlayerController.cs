using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform cameraTrans;

	public GameManager gameManagerScript;

	[SerializeField]
	AudioSource myAudioSource;

	[SerializeField]
	AudioClip jumpSound;
	[SerializeField]
	AudioClip fallSound;

	public int score = 0;

	[SerializeField]
	Vector3 firstPos;

	[SerializeField]
	float playerFirstYPos;

	[SerializeField]
	float moveSpd;

	[SerializeField]
	float jumpPower;

	Rigidbody2D myRigidbody2d;
	WaitForSeconds waitFallSound;

	public bool isDead = false;

	void Awake(){
		waitFallSound = new WaitForSeconds (fallSound.length/4f);
		myRigidbody2d = GetComponent<Rigidbody2D> ();

	}

	void Start(){

		StartCoroutine (ImAlive ());

	}

	public void Reset(){
		myRigidbody2d.velocity = Vector3.zero;
		transform.position = firstPos;

	}

	void Update(){
		if (gameManagerScript.gameRunning) {
			FlipCharacter ();
			CheckIfDead ();
			SetScore ();
		}
	}

	void FixedUpdate () {
		if(gameManagerScript.gameRunning)
			Movement ();

	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.CompareTag("Jumpable")){
			myRigidbody2d.velocity = new Vector2 (myRigidbody2d.velocity.x, 0f);
			myRigidbody2d.AddForce (Vector2.up * jumpPower);
			if(!isDead)
				PlaySound (jumpSound);
		}

	}

	int TouchInput(){
		#if UNITY_EDITOR
		if(Application.isPlaying)
			return Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
		#endif

		#if UNITY_ANDROID
		if (Input.touchCount > 0){
			if (Input.GetTouch (0).position.x < Screen.width / 2f)
				return -1;
			else if (Input.GetTouch (0).position.x >= Screen.width / 2f)
				return 1;
		}
		return 0;
		#endif


	}

	void Movement(){
		myRigidbody2d.AddForce (Vector2.right * TouchInput() * moveSpd);
		float maxXVel = Mathf.Clamp (myRigidbody2d.velocity.x, -11150f, 11150f);
		myRigidbody2d.velocity = new Vector2 (maxXVel, myRigidbody2d.velocity.y);
	}

	void FlipCharacter(){
		if (TouchInput () < 0 && transform.localScale.x > 0)			//Kiri
			transform.localScale = Vector3.one - (Vector3.right * 2);
		else if (TouchInput () > 0 && transform.localScale.x < 0)		//Kanan
			transform.localScale = Vector3.one;
	}

	void CheckIfDead(){

		if(transform.position.y <= cameraTrans.position.y - 8f && !isDead){
			//Player Mati
			isDead = true;
			StartCoroutine (ImDead ());
		}

	}

	IEnumerator ImDead(){

		PlaySound (fallSound);

		yield return waitFallSound;

		gameManagerScript.GameOver (score);

		yield return waitFallSound;
		yield return waitFallSound;
		yield return waitFallSound;


		gameManagerScript.UIRestart(true);

	}

	IEnumerator ImAlive(){

		PlaySound (fallSound);

		yield return waitFallSound;

		Reset ();

		yield return waitFallSound;
		yield return waitFallSound;
		yield return waitFallSound;


		gameManagerScript.UIRestart(false);

	}

	void SetScore(){

		score = Mathf.Max(score,Mathf.FloorToInt(transform.position.y - playerFirstYPos) * 10);

	}

	void PlaySound(AudioClip clipToPlay){
		myAudioSource.clip = clipToPlay;
		myAudioSource.Play ();
	}
}
