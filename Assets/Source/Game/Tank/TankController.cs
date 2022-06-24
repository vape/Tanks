using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Game.Tank
{
    public class TankController : MonoBehaviour
    {
        public Vector2 CurrentSpeed => currentMove * speed;

        [SerializeField]
        private Rigidbody body;

        [SerializeField]
        private float speed;
        [SerializeField]
        private float angularSpeed;
        [SerializeField]
        private float rotationSmoothTime = 0.1f;
        [SerializeField]
        private float accelerationSmoothTime = 0.1f;

        private Vector2 moveTarget;
        private Vector2 currentMove;
        private Vector2 moveChangingVelocity;

        public void OnMovement(InputAction.CallbackContext context)
        {
            moveTarget = context.action.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            currentMove.y = Mathf.SmoothDamp(currentMove.y, moveTarget.y, ref moveChangingVelocity.y, accelerationSmoothTime);
            currentMove.x = Mathf.SmoothDamp(currentMove.x, moveTarget.x, ref moveChangingVelocity.x, rotationSmoothTime);

            var move = transform.forward * currentMove.y * Time.fixedDeltaTime * speed;
            var rotation = Quaternion.Euler(0, currentMove.x * angularSpeed * Time.fixedDeltaTime, 0);

            body.MovePosition(transform.position + move);
            body.MoveRotation(transform.rotation * rotation);
        }
    }
}