using UnityEngine;

public class FoodController : MonoBehaviour
{
    public float moveSpeed = 3f; // ������ �̵� �ӵ�
    public bool isRealFood; // ��¥ �������� ����

    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �÷��̾��� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �浹�� ������Ʈ�κ��� PlayerStatus ������Ʈ ��������
            PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
            // PlayerStatus ������Ʈ�� �ִ� ���
            if (playerStatus != null)
            {
                // �÷��̾� ���¸� ������Ʈ�ϴ� EatFood �Լ� ȣ��
                playerStatus.EatFood(isRealFood);
            }
            // ���� ������Ʈ �ı�
            Destroy(gameObject);
        }
    }
}
