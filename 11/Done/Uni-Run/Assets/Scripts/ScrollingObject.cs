using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScrollingObject 컴포넌트는 게임 오브젝트를 왼쪽으로 계속 이동시키는 역할을 한다
public class ScrollingObject : MonoBehaviour
{
    public float speed = 8f; // 이동 속도
    private void Update()
    {
        // 1초에 speed 만큼 지속적으로 왼쪽으로 이동하는 처리가 온다
    }

}