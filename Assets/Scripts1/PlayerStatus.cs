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

    // ����� ���� ���� �߰�
    public AudioSource audioSource; // ����� �ҽ�
    public AudioClip realFoodClip; // ��¥ ���̸� ���� �� ����� ����� Ŭ��
    public AudioClip fakeFoodClip; // ��¥ ���̸� ���� �� ����� ����� Ŭ��
    public AudioClip gameOverClip; // ���� ���� �Ҹ� Ŭ��

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateUI();
        StartCoroutine(ReduceSatietyOverTime());
    }

    void UpdateUI()
    {
        // ��Ʈ UI ������Ʈ
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

            // ��¥ ���� ����� Ŭ�� ���
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

            // ��¥ ���� ����� Ŭ�� ���
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
        // ���� �ܰ� ����
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
        // ���� ���� ó��
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }

    public void GameClear()
    {
        // ���� Ŭ���� ó��
        gameClearImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        // ���� �����
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹 ó��
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