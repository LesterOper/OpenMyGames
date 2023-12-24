using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balloons
{
    public class BalloonSpawner : MonoBehaviour
    {
        [SerializeField] private BalloonController balloonPrefab;
        private Canvas _canvas;
        private Rect rect;
        private readonly int maxBalloonCount = 3;
        private int ballonCount = 0;

        public int BallonCount
        {
            get => ballonCount;
            set => ballonCount = value;
        }

        
        private void Start()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rect = rectTransform.rect;
            StartCoroutine(SpawnBalloons());
        }

        private IEnumerator SpawnBalloons()
        {
            while (true)
            {
                if (ballonCount < maxBalloonCount)
                {
                    yield return new WaitForSeconds(Random.Range(1, 10));
                    
                    BalloonController balloon = Instantiate(balloonPrefab, transform);
                    GenerateStartPosition(balloon);
                    ballonCount++;
                }
                yield return new WaitForSeconds(3);
            }
        }

        private void GenerateStartPosition(BalloonController balloonController)
        {
            int rand = Random.Range(0, 2);

            float x = rand == 0 ? -rect.width / 2 - 100f : rect.width/2 +100f;
            float y = Random.Range(-rect.height / 2, rect.height / 2);
            Vector2 startPos = new Vector2(x, y);

            x = rand == 0 ? rect.width / 2 + 100f : -rect.width / 2 - 100f;
            y = Random.Range(-rect.height / 2, rect.height / 2);
            Vector2 target = new Vector2(x, y);
            
            balloonController.Setup(startPos, target, this);
        }
    }
}