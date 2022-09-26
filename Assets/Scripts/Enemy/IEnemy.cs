using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Move();
    void Attack();
    void CheckRange(Vector3 center, float radius);
    void GetHit(float damage);
}
