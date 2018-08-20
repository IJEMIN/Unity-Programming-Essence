using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtil {
    // 네브 메시 위의 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 maxDistance 반경 안에서 랜덤한 위치를 찾는다

    public static Vector3 GetRandomPosition(Vector3 center, float maxDistance) {
        // center를 중심으로 반지름이 maxDinstance인 구 안에서의 랜덤한 위치 하나를 저장한다
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // 네브 메시 샘플링의 정보를 저장하는 변수

        // randomPos를 기준으로 maxDistance 반경 안에서, randomPos에 가장 가까운 네브 메시 위의 한 점을 찾는다
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position; // 찾은 점을 반환
    }
}