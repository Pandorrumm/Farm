using System;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Farm
{
    [RequireComponent(typeof(Button))]
    public class ItemUIView : MonoBehaviour
    {
        [SerializeField] private Image icon;

        private Button selectButton;
        private TypeOfProduct.EnTypeOfProducts product;

        public static Action<TypeOfProduct.EnTypeOfProducts> OnSelectProduct;
        public static Action OnCloseSelectPanel;

        private void Start()
        {
            selectButton = GetComponent<Button>();
            selectButton.onClick.AddListener(PlantProduct);
        }

        public void SetIcon(Sprite _icon)
        {
            icon.sprite = _icon;
        }

        public void Init(TypeOfProduct.EnTypeOfProducts _product)
        {
            product = _product;         
        }

        private void PlantProduct()
        {
            OnSelectProduct?.Invoke(product);
            OnCloseSelectPanel?.Invoke();
        }
    }
}
