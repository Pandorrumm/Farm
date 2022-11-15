using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace TestTask.Farm
{
    public class ProductCreator : MonoBehaviour
    {
        [SerializeField] private List<ProductInfo> products = new List<ProductInfo>();

        public FarmProducts carrot;
        public FarmProducts grass;

        private static ObjectPool<FarmProducts> carrotPool;
        private static ObjectPool<FarmProducts> grassPool;
        private static ObjectPool<FarmProducts> treePool;

        private TypeOfProduct.EnTypeOfProducts typeProduct;

        private Transform spawnPosition;
        private int indexEarthCell;

        private void OnEnable()
        {
            Farmer.OnPlanted += CreateFarmProduct;
            EarthCell.OnSetDataPlantEarthCell += GetSpawnPosition;
        }

        private void OnDisable()
        {
            Farmer.OnPlanted -= CreateFarmProduct;
            EarthCell.OnSetDataPlantEarthCell -= GetSpawnPosition;
        }

        private void CreateProductPools()
        {
            if (grassPool != null)
                return;
            grassPool = new ObjectPool<FarmProducts>(GrassPoolCreationFunc, ActionOnGet, ActionOnRelease, null, true, 10, 5);

            if (carrotPool != null)
                return;
            carrotPool = new ObjectPool<FarmProducts>(CarrotPoolCreationFunc, ActionOnGet, ActionOnRelease, null, true, 10, 5);

            if (treePool != null)
                return;
            treePool = new ObjectPool<FarmProducts>(TreePoolCreationFunc, ActionOnGet, ActionOnRelease, null, true, 10, 5);
        }

        private FarmProducts CarrotPoolCreationFunc()
        {
            var result = Instantiate(GetFarmProduct(typeProduct), spawnPosition);
            result.InitializeCarrotPool(carrotPool);
            result.gameObject.SetActive(false);
            return result;
        } 

        private FarmProducts GrassPoolCreationFunc()
        {
            var result = Instantiate(GetFarmProduct(typeProduct), spawnPosition);
            result.InitializeGrassPool(grassPool);
            result.gameObject.SetActive(false);
            return result;
        }

        private FarmProducts TreePoolCreationFunc()
        {
            var result = Instantiate(GetFarmProduct(typeProduct), spawnPosition);
            result.InitializeTreePool(treePool);
            result.gameObject.SetActive(false);
            return result;
        }

        private void ActionOnGet(FarmProducts product) => product.gameObject.SetActive(true);

        private void ActionOnRelease(FarmProducts product) => product.gameObject.SetActive(false);

        private void CreateProduct(TypeOfProduct.EnTypeOfProducts _typeProduct)
        {
            switch (_typeProduct)
            {
                case TypeOfProduct.EnTypeOfProducts.TREE:
                    var tree = treePool.Get();
                    AssignDataProduct(tree, _typeProduct);
                    break;

                case TypeOfProduct.EnTypeOfProducts.CARROT:
                    var carrot = carrotPool.Get();
                    AssignDataProduct(carrot, _typeProduct);
                    break;

                case TypeOfProduct.EnTypeOfProducts.GRASS:
                    var grass = grassPool.Get();
                    AssignDataProduct(grass, _typeProduct);
                    break;
            }                   
        }

        private void AssignDataProduct(FarmProducts _product, TypeOfProduct.EnTypeOfProducts _typeProduct)
        {
            _product.transform.SetParent(spawnPosition.transform);
            _product.transform.position = spawnPosition.position;
            _product.Initialize(_typeProduct, GetMaturationTime(_typeProduct), indexEarthCell, GetTypeOfHarvest(_typeProduct));
        }

        private void CreateFarmProduct(TypeOfProduct.EnTypeOfProducts _product)
        {
            CreateProductPools();
            typeProduct = _product;
            CreateProduct(typeProduct);
        }

        private void GetSpawnPosition(int _indexCell, Transform _position)
        {
            indexEarthCell = _indexCell;
            spawnPosition = _position;
        }

        private float GetMaturationTime(TypeOfProduct.EnTypeOfProducts _product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].typeOfProduct.product == _product)
                {
                    return products[i].MaturationTime;
                }
            }
            return 0;
        }

        private TypeOfHarvest.EnTypeOfHarvest GetTypeOfHarvest(TypeOfProduct.EnTypeOfProducts _product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].typeOfProduct.product == _product)
                {
                    return products[i].TypeHarvest.harvest;
                }
            }
            return TypeOfHarvest.EnTypeOfHarvest.NONE;
        }

        private FarmProducts GetFarmProduct(TypeOfProduct.EnTypeOfProducts _product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].typeOfProduct.product == _product)
                {
                    return products[i].ProductPrefab;
                }
            }
            return null;
        }

        private void OnDestroy()
        {
            treePool.Clear();
            treePool = null;

            grassPool.Clear();
            grassPool = null;

            carrotPool.Clear();
            carrotPool = null;
        }
    }
}

