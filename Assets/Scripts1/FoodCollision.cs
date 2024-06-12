using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    public AudioClip eatSound; // ���̸� ���� �� ����� ����� Ŭ��
    private AudioSource audioSource; // ���� ����� �ҽ�

    void Start()
    {
        // ���� ���� ������Ʈ�� �ִ� AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹���� �� ����� ���
            audioSource.PlayOneShot(eatSound);
            // ���� ������Ʈ �ı�
            Destroy(gameObject);
        }
    }
}
