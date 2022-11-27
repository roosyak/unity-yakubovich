using UnityEngine;
using PixselCrew.Model;
using UnityEngine.Events;
using System;

namespace PixselCrew.Components
{
    /*
     проверка нужного кол-ва предмета
     */
    public class RequiretItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _required;
        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var isAllRequirmentMet = true;

            foreach (var item in _required)
            {
                var numItem = session.Data.Inventory.Count(item.Id);
                if (numItem < item.Value)
                    isAllRequirmentMet = false;
            }

            if (isAllRequirmentMet)
            {
                if (_removeAfterUse)
                    foreach (var item in _required)
                        session.Data.Inventory.Remove(item.Id, item.Value);

                _onSuccess?.Invoke();
            }
            else
                _onFail?.Invoke();
        }

    }
}