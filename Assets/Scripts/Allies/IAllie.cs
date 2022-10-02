using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAllie
{
    void Move(Vector3 position);
    void Attack();
    void CheckRange(Vector3 center, float radius);
    void GetHit(float damage);
    float GetHealth();
}
