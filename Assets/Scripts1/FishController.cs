using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FishController : MonoBehaviour
{
    public float jumpForce = 5f; // ���� ��
    private Rigidbody2D rb; // ���� �ý��� Rigidbody2D
    public GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ
    public AudioSource audioSource; // ����� �ҽ�
    public AudioClip jumpClip; // ���� �Ҹ� Ŭ��
    public AudioClip gameOverClip; // ���� ���� �Ҹ� Ŭ��
    public AudioClip realFoodClip; // ��¥ ���� �Ҹ� Ŭ��
    public AudioClip fakeFoodClip; // ��¥ ���� �Ҹ� Ŭ��

    private bool isGameOver = false; // ���� ���� ���θ� ��Ÿ���� �÷���

    IEnumerator RestartGame()
    {
        // ������� ���� �ڷ�ƾ
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        // ������ �� Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ������ ������� �ʾҰ�, �����̽��ٰ� ������ ��
        if (!isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // ���� ����
        }
    }

    void Jump()
    {
        // ���� �Լ�
        // Rigidbody2D�� �ӵ��� �����Ͽ� ���� ȿ��
        rb.velocity = Vector2.up * jumpForce; 
        
        // ���� �Ҹ� ���
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameOver(); // �ٴڿ� ������ ���� ����
        }
        else if (collision.gameObject.CompareTag("Ceiling"))
        {
            // õ�忡 ������ �ӵ��� 0���� �����Ͽ� ���� ���ϰ� ��
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            // ��¥ ���̿� �浹�� ��� ó��
            HandleFoodCollision(true);
            Destroy(collision.gameObject); // ���� ������Ʈ �ı�
        }
        else if (collision.gameObject.CompareTag("FakeFood"))
        {
            // ��¥ ���̿� �浹�� ��� ó��
            HandleFoodCollision(false);
            Destroy(collision.gameObject); // ���� ������Ʈ �ı�
        }
    }

    void HandleFoodCollision(bool isRealFood)
    {
        // �÷��̾��� ���¸� ó���ϴ� �Լ� ȣ��
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();
        if (playerStatus != null)
        {
            playerStatus.EatFood(isRealFood);
        }

        // ���̿� ���� �ٸ� �Ҹ� ���
        if (audioSource != null)
        {
            if (isRealFood && realFoodClip != null)
            {
                audioSource.PlayOneShot(realFoodClip); // ��¥ ������ ��
            }
            else if (!isRealFood && fakeFoodClip != null)
            {
                audioSource.PlayOneShot(fakeFoodClip); // ��¥ ������ ��
            }
        }
    }

    void GameOver()
    {
        // ���� ���� ���� ����
        isGameOver = true;

        // ���� ���� �Ҹ� ���
        if (audioSource != null && gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }

        // ���� ���� �̹��� Ȱ��ȭ �� ����� �ڷ�ƾ ����
        gameOverImage.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(RestartGame());
    }
}
