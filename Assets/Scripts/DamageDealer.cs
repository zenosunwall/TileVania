﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 1f;

    public float GetDamage()
    {
        return damage;
    }
}
