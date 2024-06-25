using UnityEngine;
using Character;

namespace Character2D {
    public class PlayerPlatformerMovement : PlatformerMovement {
        protected Vector2 groundPosition;
        public override void Initialize(IMovable moveObj) {
            if (!gameObject.TryGetComponent<Rigidbody2D>(out rb2D)) {
                rb2D = gameObject.AddComponent<Rigidbody2D>();
            }
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            ChangeMass(target.GetMass());
            ChangeDownForce(target.GetGravity());
        }
        public bool DoJump() {
            if (transform.position.y <= groundPosition.y) {
                ApplyImpact(target.GetPower());
                return true;
            }
            return false;
        }
        public void SetGroundHeight(Vector2 point) {
            groundPosition = point;

        }
        public override void ApplyImpact(float force) {
            rb2D.AddForce(new Vector2(0, force));
        }
        public override void ApplyImpact(int force) {
            rb2D.AddForce(new Vector2(0, force));
        }
        public override void ChangeDownForce(float gravity) {
            base.ChangeDownForce(gravity);
            rb2D.gravityScale = this.gravity.y;
        }
        public override void ChangeMass(float mass) {
            base.ChangeMass(mass);
            rb2D.mass = mass;
        }
    }
}