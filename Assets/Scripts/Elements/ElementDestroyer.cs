using UnityEngine;

namespace Elements
{
    public class ElementDestroyer : MonoBehaviour
    {
        private GameObject _parentOfElement;

        public void SetupParent(GameObject parentOfElement) => _parentOfElement = parentOfElement;
        
        public void DestroyElement() => Destroy(_parentOfElement);
    }
}