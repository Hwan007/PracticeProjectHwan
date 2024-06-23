using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public struct VelocityPair {
    public float vel;
    public float deltaTime;
    public VelocityPair(float vel, float deltaTime) {
        this.vel = vel;
        this.deltaTime = deltaTime;
    }
}

public class Movement : MonoBehaviour {
    protected float gravity = 0.5f;
    protected float mass = 1;
    protected Vector2 originPosition;
    protected Rigidbody2D rb2D;
    protected CharacterStat stat;
    [SerializeField] protected float sqrMaxVelocity;
    protected virtual void Awake() {
        rb2D = rb2D != null ? rb2D : gameObject.GetComponent<Rigidbody2D>();
    }
    public virtual void Initialize(CharacterStat stat) {
        this.stat = stat;
        ChangeMass(stat.InstanceStat.totalStat.GetStat(Define.EStatType.JumpMass));
    }
    public void SetOriginPoint(Vector2 point) {
        originPosition = point;
        rb2D.MovePosition(originPosition);
    }
    public void ApplyImpact(int force) {
        rb2D.AddForce(new Vector2(0, force));
    }
    public void ApplyImpact(float force) {
        rb2D.AddForce(new Vector2(0, force));
    }
    public void ChangeDownForce(float acceleration) {
        gravity += acceleration;
        rb2D.gravityScale = gravity;
    }
    public void ChangeMass(float mass) {
        this.mass = mass;
        rb2D.mass = this.mass;
    }
    private void FixedUpdate() {
        if (rb2D.velocity.sqrMagnitude > sqrMaxVelocity)
            rb2D.velocity *= 0.99f;
    }
    public virtual void Update() {
        if (rb2D.velocity.sqrMagnitude > sqrMaxVelocity)
            rb2D.velocity *= 0.99f;
    }

    public virtual bool DoJump() {
        return false;
    }
}
