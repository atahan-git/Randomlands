using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class V2_EnemySpawner : MonoBehaviour {

	public Transform player;
	//GameObject[] spawnPoints;

	//public int[] diffEnemySpawnChance = new int[3];
	//0 = zomgaz
	//1 = sawtank
	//2 = rollergun

	STORAGE_Enemies prefabs;

	//Vector2 nextSpawnTime = new Vector2(5f,8f);

	public int curWave = 1;

	public int enemyCount = 18;
	public float timeBWaveEnenmies = 0.2f;
	public float waveTime = 40f;
	public float restTime = 30f;

	public GameObject spawnEffect;

	public Image waveFlash;
	public Text waveText;
	public Text levelText;
	// Use this for initialization

	public bool isRepeatedSpawnFinished = false;
	public bool isContinuousSpawnFinished = false;

    public int finalWave = 10;

	//----------------DEBUG-----------------
	public Text TwaveTime;
	public Text TrestTime;
	public Text TcurWaveTime;
	float time;

	public Text TenemyCount;
	public Text TspawnEnemyCount;
	public Text TcurEnemyCount;
	public Text TlastEnemySize;
	//----------------DEBUG-----------------

	void Start () {


		if (PlayerPrefs.GetInt ("Diff", -1) == 4)
			enemyCount = (int)(enemyCount * 1.5f);

		//spawnPoints = GameObject.FindGameObjectsWithTag ("Respawn");
		prefabs = STORAGE_Enemies.s;

		for(int i = 1; i < curWave; i++){
			CalcRestTime ();
			CalcWaveTime ();
			CalcEnemyCount ();

			if (TwaveTime != null && TrestTime != null && TenemyCount != null) {
				TwaveTime.text = ((int)waveTime).ToString ();
				TrestTime.text = ((int)restTime).ToString ();
				TenemyCount.text = enemyCount.ToString ();
			}
		}

		Invoke ("Begin", 3f);
	}

	// Update is called once per frame
	void Update () {

		//----------------DEBUG-----------------
		if(TcurWaveTime != null)
			TcurWaveTime.text = ((int)time).ToString ();
		time += Time.deltaTime;
		//----------------DEBUG-----------------

		//print(Random.Range(0, STORAGE_Enemies.s.EnemyLevels [1].EnemySizes[1].Enemies.Length));
	}

	void Begin(){
		StartCoroutine (FlashWave ());
		StartCoroutine (StartWave ());
	}

	//----------------------------------------------------------------------------------------------------------------------------------

	IEnumerator StartWave (){

        if (curWave == finalWave)
            GameObject.FindObjectOfType<UFO_Script>().lastArea();

        if (curWave == 1)
            enemyCount -= 3;

		//----------------DEBUG-----------------
		time = 0;
		if (TwaveTime != null && TrestTime != null && TenemyCount != null) {
			TwaveTime.text = ((int)waveTime).ToString ();
			TrestTime.text = ((int)restTime).ToString ();
			TenemyCount.text = enemyCount.ToString ();
		}
		//----------------DEBUG-----------------

		print ("Wave has begun");
		UpdateHUD ();


		StartCoroutine(RepeatedSpawnEnemies (enemyCount/2));

		while(!isRepeatedSpawnFinished){
			//yield return new WaitForSeconds (waveTime/3f);
			yield return 0;
		}
		isRepeatedSpawnFinished = false;

		StartCoroutine(ContinuousSpawnEnemies (enemyCount, waveTime));

		while (!isContinuousSpawnFinished) {
			//yield return new WaitForSeconds (waveTime * 6f / 5f);
			yield return 0;
		}
		isContinuousSpawnFinished = false;

		StartCoroutine(RepeatedSpawnEnemies ((int)((float)enemyCount*2f/3f)));

        if (curWave == 1)
            enemyCount += 3;

        CalcRestTime ();
		CalcWaveTime ();
		CalcEnemyCount ();
		curWave++;

		waveFlash.color = Color.red;
		yield return new WaitForSeconds (restTime);
		StartCoroutine (StartWave ());
		StartCoroutine (FlashWave ());
	}

	//----------------------------------------------------------------------------------------------------------------------------------

	IEnumerator RepeatedSpawnEnemies (int spawnEnemyCount){

		print ("Repeated spawn started");

		//int waveStarted = curWave;

		//----------------DEBUG-----------------
		if (TspawnEnemyCount != null)
			TspawnEnemyCount.text = spawnEnemyCount.ToString ();
		//----------------DEBUG-----------------

		int i = 1;
		while (i <= spawnEnemyCount) {

			int lastEnemySize = SpawnEnemyRandom ();

			i += lastEnemySize;
			//----------------DEBUG-----------------
			if (TcurEnemyCount != null && TlastEnemySize != null) {
				TcurEnemyCount.text = i.ToString ();
				TlastEnemySize.text = lastEnemySize.ToString ();
			}
			//----------------DEBUG-----------------
			yield return new WaitForSeconds (timeBWaveEnenmies);

		}

		isRepeatedSpawnFinished = true;
		//print ("Repeated spawn finished" + waveStarted + " - " + curWave);
	}

	IEnumerator ContinuousSpawnEnemies (int spawnEnemyCount, float timeSpan){

		print ("Continious spawn started");

		//----------------DEBUG-----------------
		if (TspawnEnemyCount != null)
			TspawnEnemyCount.text = spawnEnemyCount.ToString ();
		//----------------DEBUG-----------------

		//int waveStarted = curWave;
		int i = 1;
		while (i <= spawnEnemyCount) {

			int lastEnemySize = SpawnEnemyRandom ();

			i += lastEnemySize;
			//----------------DEBUG-----------------
			if (TcurEnemyCount != null && TlastEnemySize != null) {
				TcurEnemyCount.text = i.ToString ();
				TlastEnemySize.text = lastEnemySize.ToString ();
			}
			//----------------DEBUG-----------------
			yield return new WaitForSeconds (timeSpan/(float)(spawnEnemyCount / lastEnemySize));

		}
		isContinuousSpawnFinished = true;
		//print ("Continious spawn finished" + waveStarted + " - " + curWave);

	}

	//----------------------------------------------------------------------------------------------------------------------------------


	int SpawnEnemyRandom () {


		int enemyLevel = 0;
		int enemySize = 0;
		int enemyType = 0;

		/*enemyLevel = (int)Random.Range ((float)curWave / 6.1f, (float)curWave / 2.9f);
		//enemyLevel = 2;
		enemySize = Random.Range(0, STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes.Length);
		enemyType = Random.Range(0, STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes[enemySize].Enemies.Length);*/


		//print (
		//print ("Level:" + enemyLevel + "  Size:" + enemySize + "  Type:" + enemyType);

		GameObject myEnemy = GetRandomEnemy (out enemyLevel, out enemySize, out enemyType);


		//Transform mySpawn = spawnPoints [Random.Range (0, spawnPoints.Length)].transform;
		Transform mySpawn = GetSpawnPoint();

		Instantiate (myEnemy, mySpawn.position, mySpawn.rotation);
		Instantiate (spawnEffect, mySpawn.position, mySpawn.rotation);

		return (enemySize * enemyLevel) + enemyLevel + 1 /*- (int)((float)curWave / 6.1f)*/;

	}

	GameObject GetRandomEnemy(out int enemyLevel, out int enemySize, out int enemyType){


		pickEnemyLevel:
		enemyLevel = (int)Random.Range ((float)curWave / 5.9f, (float)curWave / 1.9f);
		enemyLevel = Mathf.Clamp (enemyLevel, 0, STORAGE_Enemies.s.EnemyLevels.Length - 1);

		if (STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes.Length == 0) //if there are no enemies in that level pick it again
			goto pickEnemyLevel;
		//------------------------------

		pickEnemySize:

		//we want bigger sizes less likely
		//if there are 4 sizes -> size 1 = 8x, size 2=4x, size 3=2x, size 4=1x

		int allChances = (int)Mathf.Pow (2, STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes.Length);
		int random = Random.Range (1, allChances);
		int ourSize = (int)System.Math.Log(random, 2);

        ourSize = (-ourSize) + STORAGE_Enemies.s.EnemyLevels[enemyLevel].EnemySizes.Length - 1;

        enemySize = ourSize;

		try{ 
			int isThisWorking = STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes[enemySize].Enemies.Length;
		}catch{//if something goes wrong try again
			goto pickEnemySize;
		}
		if (STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes[enemySize].Enemies.Length == 0) //if there are no enemies in that size pick it again
			goto pickEnemySize;
		//------------------------------

		enemyType = Random.Range(0, STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes[enemySize].Enemies.Length);

		GameObject myEnemy = null;
		try{
			myEnemy = STORAGE_Enemies.s.EnemyLevels [enemyLevel].EnemySizes [enemySize].Enemies [enemyType];
		}catch{//if something goes wrong try again
			goto pickEnemyLevel;
		}
		if(myEnemy == null)//if the handler that should handle wrong stuff somehow fails try again
			goto pickEnemyLevel;

		return myEnemy;
	}


	//----------------------------------------------------------------------------------------------------------------------------------


	IEnumerator FlashWave (){
		for (int i = 0; i <= 3; i++) {

			waveFlash.color = Color.green;
			yield return new WaitForSeconds (0.2f);

			waveFlash.color = Color.red;
			yield return new WaitForSeconds (0.2f);

		}
		waveFlash.color = Color.green;
	}
		
	void UpdateHUD(){

		waveText.text = curWave.ToString();

        int minLevel = ((int)((float)curWave / 5.9f) + 1);
        int maxLevel = ((int)((float)curWave / 1.9f) + 1);

        maxLevel = Mathf.Clamp(maxLevel, 0, STORAGE_Enemies.s.EnemyLevels.Length);
        minLevel = Mathf.Clamp(minLevel, 0, STORAGE_Enemies.s.EnemyLevels.Length);


        if (maxLevel == minLevel)
        {
			levelText.text = maxLevel.ToString ();

		}else{
			levelText.text = minLevel.ToString () + "-" + maxLevel.ToString ();

		}

	}


	//----------------------------------------------------------------------------------------------------------------------------------


	Transform GetSpawnPoint(){

        pickAnotherPoint:

		float radius = Random.Range (50f, 80f);
		float angleInDegrees = Random.Range (0f, 360f);
		Vector2 origin = new Vector2 (player.position.x, player.position.z);


		// Convert from degrees to radians via multiplication by PI/180        
		float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
		float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.y;

		Transform toBeReturned = transform;

		toBeReturned.position = new Vector3 (x, 50, y);
		RaycastHit hit;

		if(Physics.Raycast (toBeReturned.position, Vector3.down, out hit, 100)) {
			toBeReturned.position = new Vector3 (x, hit.point.y, y);
        }
        else
        {
            goto pickAnotherPoint;
        }

		return toBeReturned;

	}

	//----------------------------------------------------------------------------------------------------------------------------------

	void CalcRestTime (){
		//restTime *= 1.1f;
	}
	void CalcWaveTime (){
		waveTime *= 1.1f;
	}
	void CalcEnemyCount (){
		enemyCount = (int)(enemyCount * 1.3f);
	}
}
