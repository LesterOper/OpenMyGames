using DG.Tweening;
using UnityEngine;

namespace Elements
{
    public class Element : MonoBehaviour
    {
        [SerializeField] private Animator _elementAnimator;
        [SerializeField] private RectTransform rect;
        [SerializeField] private SwipeElementController _swipeElementController;

        public void Initialize(ElementPosition elementPosition) => _swipeElementController.Initialize(elementPosition);

        public void MoveElement(Transform newParent)
        {
            transform.SetParent(newParent);
            rect.DOAnchorPos(new Vector2(0, 0), 0.4f)
                .SetEase(Ease.InSine);
        }

        public void PlayDestroyAnimation()
        {
            _elementAnimator.SetTrigger("Destroy");
            Destroy(gameObject, 1.5f);
        }
    }

    public class ElementPosition
    {
        private int row;
        private int column;

        public ElementPosition(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int Row
        {
            get => row;
            set => row = value;
        }

        public int Column
        {
            get => column;
            set => column = value;
        }

        public bool Equals(ElementPosition obj)
        {
            return row == obj.row && column == obj.column;
        }
    }

    public enum ElementType
    {
        NONE = 0,
        FIRE = 1,
        WATER = 2,
    }
}