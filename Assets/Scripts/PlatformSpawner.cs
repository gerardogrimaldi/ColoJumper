using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	[SerializeField]
	GameManager gameManagerScript;

	[SerializeField]
	CameraFollow cameraScript;

	[SerializeField]
	GameObject[] platformPrefabs;

	[SerializeField]
	GameObject playerPrefab;

	[SerializeField]
	Transform platformSpawned;

	[SerializeField]
	Transform[] platformBins;

	[SerializeField]
	BoxCollider2D playerBoxCol2d;

	GameObject platformGO;

	int totalBreakPlatformSpawn;

	[SerializeField]
	int totalSpawnStart;

	[SerializeField]
	float startYPos;

	public float topPlatformYPos = 0;

	public float distBetween;

	float cameraHalfWidth;

	void Awake(){

		cameraHalfWidth = ((Camera.main.orthographicSize / (float)Screen.height) * (float)Screen.width) - .5f;

	}

	void OnEnable(){

		SpawnPlatformStart ();

	}

	void SpawnPlatformStart(){

		float yPos = startYPos + distBetween;

		float xPos = 0f;

		topPlatformYPos = 0;


		for (int i = 0; i < totalSpawnStart; i++) {

			xPos = Random.Range (-cameraHalfWidth, cameraHalfWidth);
			if (platformBins[0].childCount <= 0) {
				platformGO = Instantiate (platformPrefabs[0], new Vector3 (xPos, yPos, 0f), Quaternion.identity) as GameObject;
				platformGO.transform.SetParent (platformSpawned);
				Platform platformScript = platformGO.GetComponent<Platform> ();
				GetComponentForPlatformScript (platformScript);
			} else {
				platformGO = platformBins[0].GetChild (0).gameObject;
				platformGO.transform.SetParent (platformSpawned);
				platformGO.transform.position = new Vector3 (xPos, yPos, 0f);
			}

			topPlatformYPos = yPos;
			yPos += distBetween;
		}

	}

	public void SpawnNext(){
		int prefabToSpawn;
		if (totalBreakPlatformSpawn <= 1)
			prefabToSpawn = Random.Range (0, platformPrefabs.Length);
		else
			prefabToSpawn = 0;

		switch(prefabToSpawn){
			
		case 0:
			SpawnNormalPlatform ();
			totalBreakPlatformSpawn = 0;
			break;
		case 1:
		case 2:
			SpawnBreakPlatform ();
			break;

		}

	}

	void SpawnNormalPlatform(){


		if (platformBins[0].childCount <= 0) {
			platformGO = Instantiate (platformPrefabs[0], new Vector3 (Random.Range (-cameraHalfWidth, cameraHalfWidth), topPlatformYPos, 0f), Quaternion.identity) as GameObject;
			platformGO.transform.SetParent (platformSpawned);
			Platform platformScript = platformGO.GetComponent<Platform> ();
			GetComponentForPlatformScript (platformScript);
		} else {
			platformGO = platformBins[0].GetChild (0).gameObject;
			platformGO.transform.position = new Vector3 (Random.Range (-cameraHalfWidth, cameraHalfWidth), topPlatformYPos, 0f);
			platformGO.transform.SetParent (platformSpawned);
		}

	}

	void SpawnBreakPlatform(){
		totalBreakPlatformSpawn++;
		float xPos = Random.Range (-cameraHalfWidth, cameraHalfWidth);

		if (platformBins[1].childCount <= 0) {
			platformGO = Instantiate (platformPrefabs[1], new Vector3 (xPos - 0.3f, topPlatformYPos, 0f), Quaternion.identity) as GameObject;
			platformGO.transform.SetParent (platformSpawned);
			Platform platformScript = platformGO.GetComponent<Platform> ();
			GetComponentForPlatformScript (platformScript);
		} else {
			platformGO = platformBins[1].GetChild (0).gameObject;
			platformGO.transform.position = new Vector3 (xPos - 0.3f, topPlatformYPos, 0f);
			platformGO.transform.SetParent (platformSpawned);
		}

		if (platformBins[2].childCount <= 0) {
			platformGO = Instantiate (platformPrefabs[2], new Vector3 (xPos + 0.3f, topPlatformYPos, 0f), Quaternion.identity) as GameObject;
			platformGO.transform.SetParent (platformSpawned);
			Platform platformScript = platformGO.GetComponent<Platform> ();
			GetComponentForPlatformScript (platformScript);
		} else {
			platformGO = platformBins[2].GetChild (0).gameObject;
			platformGO.transform.position = new Vector3 (xPos + 0.3f, topPlatformYPos, 0f);
			platformGO.transform.SetParent (platformSpawned);
		}

	}

	public void SendToBin(Transform toSend, int binType){

		toSend.SetParent (platformBins[binType]);

	}

	void GetComponentForPlatformScript(Platform _platformScript){
		//Mengambil semua komponen yang dibutuhkan oleh PlatformScript
		_platformScript.playerBoxCollider2d = playerBoxCol2d;
		_platformScript.platformSpawnerScript = this;
		_platformScript.cameraTrans = cameraScript.gameObject.transform;
		_platformScript.gameManagerScript = this.gameManagerScript;

	}

	void GetComponentForPlayerScript(PlayerController _playerScript){
		//Mengambil semua komponen yang dibutuhkan oleh playerScript
		_playerScript.cameraTrans = cameraScript.gameObject.transform;
		_playerScript.gameManagerScript = this.gameManagerScript;

	}

	public void ClearSpawnedPlatform(){
		if (platformSpawned.childCount > 0) {
			for (int i = platformSpawned.childCount - 1; i >= 0; i--) {
				Transform p = platformSpawned.GetChild (i);
				SendToBin (p, p.GetComponent<Platform> ().platformType);

			}
		}
	}

}
