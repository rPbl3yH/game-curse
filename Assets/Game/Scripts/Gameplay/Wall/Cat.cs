using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private Transform catModelTransform;

        [SerializeField] private float _speed = 2f;

        [SerializeField, Range(0, 1)] private float waggingLeftRightPosition = 0.5f;
        [SerializeField, Range(0, 180)] private float waggingLeftRightRotation = 10f;
        [SerializeField, Range(0, 10)] private float waggingSpeed = 1f;
        [SerializeField, Range(0, 1)] private float waggingSpeedRandomize = 0.5f;

        private bool _isActivated;
        private Vector3 _targetPosition;
        private Action _action;

        private Vector3 waggingVector;
        private float waggingPos = 0f;
        private float lastWagging = 0f;
        private float WaggingSpeedRandomShift = 1;
        private float waggingRotationSmooth = 50f; // ��������������������������������������

        private Vector3 modelCenterPosShift;

        private void Start()
        {
            waggingVector = transform.right;

            // �������� ����� 0 ��� PI (����� ��� ������) ��� ��������� ��������
            waggingPos = Mathf.Round(UnityEngine.Random.Range(0, 1)) * Mathf.PI;

            if (catModelTransform.GetComponent<Renderer>().IsUnityNull())
                Debug.LogWarning("������� �� �� ������ catModelTransform ������� ������������� ��������");
            // ���� �������� ������ 3� ������ �� ������ �������
            modelCenterPosShift = catModelTransform.position -
                (!catModelTransform.GetComponent<Renderer>().IsUnityNull() ?
                catModelTransform.GetComponent<Renderer>().bounds.center :
                Vector3.zero);

        }

        public void MoveToPosition(Vector3 position, Action onComplete)
        {
            _targetPosition = position;
            _isActivated = true;
            _action = onComplete;
        }

        private void Update()
        {
            if (!_isActivated)
            {
                return;
            }

            var distance = Vector3.Distance(transform.position, _targetPosition);
            if (distance < Mathf.Epsilon)
            {
                _action?.Invoke();
                Destroy(gameObject);
                return;
            }

            var forward = (_targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            transform.position = Vector3.MoveTowards(
                transform.position, _targetPosition, _speed * Time.deltaTime);


            float sinWagging = Mathf.Sin(waggingPos) * waggingLeftRightPosition;

            float wagging = sinWagging - lastWagging;

            lastWagging = sinWagging;

            catModelTransform.position += waggingVector * wagging;
            catModelTransform.RotateAround(
                catModelTransform.position + modelCenterPosShift,
                new Vector3(0, -Mathf.Sin(waggingPos) * waggingLeftRightRotation, 0),
                waggingRotationSmooth * Time.deltaTime
                );

            // ����������� ������� � ������ �������
            waggingPos += Time.deltaTime * waggingSpeed * WaggingSpeedRandomShift;

        }

        private void FixedUpdate()
        {
            // ������ ��� � ������ ������ ��� ��������� �������
            WaggingSpeedRandomShift = (1 + UnityEngine.Random.Range(-waggingSpeedRandomize, waggingSpeedRandomize));
        }
    }
}