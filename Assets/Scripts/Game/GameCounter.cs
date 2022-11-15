using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Farm
{
    public class GameCounter : MonoBehaviour
    {
        [SerializeField] private List<ProductInfo> productInfos = new List<ProductInfo>();

        public static Action<int, TypeOfProduct.EnTypeOfProducts> OnAddedProduct;
        public static Action<int> OnAddedExperiance;

        private void OnEnable()
        {
            EarthCell.OnCollectedProduct += CollectedProducts;
        }

        private void OnDisable()
        {
            EarthCell.OnCollectedProduct -= CollectedProducts;
        }

        private void CollectedProducts(TypeOfProduct.EnTypeOfProducts _product)
        {           
            switch (_product)
            {
                case TypeOfProduct.EnTypeOfProducts.CARROT:
                    OnAddedProduct?.Invoke(GetNumberOfPiecesInEarthCell(_product), _product);
                    OnAddedExperiance?.Invoke(GetGivesExperience(_product));
                    
                    break;
                case TypeOfProduct.EnTypeOfProducts.GRASS:
                    OnAddedExperiance?.Invoke(GetGivesExperience(_product));
                    break;
            }                     
        }
        private int GetNumberOfPiecesInEarthCell(TypeOfProduct.EnTypeOfProducts _product)
        {
            for (int i = 0; i < productInfos.Count; i++)
            {
                if (productInfos[i].typeOfProduct.product == _product)
                {
                    return productInfos[i].NumberOfPiecesInEarthCell;
                }
            }

            return 0;
        }

        private int GetGivesExperience(TypeOfProduct.EnTypeOfProducts _product)
        {
            for (int i = 0; i < productInfos.Count; i++)
            {
                if (productInfos[i].typeOfProduct.product == _product)
                {
                    return productInfos[i].GivesExperience;
                }
            }

            return 0;
        }
    }
}
