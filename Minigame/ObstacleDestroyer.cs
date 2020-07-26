﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour {

    [SerializeField] private float lifetime = 1f;
    private void Start() {
        Destroy(gameObject, lifetime);
    }
}
