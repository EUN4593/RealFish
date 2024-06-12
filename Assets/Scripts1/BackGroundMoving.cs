using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMoving : MonoBehaviour

{   //배경이 이동하는 속도
    private float moveSpeed = 2f;

    // 배경이 천천히 오른쪽으로 이동
    void Update()
    {
        transform.position += Vector3.left 
            * moveSpeed * Time.deltaTime;

        /*배경이 이동하다가 일정 좌표가 되면 다시
        뒤로 이동하여 배경이 계속 이어지도록 함.*/
        if (transform.position.x < -29)
        {
            transform.position += new Vector3(60, 0, 0);
        }
    }
}
