using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using UnityEngine.Pool;

namespace TestTask.Farm
{
    public class EarthCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private float offsetY;
        [SerializeField] private float duration;

        private bool isCanPlant = true;
        private bool isCanHarvest = false;
        private int indexCell;

        private EarthCellsController earthCellController;
        private TypeOfProduct.EnTypeOfProducts typeProduct;
        public TypeOfHarvest.EnTypeOfHarvest harvest { get; private set; }

        private ObjectPool<FarmProducts> pool;

        public static Action OnSelectEarthCell;
        public static Action<int, Transform> OnSetDataPlantEarthCell;
        public static Action<ObjectPool<FarmProducts>, int, Vector3, bool> OnSetDataHarvest;
        public static Action OnClickToEarthCell;
        public static Action<TypeOfProduct.EnTypeOfProducts> OnCollectedProduct;

        private void OnEnable()
        {
            FarmProducts.OnProductHasGrown += CanHarvest;
            Farmer.OnHarvestIsHarvested += DeleteProducts;
        }

        private void OnDisable()
        {
            FarmProducts.OnProductHasGrown -= CanHarvest;
            Farmer.OnHarvestIsHarvested -= DeleteProducts;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (earthCellController.isEnableEarthCells)
            {
                if (isCanPlant)
                {
                    ClickEffect();

                    OnSelectEarthCell?.Invoke();
                    OnSetDataPlantEarthCell?.Invoke(indexCell, spawnPoint);

                    isCanPlant = false;
                }

                if (isCanHarvest)
                {
                    OnSetDataHarvest?.Invoke(pool, indexCell, transform.position, true);

                    isCanPlant = true;
                    isCanHarvest = false;
                }
            }        
        }

        private void CanHarvest(TypeOfProduct.EnTypeOfProducts _product, int _index, TypeOfHarvest.EnTypeOfHarvest _harvest, ObjectPool<FarmProducts> _pool)
        {
            if (indexCell == _index)
            {
                typeProduct = _product;
                harvest = _harvest;
                pool = _pool;

                if (harvest != TypeOfHarvest.EnTypeOfHarvest.NONE)
                {
                    isCanHarvest = true;
                }               
            }
        }

        private void DeleteProducts(int _index, ObjectPool<FarmProducts> _pool)
        {
            if (indexCell == _index)
            {
                OnCollectedProduct?.Invoke(typeProduct);

                pool.Release(GetComponentInChildren<FarmProducts>());
            }
        }

        public void Initialize(int _index, EarthCellsController _earthCellController)
        {
            indexCell = _index;
            earthCellController = _earthCellController;
        }

        private void ClickEffect()
        {
            transform.DOMoveY(offsetY, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
