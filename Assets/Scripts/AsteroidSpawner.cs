using UnityEngine;

public class AsteroidSpawner : MonoBehaviour{
    [SerializeField]
    private GameObject[] asteroidPrefabs;

    [SerializeField]
    private float secondsBetweenAsteroids = 0.5f;

    [SerializeField]
    private Vector2 forceRange;

    private float _timer;
    private Camera mainCamera;

    private void Start(){
        mainCamera = Camera.main;
    }

    private void Update(){
        _timer -= Time.deltaTime;
        if (_timer <= 0){
            SpawnAsteroid();
            _timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid(){
        var side = Random.Range(0, 4);
        var spawnPoint = Vector2.zero;
        var direction = Vector2.zero;

        switch (side){
            case 0:
                //left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1f;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        var worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;
        var selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        var asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint,
            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        var rigidBody = asteroidInstance.GetComponent<Rigidbody>();
        rigidBody.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}