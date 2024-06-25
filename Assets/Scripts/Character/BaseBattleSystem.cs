using UnityEngine;
namespace Character {
    public abstract class BaseBattleSystem : MonoBehaviour {
        [SerializeField] protected LayerMask targetLayer;
        [SerializeField] protected AttackCollider attackCollider;
        protected virtual void SubscribeOnCollider() {
            if (attackCollider != null) {
                attackCollider.OnCollider += OnAttackCollider;
            }
        }
        protected virtual void UnsubscribeOnCollider() {
            if (attackCollider != null) {
                attackCollider.OnCollider -= OnAttackCollider;
            }
        }
        protected static bool CheckLayer(int layerValue, LayerMask target) {
            return (1 << layerValue) == (1 << layerValue & target.value);
        }
        protected abstract void OnAttackCollider(Collider2D collider);
    }
}