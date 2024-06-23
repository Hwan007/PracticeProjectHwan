using System;
using UnityEngine;

public class AttackCollider : MonoBehaviour {
    public event Action<Collider2D> OnCollider;
    Collider2D col;

    private void OnTriggerEnter2D(Collider2D collision) {
        OnCollider?.Invoke(collision);
    }
}