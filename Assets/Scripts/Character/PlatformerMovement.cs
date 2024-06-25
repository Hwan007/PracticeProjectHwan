using UnityEngine;
using Character;
using System;

namespace Character2D {
    public interface IMovable {
        public float GetMass();
        public float GetGravity();
        public float GetPower();
        public float GetLimitVelocity();
    }
    /// <summary>
    /// Character 2D Physics movement. Initialize is needed.
    /// </summary>
    public class PlatformerMovement : MonoBehaviour {
        protected Rigidbody2D rb2D;
        protected Collider2D col2D;
        protected IMovable target;

        [Header("Rigidbody2D (Body type = Kinematic)")]
        [Header("Limit about moving")][SerializeField] protected float sqrMaxVelocity;
        [SerializeField] protected LayerMask ground;
        //[SerializeField] protected LayerMask obstacle;

        protected float mass = 1;
        protected Vector2 gravity = new Vector2(0, 0.5f);
        protected Vector2 outForce;
        protected bool isGround;
        //protected Vector2 prePos;
        /// <summary>
        /// Need invoke this before use this component
        /// </summary>
        /// <param name="moveObj">Object that want to move</param>
        public virtual void Initialize(IMovable moveObj) {
            if (!gameObject.TryGetComponent<Rigidbody2D>(out rb2D)) {
                rb2D = gameObject.AddComponent<Rigidbody2D>();
                //rb2D.bodyType = RigidbodyType2D.Kinematic;
                //rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                //rb2D.useFullKinematicContacts = true;
            }

            if (!gameObject.TryGetComponent<Collider2D>(out col2D)) {
                col2D = gameObject.AddComponent<BoxCollider2D>();
            }
            //col2D.contactCaptureLayers = -1;
            //col2D.callbackLayers = obstacle & ground & (1 << gameObject.layer);
            ChangeMass(target.GetMass());
        }
        public virtual void ApplyImpact(Vector2 force) {
            outForce += force / mass;
        }
        public virtual void ApplyImpact(int force) {
            ApplyImpact(new Vector2(0, force));
        }
        public virtual void ApplyImpact(float force) {
            ApplyImpact(new Vector2(0, force));
        }
        public virtual void ChangeMass(float mass) {
            if (mass > 0.00001f) {
                this.mass = mass;
            }
            else
                this.mass = 0.00001f;
        }
        public virtual void ChangeDownForce(float gravity) {
            this.gravity = new Vector2(0, gravity);
        }
        protected virtual void Update() {
            Vector2 toVel = rb2D.velocity;
            if (!isGround)
                toVel += Time.deltaTime * gravity;
            if (outForce != Vector2.zero) {
                toVel += outForce;
                outForce = Vector2.zero;
            }
            if (toVel.sqrMagnitude > sqrMaxVelocity)
                toVel *= 0.99f;

            rb2D.velocity = toVel;
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision) {
#if UNITY_EDITOR
            foreach (var contact in collision.contacts) {
                Debug.Log(gameObject.name + " : " + contact.collider.name);
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
#endif
            // TODO check layer and control vel
        }
    }
}