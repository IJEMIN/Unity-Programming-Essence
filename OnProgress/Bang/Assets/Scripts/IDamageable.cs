using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection);
}