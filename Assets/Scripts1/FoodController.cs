using UnityEngine;

public class FoodController : MonoBehaviour
{
    public float moveSpeed = 3f; // 먹이의 이동 속도
    public bool isRealFood; // 진짜 먹이인지 여부

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 플레이어인 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌한 오브젝트로부터 PlayerStatus 컴포넌트 가져오기
            PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
            // PlayerStatus 컴포넌트가 있는 경우
            if (playerStatus != null)
            {
                // 플레이어 상태를 업데이트하는 EatFood 함수 호출
                playerStatus.EatFood(isRealFood);
            }
            // 먹이 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
