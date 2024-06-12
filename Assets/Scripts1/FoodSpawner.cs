using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject realFoodPrefab; // 진짜 먹이 프리팹
    public GameObject fakeFoodPrefab; // 가짜 먹이 프리팹
    public float spawnInterval = 2f; // 먹이 생성 간격
    public float spawnPositionX = 10f; // 먹이 생성 위치 (오른쪽 끝)
    public float spawnRangeY = 5f; // 먹이 생성 위치 Y 범위

    void Start()
    {
        InvokeRepeating("SpawnFood", spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPosY);

        // 랜덤으로 진짜 먹이 또는 가짜 먹이 생성
        GameObject foodPrefab = (Random.value > 0.5f) ? realFoodPrefab : fakeFoodPrefab;
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}
