using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데미지를 입을 수 있는 타입은 IDamageable을 상속해서 만든다
public interface IDamageable {

    void OnDamage (float damage, Vector3 hitPoint, Vector3 hitDirection);
}