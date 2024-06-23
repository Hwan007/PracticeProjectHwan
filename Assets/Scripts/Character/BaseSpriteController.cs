﻿using UnityEngine;

public class BaseSpriteController : MonoBehaviour {

    [field: SerializeField] public Animator[] animator { get; protected set; }
    [SerializeField] protected SpriteRenderer[] spriteRenderer;
    [field: SerializeField] public AnimationData[] aniData { get; protected set; }

    public virtual void Initialize(PlayerContainer container) {
        foreach (var data in aniData)
            data.InitializeAnimatorHash();
    }
}

