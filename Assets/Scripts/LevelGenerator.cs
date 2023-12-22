using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Utils;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<SlotController> slots;
        private Level _level;

        private void OnEnable()
        {
            EventsInvoker.StartListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
        }

        private void OnDisable()
        {
            EventsInvoker.StopListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
        }

        public void Generate()
        {
            _level = new Level();
            slots = _level.GenerateLevel(slots);
        }

        private void Normalize()
        {
            var info = _level.NormalizeLevel();
            MoveElementsAfterNormalize(info);
        }

        private void MoveElementsAfterNormalize(List<InfoOfElementMoveAfterNormalize> infoOfElementMoveAfterNormalizes)
        {
            if (infoOfElementMoveAfterNormalizes.Count <= 0) return;
            foreach (var info in infoOfElementMoveAfterNormalizes)
            {
                SlotController needToMove =
                    slots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(info.NeedToMove));
                SlotController target =
                    slots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(info.TargetPosition));
                SwitchElements(needToMove, target);
            }
        }

        private void CheckSwitchBetweenElements(Dictionary<string, object> parameters)
        {
            SwipeEventsArgs swipeEventsArgs = (SwipeEventsArgs) parameters[EventsKeys.SWIPE];
            bool isCan = CanSwitch(swipeEventsArgs.ElementPosition, swipeEventsArgs.SwipeDirection);
            if (isCan)
            {
                ElementPosition targetSlot = _level.TargetElement;
                SlotController swiped = slots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(swipeEventsArgs.ElementPosition));
                SlotController target = slots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(targetSlot));
                SwitchElements(swiped, target); 
                Normalize();
            }
        }

        private void SwitchElements(SlotController swipedSlotElement, SlotController targetSlot)
        {
            ElementType swipedElementType = swipedSlotElement.ElementType;
            ElementType targetElementType = targetSlot.ElementType;
            swipedSlotElement.MoveElement(targetSlot.GetComponent<RectTransform>());
            targetSlot.MoveElement(swipedSlotElement.GetComponent<RectTransform>());
            swipedSlotElement.Initialize(targetElementType);
            targetSlot.Initialize(swipedElementType);
        }
        
        private bool CanSwitch(ElementPosition elementPosition, SwipeDirection swipeDirection) => _level.CanSwitchBetweenElements(elementPosition, swipeDirection);
    }
}