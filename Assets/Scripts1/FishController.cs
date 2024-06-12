using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FishController : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 힘
    private Rigidbody2D rb; // 물리 시스템 Rigidbody2D
    public GameObject gameOverImage; // 게임 오버 이미지 오브젝트
    public AudioSource audioSource; // 오디오 소스
    public AudioClip jumpClip; // 점프 소리 클립
    public AudioClip gameOverClip; // 게임 오버 소리 클립
    public AudioClip realFoodClip; // 진짜 먹이 소리 클립
    public AudioClip fakeFoodClip; // 가짜 먹이 소리 클립

    private bool isGameOver = false; // 게임 오버 여부를 나타내는 플래그

    IEnumerator RestartGame()
    {
        // 재시작을 위한 코루틴
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        // 시작할 때 Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 게임이 종료되지 않았고, 스페이스바가 눌렸을 때
        if (!isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // 점프 실행
        }
    }

    void Jump()
    {
        // 점프 함수
        // Rigidbody2D의 속도를 설정하여 점프 효과
        rb.velocity = Vector2.up * jumpForce; 
        
        // 점프 소리 재생
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 감지
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameOver(); // 바닥에 닿으면 게임 오버
        }
        else if (collision.gameObject.CompareTag("Ceiling"))
        {
            // 천장에 닿으면 속도를 0으로 설정하여 뚫지 못하게 함
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            // 진짜 먹이에 충돌한 경우 처리
            HandleFoodCollision(true);
            Destroy(collision.gameObject); // 먹이 오브젝트 파괴
        }
        else if (collision.gameObject.CompareTag("FakeFood"))
        {
            // 가짜 먹이에 충돌한 경우 처리
            HandleFoodCollision(false);
            Destroy(collision.gameObject); // 먹이 오브젝트 파괴
        }
    }

    void HandleFoodCollision(bool isRealFood)
    {
        // 플레이어의 상태를 처리하는 함수 호출
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.EatFood(isRealFood);
        }

        // 먹이에 따라 다른 소리 재생
        if (audioSource != null)
        {
            if (isRealFood && realFoodClip != null)
            {
                audioSource.PlayOneShot(realFoodClip); // 진짜 먹이일 때
            }
            else if (!isRealFood && fakeFoodClip != null)
            {
                audioSource.PlayOneShot(fakeFoodClip); // 가짜 먹이일 때
            }
        }
    }

    void GameOver()
    {
        // 게임 오버 상태 설정
        isGameOver = true;

        // 게임 오버 소리 재생
        if (audioSource != null && gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }

        // 게임 오버 이미지 활성화 및 재시작 코루틴 시작
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }
}
