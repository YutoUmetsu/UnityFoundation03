using UnityEngine;
// 新しいInput Systemの標準APIを使用するために追加
using UnityEngine.InputSystem;

namespace VoxelCharacter
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 1.0f, 0);
        [SerializeField] private float sensitivity = 3.0f;
        [SerializeField] private float smoothTime = 0.1f;

        private float distanceFromTarget;
        private float rotationX = 160.0f;

        private Vector3 currentRotation = Vector3.zero;
        private Vector3 smoothVelocity = Vector3.zero;


        private void Awake()
        {
            distanceFromTarget = Vector3.Distance(this.transform.position, target.transform.position);

            currentRotation = transform.position + Vector3.up * rotationX;
            Rotate();
        }

        private void Update()
        {
            // 現在接続されているマウスが存在するかチェック
            if (Mouse.current == null) return;

            // 左クリックまたは右クリックが押されているか判定
            if (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            {
                // マウスの横移動量（Delta）を取得
                float mouseX = Mouse.current.delta.x.ReadValue() * (sensitivity * 0.1f);
                rotationX += mouseX;
            }

            currentRotation = Vector3.SmoothDamp(currentRotation, transform.position + Vector3.up * rotationX, ref smoothVelocity, smoothTime);
            Rotate();
        }


        private void Rotate()
        {
            transform.localEulerAngles = currentRotation;
            transform.position = (target.position + offset) - transform.forward * distanceFromTarget;
        }

    }
}
