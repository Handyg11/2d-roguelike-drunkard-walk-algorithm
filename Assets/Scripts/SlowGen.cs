using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlowGen : MonoBehaviour
{
public enum gridSpace {empty, floor, wall, enemy, ammo, healthKit, food, player};
	bool isPlayerSpawned = false;
	public gridSpace[,] grid;
	int roomHeight, roomWidth;
	float chanceToSpawnEnemy = 0.05f, chanceToSpawnAmmo = 0.006f, chanceToSpawnHealthKit = 0.002f, chanceToSpawnFood = 0.005f;
	Vector2 roomSizeWorldUnits = new Vector2(30,30);
	float worldUnitsInOneGridCell = 1;
	struct walker{
		public Vector2 dir;
		public Vector2 pos;
	}
	List<walker> walkers;
	float chanceWalkerChangeDir = 0.5f, chanceWalkerSpawn = 0.05f;
	float chanceWalkerDestoy = 0.05f;
	int maxWalkers = 10;

	float x, y;
	float percentToFill = 0.2f; 
	public GameObject wallObj ,floorObj, floorObj2, floorObj3, enemyObj, enemy2Obj, enemy3Obj, ammoObj, healthKitObj, foodObj, playerObj;

	private Vector2 playerSpawn;
    public Vector3 initialPlayerSpawn;

	public CameraFollow cameraFollow; 

	//public Vector3 offset;
	public Vector3 playerCamPosContainer;

	public int enemyCount;
	
	public int enemyDecrease = 1;
	float timeBetweenLoops = 0.1f, timeBetweenLoopsLong = 0.1f;

	void Start () {
		StartUp();
	}
	//Bagian penciptaan ruangan secara umum
	public void StartUp(){
		//Membuat ruangan
		Setup();

		//Menciptakan template Lantai
		StartCoroutine(CreateFloors());

		
	}
	//fungsi untuk membuat ruangan pada permainan
	void Setup(){
		//Mencari ukuran Grid berdasarkan ukuran room yang ditentukan
		//ukuran room yang ditentukan sebesar 30x30
		roomHeight = Mathf.RoundToInt(roomSizeWorldUnits.x / worldUnitsInOneGridCell);
		roomWidth = Mathf.RoundToInt(roomSizeWorldUnits.y / worldUnitsInOneGridCell);
		//Menciptakan Grid
		grid = new gridSpace[roomWidth,roomHeight];
		//membuat gridspace kosong sebagai default gridspace
		for (int x = 0; x < roomWidth-1; x++){
			for (int y = 0; y < roomHeight-1; y++){
				grid[x,y] = gridSpace.empty;
			}
		}
		//Membuat list Walker
		walkers = new List<walker>();
		//Menciptakan walker
		walker newWalker = new walker();
		newWalker.dir = RandomDirection();
		//Mencari bagian tengah pada Grid
		Vector2 spawnPos = new Vector2(Mathf.RoundToInt(roomWidth/ 2.0f),
										Mathf.RoundToInt(roomHeight/ 2.0f));
		newWalker.pos = spawnPos;
		//Memasukan Walker kedalam walker list
		walkers.Add(newWalker);
		// AstarPath.active.Scan();
	}
	IEnumerator CreateFloors(){
		int iterations = 0;//loop tidak akan bekerja untuk selamanya
		do{
			bool spawned = false;
			//membuat template lantai pada setiap posisi walker yang disimpan dari walker list
			foreach (walker myWalker in walkers){
				if (grid[(int)myWalker.pos.x,(int)myWalker.pos.y] != gridSpace.floor){
					Spawn(myWalker.pos.x,myWalker.pos.y, floorObj);//update visuals
					spawned = true;
				}
				grid[(int)myWalker.pos.x,(int)myWalker.pos.y] = gridSpace.floor;
			}
			//Kemungkinan untuk menghapus suatu walker
			int numberChecks = walkers.Count;
			for (int i = 0; i < numberChecks; i++){
				//apabila Counter Walker dimulai lebih dari 1
				//dan memiliki kemungkinan lebih rendah untuk dihancurkan
				if (Random.value < chanceWalkerDestoy && walkers.Count > 1){
					walkers.RemoveAt(i);
					Debug.Log(walkers.Count);
					break;
				}
			}
			//Kemungkinan untuk walker mengambil arah baru dari list
			for (int i = 0; i < walkers.Count; i++){
				//apabila random value yang didapat lebih kecil dari kemungkinan mengambil arah baru
				if (Random.value < chanceWalkerChangeDir){
					walker thisWalker = walkers[i];
					thisWalker.dir = RandomDirection();
					walkers[i] = thisWalker;
				}
			}	
			//Kemungkinan untuk menciptakan walker yang baru
			numberChecks = walkers.Count;
			for (int i = 0; i < numberChecks; i++){
				//apabila random value yang didapat lebih kecil dari kemungkinan menciptakan walker baru
				//dan cap jumlah walker agar tidak melebihi walker yang dibuat
				if (Random.value < chanceWalkerSpawn && walkers.Count < maxWalkers){
					walker newWalker = new walker();
					newWalker.dir = RandomDirection();
					newWalker.pos = walkers[i].pos;
					walkers.Add(newWalker);
				}
			}
			//Menggerakkan walker
			for (int i = 0; i < walkers.Count; i++){
				walker thisWalker = walkers[i];
				thisWalker.pos += thisWalker.dir;
				walkers[i] = thisWalker;				
			}
			//membatasi walker agar tidak keluar dari grid
			for (int i =0; i < walkers.Count; i++){
				walker thisWalker = walkers[i];
				//membuat ruang untuk dinding
				thisWalker.pos.x = Mathf.Clamp(thisWalker.pos.x, 1, roomWidth-2);
				thisWalker.pos.y = Mathf.Clamp(thisWalker.pos.y, 1, roomHeight-2);
				walkers[i] = thisWalker;
			}
			//mengecek iterasi
			if ((float)NumberOfFloors() / (float)grid.Length > percentToFill){
				break;
			}
			iterations++;
			if (spawned){
				yield return new WaitForSeconds(timeBetweenLoops);//make it wait
			}
		}while(iterations < 100000);
		StartCoroutine(CreateWalls());
	}

	//Menciptakan dinding dari lantai yang telah dibuat
	IEnumerator CreateWalls(){
		// loop untuk grid space
		for (int x = 0; x < roomWidth-1; x++){
			for (int y = 0; y < roomHeight-1; y++){
				//mencari spaces dari floor yang dicek
				if (grid[x,y] == gridSpace.floor){
					bool placed = false;
					//jika sekitarnya kosong maka ditaruh dindng
					if (grid[x,y+1] == gridSpace.empty){
						grid[x,y+1] = gridSpace.wall;
						placed = true;
					}
					if (grid[x,y-1] == gridSpace.empty){
						grid[x,y-1] = gridSpace.wall;
						placed = true;
					}
					if (grid[x+1,y] == gridSpace.empty){
						grid[x+1,y] = gridSpace.wall;
						placed = true;
					}
					if (grid[x-1,y] == gridSpace.empty){
						grid[x-1,y] = gridSpace.wall;
						placed = true;
					}
					if (placed){
						yield return new WaitForSeconds(timeBetweenLoops/2);
					}
				}
			}
		}
		StartCoroutine(RemoveSingleWalls());
	}

	//menghilangkan dinding yang berdiri sendiri
	IEnumerator RemoveSingleWalls(){
		//mengecek gridspace
		for (int x = 0; x < roomWidth-1; x++){
			for (int y = 0; y < roomHeight-1; y++){
				//jika ada dinding, cek bagian samping dinding
				if (grid[x,y] == gridSpace.wall){
					bool allFloors = true;
					//cek setiap dinding apabila disekitarnya hanya lantai semua
					for (int checkX = -1; checkX <= 1 ; checkX++){
						for (int checkY = -1; checkY <= 1; checkY++){
							if (x + checkX < 0 || x + checkX > roomWidth - 1 || 
								y + checkY < 0 || y + checkY > roomHeight - 1){
								continue;
							}
							if ((checkX != 0 && checkY != 0) || (checkX == 0 && checkY == 0)){
								continue;
							}
							if (grid[x + checkX,y+checkY] != gridSpace.floor){
								allFloors = false;
							}
						}
					}
					if (allFloors){
						grid[x,y] = gridSpace.floor;
						yield return new WaitForSeconds(timeBetweenLoopsLong);
					}
				}
			}
		}StartCoroutine(SpawnLevel());
	}
	//Spawn enemy
	IEnumerator CreateEnemy(){
		//menghitung jumlah enemy yang dibuat
		int tempCount=0;
		for (int x=0; x < roomWidth; x++){
			for (int y=0; y < roomHeight; y++){
				//mengecek kemungkinan spawn enemy di dalam grid
				if(grid[x,y] == gridSpace.floor && Random.value < chanceToSpawnEnemy){
					bool placed = false;
					grid[x,y] = gridSpace.enemy;
					//mengecek apabila ada pemain terdekat
					if(grid[x+1,y+1] != gridSpace.player 
					&& grid[x-1,y-1] != gridSpace.player 
					&& grid[x,y+1] != gridSpace.player 
					&& grid[x,y-1] != gridSpace.player 
					&& grid[x+1,y] != gridSpace.player 
					&& grid[x-1,y] != gridSpace.player 
					&& grid[x+1,y-1] != gridSpace.player 
					&& grid[x-1,y+1] != gridSpace.player)
					{
						if(StatsDisplayManager.levelCounter>=6 && StatsDisplayManager.levelCounter<=9){
							Spawn(x,y,enemy2Obj);
							placed = true;
						}else if(StatsDisplayManager.levelCounter>=11 && StatsDisplayManager.levelCounter<=14){
							Spawn(x,y,enemy3Obj);
							placed = true;
						}else{
							Spawn(x,y,enemyObj);
							placed = true;
						}
						if (placed){
							yield return new WaitForSeconds(timeBetweenLoops/2);
						}
					}
					else continue;
					tempCount++;
				}
			}	
		}
		enemyCount = tempCount;
		StartCoroutine(CreateAmmo());
	}

	//spawn ammo. memiliki kemiripan dengan spawn health kit dan food.
	IEnumerator CreateAmmo(){
		//mengecek grid
		for (int x=0; x < roomWidth; x++){
			for (int y=0; y < roomHeight; y++){
				//mengecek kemungkinan spawn ammo di dalam grid
				if(grid[x,y] == gridSpace.floor && Random.value < chanceToSpawnAmmo){
					bool placed = false;
					grid[x,y] = gridSpace.ammo;
					//mengecek apabila ada pemain terdekat
					if(grid[x+1,y+1] != gridSpace.player 
					&& grid[x-1,y-1] != gridSpace.player 
					&& grid[x,y+1] != gridSpace.player 
					&& grid[x,y-1] != gridSpace.player 
					&& grid[x+1,y] != gridSpace.player 
					&& grid[x-1,y] != gridSpace.player 
					&& grid[x+1,y-1] != gridSpace.player 
					&& grid[x-1,y+1] != gridSpace.player)
					{
					Spawn(x,y,ammoObj);
					placed = true;
					if (placed){
							yield return new WaitForSeconds(timeBetweenLoops/2);
					}
					}
					else continue;
				}
			}	
		}StartCoroutine(CreateHealthKit());
	}
	IEnumerator CreateHealthKit(){
		for (int x=0; x < roomWidth; x++){
			for (int y=0; y < roomHeight; y++){
				if(grid[x,y] == gridSpace.floor && Random.value < chanceToSpawnHealthKit){
					bool placed = false;
					grid[x,y] = gridSpace.healthKit;
					if(grid[x+1,y+1] != gridSpace.player 
					&& grid[x-1,y-1] != gridSpace.player 
					&& grid[x,y+1] != gridSpace.player 
					&& grid[x,y-1] != gridSpace.player 
					&& grid[x+1,y] != gridSpace.player 
					&& grid[x-1,y] != gridSpace.player 
					&& grid[x+1,y-1] != gridSpace.player 
					&& grid[x-1,y+1] != gridSpace.player)
					{
						Spawn(x,y,healthKitObj);
						placed = true;
					if (placed){
						yield return new WaitForSeconds(timeBetweenLoops/2);
					}
					}else continue;
				}
			}	
		}StartCoroutine(CreateFood());
	}
	IEnumerator CreateFood(){
		for (int x=0; x < roomWidth; x++){
			for (int y=0; y < roomHeight; y++){
				if(grid[x,y] == gridSpace.floor && Random.value < chanceToSpawnFood){
					bool placed = false;
					grid[x,y] = gridSpace.food;
					if(grid[x+1,y+1] != gridSpace.player 
					&& grid[x-1,y-1] != gridSpace.player 
					&& grid[x,y+1] != gridSpace.player 
					&& grid[x,y-1] != gridSpace.player 
					&& grid[x+1,y] != gridSpace.player 
					&& grid[x-1,y] != gridSpace.player 
					&& grid[x+1,y-1] != gridSpace.player 
					&& grid[x-1,y+1] != gridSpace.player)
					{
					Spawn(x,y,foodObj);
					placed = true;
					if (placed){
						yield return new WaitForSeconds(timeBetweenLoops/2);
					}
					} else continue;
				}
			}	
		}
	}
	public IEnumerator SpawnPlayer(){
		//toggle untuk spawn player
		playerCamPosContainer = initialPlayerSpawn;
		playerCamPosContainer.z=-.3f;
		//mengecek keseluruhan grid size
		for (int x=0; x < roomWidth; x++){
			for (int y=0; y < roomHeight; y++){
				initialPlayerSpawn = new Vector2(x,y) * worldUnitsInOneGridCell - roomSizeWorldUnits / 2.0f;
				if(isPlayerSpawned== false && grid[x,y]==gridSpace.floor) {
					grid[x,y] = gridSpace.player;
					playerObj = Instantiate(playerObj,initialPlayerSpawn,Quaternion.identity);
					isPlayerSpawned=true;
					//mengatur kamera agar tidak memulai dari tengah
					//Camera.main.transform.position= new Vector3(initialPlayerSpawn.x,initialPlayerSpawn.y,playerCamPosContainer.z);
					yield return new WaitForSeconds(timeBetweenLoopsLong);
				}
			}	
		}
		StartCoroutine(CreateEnemy());
	}

	//spawn objek dinding dan lantai dari hasil walker yang telah dibuat
	IEnumerator SpawnLevel(){
		for (int x = 0; x < roomWidth; x++){
			for (int y = 0; y < roomHeight; y++){
                bool placed = false;
				switch(grid[x,y]){
					case gridSpace.empty:
						break;
					case gridSpace.floor:
						if(StatsDisplayManager.levelCounter>=6 && StatsDisplayManager.levelCounter<=9){
							Spawn(x,y,floorObj2);
                            placed = true;
						}
						else if(StatsDisplayManager.levelCounter>=11){
							Spawn(x,y,floorObj3);
                            placed = true;
						}
						else{
                            Spawn(x,y,floorObj);
                            placed = true;
                        } 
                        break;
						
					case gridSpace.wall:
						Spawn(x,y,wallObj);
                        placed = true;
						break;
				}if (placed){
							yield return new WaitForSeconds(timeBetweenLoops/2);
				}
			}
		}
        StartCoroutine(SpawnPlayer());
	}
	//Bagian untuk random walk
	Vector2 RandomDirection(){
		//mengambil nilai random dari 0 sampai 3
		int choice = Mathf.FloorToInt(Random.value * 3.99f);
		//nilai yang diambil dijadikan arah walker bergerak
		switch (choice){
			case 0:
				return Vector2.down;
			case 1:
				return Vector2.left;
			case 2:
				return Vector2.up;
			default:
				return Vector2.right;
		}
	}
	//Menghitung jumlah lantai yang dibuat
	int NumberOfFloors(){
		int count = 0;
		foreach (gridSpace space in grid){
			if (space == gridSpace.floor){
				count++;
			}
		}
		//Debug.Log(count);
		return count;
	}

	//Fungsi untuk spawn objek apapun di dalam grid
	void Spawn(float x, float y, GameObject toSpawn){
		//mencari posisi untuk spawn
		Vector2 offset = roomSizeWorldUnits / 2.0f;
		Vector2 spawnPos = new Vector2(x,y) * worldUnitsInOneGridCell - offset;
		Instantiate(toSpawn, spawnPos, Quaternion.identity);
		
	}
}