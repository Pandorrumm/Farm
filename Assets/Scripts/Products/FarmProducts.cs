using UnityEngine;
using System;
using UnityEngine.Pool;

namespace TestTask.Farm
{
    public class FarmProducts : MonoBehaviour
    {
        [SerializeField] private ChangingGrowthStage changingGrowthStage;
        [SerializeField] private GameObject maturationTimer;

        private float maturationTime;
        private int indexEarthCell;

        private TypeOfProduct.EnTypeOfProducts product;
        private TypeOfHarvest.EnTypeOfHarvest harvest;

        private ObjectPool<FarmProducts> carrotPool;
        private ObjectPool<FarmProducts> grassPool;
        private ObjectPool<FarmProducts> treePool;

        public static Action<TypeOfProduct.EnTypeOfProducts, int, TypeOfHarvest.EnTypeOfHarvest, ObjectPool<FarmProducts>> OnProductHasGrown;

        public void Initialize(TypeOfProduct.EnTypeOfProducts _product, float _maturationTime, int _indexEarthCell, TypeOfHarvest.EnTypeOfHarvest _harvest)
        {
            maturationTime = _maturationTime;
            product = _product;
            indexEarthCell = _indexEarthCell;
            harvest = _harvest;

            EnableChangingGrowthStage();
        }

        private void EnableChangingGrowthStage()
        {
            changingGrowthStage.Initialize(maturationTime);
            maturationTimer.SetActive(true);
        }

        public void ProductIsGrown()
        {
            maturationTimer.SetActive(false);

            switch (product)
            {
                case TypeOfProduct.EnTypeOfProducts.TREE:
                    OnProductHasGrown?.Invoke(product, indexEarthCell, harvest, treePool);
                    break;

                case TypeOfProduct.EnTypeOfProducts.CARROT:
                    OnProductHasGrown?.Invoke(product, indexEarthCell, harvest, carrotPool);
                    break;

                case TypeOfProduct.EnTypeOfProducts.GRASS:
                    OnProductHasGrown?.Invoke(product, indexEarthCell, harvest, grassPool);
                    break;
            }          
        }

        public void InitializeCarrotPool(ObjectPool<FarmProducts> _pool) => carrotPool ??= _pool;
        public void InitializeGrassPool(ObjectPool<FarmProducts> _pool) => grassPool ??= _pool;
        public void InitializeTreePool(ObjectPool<FarmProducts> _pool) => treePool ??= _pool;
    }
}
