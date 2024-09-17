using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealthPoint;
    public int maximumHealthPoint;

    public void isHit (int damage)
    {
        currentHealthPoint -= damage;

        if(currentHealthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
