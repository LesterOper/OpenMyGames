using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balloons
{
    public class BalloonController : MonoBehaviour
    {
        [SerializeField] private RectTransform balloon;
        private BalloonSpawner _balloonSpawner;
        private RectTransform _balloonParent;
        private Vector2 _target;
        private bool _move = false;
        private float speed;
        private float amplitude;
        private float sinusSpeed;
        
        private void Awake()
        {
            _balloonParent = GetComponent<RectTransform>();
        }

        public void Setup(Vector2 startPos, Vector2 targetLocal, BalloonSpawner balloonSpawner)
        {
            _target = targetLocal;
            _balloonSpawner = balloonSpawner;
            _balloonParent.anchoredPosition = startPos;
            speed = Random.Range(30, 121);
            amplitude = Random.Range(0.3f, 0.8f);
            sinusSpeed = Random.Range(2, 5);
            _move = true;
        }

        private void Update()
        {
            if(!_move) return;
            balloon.anchoredPosition = new Vector2(0, balloon.anchoredPosition.y + amplitude * Mathf.Sin(Time.time *sinusSpeed));
            _balloonParent.anchoredPosition =
                Vector2.MoveTowards(_balloonParent.anchoredPosition, _target, speed * Time.deltaTime);
            if (Vector2.Distance(_balloonParent.anchoredPosition, _target) <= 0.01f)
            {
                _balloonSpawner.BallonCount--;
                Destroy(gameObject);
            }
        }
    }
}