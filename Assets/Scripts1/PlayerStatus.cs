using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int level = 1;
    public int foodEaten = 0;
    public int satiety = 20;
    public int maxSatiety = 20;
    public int lives = 5;
    public int maxLives = 5;
    public GameObject[] hearts;
    public Sprite growthStage1Sprite;
    public Sprite growthStage2Sprite;
    public Sprite growthStage3Sprite;
    public GameObject gameOverImage;
    public GameObject gameClearImage;

    // 오디오 관련 변수 추가
    public AudioSource audioSource; // 오디오 소스
    public AudioClip realFoodClip; // 진짜 먹이를 먹을 때 재생할 오디오 클립
    public AudioClip fakeFoodClip; // 가짜 먹이를 먹을 때 재생할 오디오 클립
    public AudioClip gameOverClip; // 게임 오버 소리 클립

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateUI();
        StartCoroutine(ReduceSatietyOverTime());
    }

    void UpdateUI()
    {
        // 하트 UI 업데이트
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }

    public void EatFood(bool isReal)
    {
        if (isReal)
        {
            foodEaten++;
            satiety = Mathf.Min(satiety + 2, maxSatiety);
            if (foodEaten >= 5)
            {
                level++;
                foodEaten = 0;
                if (level == 5 || level == 10)
                {
                    lives = maxLives;
                    ChangeGrowthStage();
                }
                else if (level == 15)
                {
                    GameClear();
                    return;
                }
            }

            // 진짜 먹이 오디오 클립 재생
            if (audioSource != null && realFoodClip != null)
            {
                audioSource.PlayOneShot(realFoodClip);
            }
        }
        else
        {
            lives--;
            if (lives <= 0)
            {
                GameOver();
                audioSource.PlayOneShot(gameOverClip);
                return;
            }

            // 가짜 먹이 오디오 클립 재생
            if (audioSource != null && fakeFoodClip != null)
            {
                audioSource.PlayOneShot(fakeFoodClip);
            }
        }

        UpdateUI();
    }

    IEnumerator ReduceSatietyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            satiety -= 1;
            if (satiety <= 0)
            {
                satiety = 0;
                lives--;
                if (lives <= 0)
                {
                    GameOver();
                    yield break;
                }
            }
            UpdateUI();
        }
    }

    void ChangeGrowthStage()
    {
        // 성장 단계 변경
        if (level >= 10)
        {
            spriteRenderer.sprite = growthStage3Sprite;
        }
        else if (level >= 5)
        {
            spriteRenderer.sprite = growthStage2Sprite;
        }
        else
        {
            spriteRenderer.sprite = growthStage1Sprite;
        }
    }

    public void GameOver()
    {
        // 게임 오버 처리
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }

    public void GameClear()
    {
        // 게임 클리어 처리
        gameClearImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        // 게임 재시작
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌 처리
        if (other.gameObject.CompareTag("Food"))
        {
            bool isRealFood = other.GetComponent<Food>().isReal;
            EatFood(isRealFood);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("FakeFood"))
        {
            EatFood(false);
            Destroy(other.gameObject);
        }
    }
}