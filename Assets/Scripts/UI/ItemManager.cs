using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Farm
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private ItemUIView itemUIPrefab = null;
        [SerializeField] private Transform itemsParent = null;
        [SerializeField] private ItemsDataBase itemsDataBase;

        private List<ItemUIView> uiViews = new List<ItemUIView>();

        private void Start()
        {
            Init(itemsDataBase.products);
        }

        public void Init(List<ItemData> _items)
        {
            for (int i = 0; i < uiViews.Count; i++)
            {
                Destroy(uiViews[i].gameObject);
            }

            uiViews.Clear();

            for (int i = 0; i < _items.Count; i++)
            {
                ItemUIView item = Instantiate(itemUIPrefab.gameObject, itemsParent).GetComponent<ItemUIView>();
                item.Init(_items[i].product);
                item.SetIcon(_items[i].icon);
                uiViews.Add(item);
            }
        }
    }
}
