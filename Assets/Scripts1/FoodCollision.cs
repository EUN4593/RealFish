using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    public AudioClip eatSound; // 먹이를 먹을 때 재생될 오디오 클립
    private AudioSource audioSource; // 먹이 오디오 소스

    void Start()
    {
        // 먹이 게임 오브젝트에 있는 AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌했을 때 오디오 재생
            audioSource.PlayOneShot(eatSound);
            // 먹이 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
