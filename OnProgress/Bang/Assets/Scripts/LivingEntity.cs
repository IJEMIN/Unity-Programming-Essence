using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 게임상에서 "살아있고" "공격 당할수 있는"
// 모든 오브젝트들을 위한 기반 설계를 제공
public class LivingEntity : MonoBehaviour, IDamageable
{

    public float startingHealth = 100f; // 시작 체력

    // 외부에서 체력 값을 가져올순있지만, 덮어쓸순없다
    // 체력값이 어떻게 결정되냐는, LivingEntity 내부에서만 결정할수있다
    public float health { get; protected set; }

    protected bool dead; //죽었나 살았나

    // 델리게이트 : (외부) 함수를 적재해놓고, 원하는 시점에 실행시킬수 있다
    public event Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    // 데미지를 입었을때 실행될 처리
    // 자식 클래스가 덮어쓰기(오버라이드) 하여 확장 가능
    public virtual void OnDamage(float damage, Vector3 hitPoint,
        Vector3 hitDirection)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die(); // 실제 죽는처리 실행
        }
    }

    // 죽었을때 실행될 처리, virutal이기 때문에 확장클래스가 확장가능
    public virtual void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath(); // 죽었을때 실행할 연쇄 처리들을 
        }
        Destroy(gameObject,10f); // 스스로를 파괴
    }
}