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
        [SerializeField] private ElementsConfig _elementsConfig;
        [SerializeField] private SlotController slotPrefab;
        private List<SlotController> _generatedSlots;
        private Action _destroyElements;
        private Level _level;

        private void OnEnable() => EventsInvoker.StartListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);

        private void OnDisable() => EventsInvoker.StopListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);

        public void Generate(ElementType[,] level)
        {
            _level = new Level();
            _generatedSlots = _level.GenerateLevel(level, transform, slotPrefab, _elementsConfig);
        }

        private void Normalize()
        {
            var info = _level.NormalizeLevel();
            MoveElementsAfterNormalize(info);
            Invoke(nameof(Match), 1);
        }

        private void Match()
        {
            var list = _level.Match();
            ClearMatchedSlots(list);
        }

        private void ClearMatchedSlots(List<ElementPosition> matched)
        {
            if (matched.Count <= 0)
            {
                _level.CheckLevelProgress();
                return;
            }
            foreach (var slot in matched)
            {
                SlotController slotMatched =
                    _generatedSlots.FirstOrDefault(slotLocal => slotLocal.SlotElementPosition.Equals(slot));
                if(slotMatched != null)
                    _destroyElements += slotMatched.ClearElement;
            }
            _destroyElements.Invoke();
            _destroyElements = null;
            Invoke(nameof(Normalize), 1.5f);
        }

        private void MoveElementsAfterNormalize(List<InfoOfElementMoveAfterNormalize> infoOfElementMoveAfterNormalizes)
        {
            if (infoOfElementMoveAfterNormalizes.Count <= 0) return;
            foreach (var info in infoOfElementMoveAfterNormalizes)
            {
                SlotController needToMove =
                    _generatedSlots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(info.NeedToMove));
                SlotController target =
                    _generatedSlots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(info.TargetPosition));
                SwitchElements(needToMove, target);
            }
        }

        private void CheckSwitchBetweenElements(Dictionary<string, object> parameters)
        {
            SwipeEventsArgs swipeEventsArgs = (SwipeEventsArgs) parameters[EventsKeys.SWIPE];
            bool isCan = CanSwitch(swipeEventsArgs.ElementPosition, swipeEventsArgs.SwipeDirection);
            if (isCan)
            {
                EventsInvoker.TriggerEvent(EventsKeys.SWIPE_BLOCK, new Dictionary<string, object>(){{EventsKeys.SWIPE_BLOCK, false}});
                ElementPosition targetSlot = _level.TargetElementPosition;
                SlotController swiped = _generatedSlots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(swipeEventsArgs.ElementPosition));
                SlotController target = _generatedSlots.FirstOrDefault(slot => slot.SlotElementPosition.Equals(targetSlot));
                SwitchElements(swiped, target); 
                Invoke(nameof(Normalize), 1);
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
        
        private bool CanSwitch(ElementPosition elementPosition, SwipeDirection swipeDirection) => 
            _level.CanSwitchBetweenElements(elementPosition, swipeDirection);
    }
}