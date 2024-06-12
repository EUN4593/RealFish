using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerStatus playerStatus; // 플레이어 상태를 저장할 변수

    void Start()
    {
        // 게임 오브젝트에 연결된 PlayerStatus 스크립트 가져오기
        playerStatus = GetComponent<PlayerStatus>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 Food 태그를 가지고 있는 경우
        if (other.gameObject.CompareTag("Food"))
        {
            // 충돌한 먹이가 진짜 먹이인지 가짜 먹이인지 확인
            bool isRealFood = other.GetComponent<Food>().isReal;
            // 플레이어 상태 업데이트
            playerStatus.EatFood(isRealFood);
            // 충돌한 먹이 파괴
            Destroy(other.gameObject);
        }
        // 충돌한 오브젝트가 FakeFood 태그를 가지고 있는 경우
        else if (other.gameObject.CompareTag("FakeFood"))
        {
            // 플레이어 상태 업데이트 (가짜 먹이)
            playerStatus.EatFood(false);
            // 충돌한 가짜 먹이 파괴
            Destroy(other.gameObject);
        }
    }
}
