using UnityEngine;

// 데미지를 입을 수 있는 타입들이 공통적으로 가져야 하는 인터페이스
public interface IDamageable {
    // 데미지를 입을 수 있는 타입들은 IDamageable을 상속하고 OnDamage 메서드를 반드시 구현해야 한다
    // OnDamage 메서드는 입력으로 데미지 크기(damage), 맞은 지점(hitPoint), 맞은 표면의 방향(hitNormal)을 받는다
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}