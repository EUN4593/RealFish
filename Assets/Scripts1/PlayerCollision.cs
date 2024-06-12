using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerStatus playerStatus; // �÷��̾� ���¸� ������ ����

    void Start()
    {
        // ���� ������Ʈ�� ����� PlayerStatus ��ũ��Ʈ ��������
        playerStatus = GetComponent<PlayerStatus>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� Food �±׸� ������ �ִ� ���
        if (other.gameObject.CompareTag("Food"))
        {
            // �浹�� ���̰� ��¥ �������� ��¥ �������� Ȯ��
            bool isRealFood = other.GetComponent<Food>().isReal;
            // �÷��̾� ���� ������Ʈ
            playerStatus.EatFood(isRealFood);
            // �浹�� ���� �ı�
            Destroy(other.gameObject);
        }
        // �浹�� ������Ʈ�� FakeFood �±׸� ������ �ִ� ���
        else if (other.gameObject.CompareTag("FakeFood"))
        {
            // �÷��̾� ���� ������Ʈ (��¥ ����)
            playerStatus.EatFood(false);
            // �浹�� ��¥ ���� �ı�
            Destroy(other.gameObject);
        }
    }
}
