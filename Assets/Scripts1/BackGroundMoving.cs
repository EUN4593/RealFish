using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMoving : MonoBehaviour

{   //����� �̵��ϴ� �ӵ�
    private float moveSpeed = 2f;

    // ����� õõ�� ���������� �̵�
    void Update()
    {
        transform.position += Vector3.left 
            * moveSpeed * Time.deltaTime;

        /*����� �̵��ϴٰ� ���� ��ǥ�� �Ǹ� �ٽ�
        �ڷ� �̵��Ͽ� ����� ��� �̾������� ��.*/
        if (transform.position.x < -29)
        {
            transform.position += new Vector3(60, 0, 0);
        }
    }
}
