using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject realFoodPrefab; // ��¥ ���� ������
    public GameObject fakeFoodPrefab; // ��¥ ���� ������
    public float spawnInterval = 2f; // ���� ���� ����
    public float spawnPositionX = 10f; // ���� ���� ��ġ (������ ��)
    public float spawnRangeY = 5f; // ���� ���� ��ġ Y ����

    void Start()
    {
        InvokeRepeating("SpawnFood", spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPosY);

        // �������� ��¥ ���� �Ǵ� ��¥ ���� ����
        GameObject foodPrefab = (Random.value > 0.5f) ? realFoodPrefab : fakeFoodPrefab;
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}
