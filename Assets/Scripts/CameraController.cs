using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public class CameraController : MonoBehaviour {
        public static CameraController[] cameras = new CameraController[3];

        private Transform followerTr;
        private Vector3 dampVel;
        private Camera cameraComp;

        [SerializeField][Range(0, 2)] private int cameraIndex;
        [Header("Camera Follow Setting")]
        [SerializeField] private float dampingTime;
        [SerializeField] private Vector2 offset;

        [Header("Shake Effect Setting")]
        [SerializeField] private float shakeDuration = 0.01f;
        [SerializeField] private float shakeMagnitude = 0.03f;
        public float shakeTime = 0.8f;

        [Header("Camera Limit Position")]
        [SerializeField] private bool borderFlag = false;
        [SerializeField] private Vector2 xLimit;
        [SerializeField] private Vector2 yLimit;
        [SerializeField] private Vector2 zLimit;

        private void Awake() {
            cameras[cameraIndex] = this;
            cameraComp = gameObject.GetComponent<Camera>();
        }

        private void OnDestroy() {
            cameras[cameraIndex] = null;
        }

        public void SetFollow(GameObject obj) {
            followerTr = obj.transform;
        }

        public static void SetFollow(int cameraIndex, GameObject obj) {
            if (cameras[cameraIndex] != null)
                cameras[cameraIndex].SetFollow(obj);
        }

        public static void SetBorder(int cameraIndex, Vector2 xRange, Vector2 yRange, Vector2 zRange) {
            cameras[cameraIndex].xLimit = xRange;
            cameras[cameraIndex].yLimit = yRange;
            cameras[cameraIndex].zLimit = xRange;
            cameras[cameraIndex].borderFlag = true;
        }

        public static void DeleteBorder(int cameraIndex) {
            cameras[cameraIndex].borderFlag = false;
        }

        public static void Shake(int cameraIndex, float duration = .0f, float magnitude = .0f) {
            if (duration <= 0)
                duration = cameras[cameraIndex].shakeDuration;
            if (magnitude <= 0)
                magnitude = cameras[cameraIndex].shakeMagnitude;
            cameras[cameraIndex].StartCoroutine(cameras[cameraIndex].StartShake(duration, magnitude));
        }

        private IEnumerator StartShake(float duration, float magnitude) {
            Vector3 origin = Vector3.zero;
            float passed = .0f;
            while (duration > passed) {
                passed += Time.deltaTime;
                cameraComp.transform.localPosition = origin + Vector3.down * (Mathf.Sin(passed) * magnitude / passed);
                yield return null;
            }

            cameraComp.transform.localPosition = origin;
        }

        private IEnumerator StartShake(float frequency, float ksai, float r, float initialPoint, float duration) {
            float k1 = ksai / Mathf.PI / frequency;
            float k2 = 1 / Mathf.Pow(2 * Mathf.PI * frequency, 2);
            float k3 = r * ksai / (2 * Mathf.PI * frequency);

            float x = initialPoint;
            float y = initialPoint;
            float dy = .0f;

            float passedTime = .0f;
            while (passedTime < duration) {
                passedTime += Time.deltaTime;

                var target = transform;
                var targetLocal = target.localPosition;

                float dx = (targetLocal.y - x) / Time.deltaTime;
                x = targetLocal.y;

                y += Time.deltaTime * dy;
                dy = dy * Time.deltaTime * (x + k3 * dx - y - k1 * dy) / k2;

                targetLocal.y = y;
                target.localPosition = targetLocal;
                yield return null;
            }
        }

        void Update() {
            if (!ReferenceEquals(followerTr, null)) {
                var position = transform.position;
                var target = Vector3.SmoothDamp(position, followerTr.position + new Vector3(offset.x, offset.y, position.z),
                    ref dampVel, dampingTime);

                if (borderFlag) {
                    target.x = Mathf.Clamp(target.x, xLimit.x, xLimit.y);
                    target.y = Mathf.Clamp(target.y, yLimit.x, yLimit.y);
                    target.z = Mathf.Clamp(target.z, zLimit.x, zLimit.y);
                }

                transform.position = target;
            }
        }
    }
}