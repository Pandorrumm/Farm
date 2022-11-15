using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Pool;

namespace TestTask.Farm
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Farmer : MonoBehaviour
    {
        private StateMachine stateMachine;

        private WalkingState walkState;
        private IdleState idleState;
        private CollectState collectState;
        private PlantState plantState;
        private MowState mowState;

        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Vector3 targetPosition;
        private bool isHarvest;
        private TypeOfProduct.EnTypeOfProducts typeProduct;
        private int indexEarthCell;
        private List<EarthCell> earthCells = new List<EarthCell>();

        private ObjectPool<FarmProducts> pool;
        public bool isFreeFromWork { get; private set; }

        public static Action<TypeOfProduct.EnTypeOfProducts> OnPlanted;
        public static Action OnContinuePlanting;
        public static Action<int, ObjectPool<FarmProducts>> OnHarvestIsHarvested;
        public static Action<Vector3> OnFarmerStartPlanting;
        public static Action OnFarmerStopPlanting;

        private void OnEnable()
        {
            EarthCell.OnSetDataHarvest += OnGoHarvest;
            EarthCell.OnSetDataPlantEarthCell += GetDataEarthCell;
            ItemUIView.OnSelectProduct += WalkPlantProducts;
            WalkingState.OnCameToTarget += AssignStateForEarthCell;
            EarthCellCreator.OnCreateEarthCells += AddEarthCell;
        }

        private void OnDisable()
        {
            EarthCell.OnSetDataPlantEarthCell -= GetDataEarthCell;
            EarthCell.OnSetDataHarvest -= OnGoHarvest;
            ItemUIView.OnSelectProduct -= WalkPlantProducts;
            WalkingState.OnCameToTarget -= AssignStateForEarthCell;
            EarthCellCreator.OnCreateEarthCells -= AddEarthCell;
        }

        private void OnGoHarvest(ObjectPool<FarmProducts> _pool, int _index, Vector3 _targetPosition, bool _isHarvest)
        {
            pool = _pool;
            indexEarthCell = _index;
            isHarvest = _isHarvest;

            StartWalkingState(_targetPosition, indexEarthCell, isHarvest);
        }
       
        private void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            stateMachine = new StateMachine();

            isFreeFromWork = true;

            idleState = new IdleState(animator);
            stateMachine.Initialize(idleState);
        }

        private void Update()
        {
            stateMachine.CurrentState.Update();
        }

        private void GetDataEarthCell(int _index, Transform _transform)
        {
            targetPosition = _transform.position;
            indexEarthCell = _index;
        }

        private void WalkPlantProducts(TypeOfProduct.EnTypeOfProducts _product)
        {
            typeProduct = _product;
            StartWalkingState(targetPosition, indexEarthCell, isHarvest);
        }

        private void StartWalkingState(Vector3 _targetPosition, int _indexEarthCell, bool _isHarvest)
        {
            isFreeFromWork = false;
            walkState = new WalkingState(this, animator, navMeshAgent, _targetPosition, _indexEarthCell, _isHarvest);
            stateMachine.ChangeState(walkState);
        }

        private void StartIdleState()
        {
            isFreeFromWork = true;
            stateMachine.ChangeState(idleState);
        }

        private void StartPlantState()
        {
            isFreeFromWork = false;
            plantState = new PlantState(animator);
            stateMachine.ChangeState(plantState);
        }

        private void StartMowState()
        {
            isFreeFromWork = false;
            mowState = new MowState(animator);
            stateMachine.ChangeState(mowState);
        }

        private void StartCollectState()
        {
            isFreeFromWork = false;
            collectState = new CollectState(animator);
            stateMachine.ChangeState(collectState);
        }    

        private void AssignStateForEarthCell(int _indexEarthCell, bool _isHarvest)
        {
            if (isHarvest)
            {
                if (earthCells[_indexEarthCell].harvest == TypeOfHarvest.EnTypeOfHarvest.COLLECT)
                {
                    StartCollectState();
                }
                else if (earthCells[_indexEarthCell].harvest == TypeOfHarvest.EnTypeOfHarvest.MOW)
                {
                    StartMowState();
                }

                isHarvest = false;
            }
            else
            {
                OnFarmerStartPlanting?.Invoke(targetPosition);
                StartPlantState();
            }
        }

        private void AddEarthCell(List<EarthCell> _earthCells)
        {
            earthCells = _earthCells;
        }

        private void EndPlantedAnimationEvent()
        {
            StartIdleState();
            OnFarmerStopPlanting?.Invoke();
            OnContinuePlanting?.Invoke();          
            OnPlanted?.Invoke(typeProduct);
        }

        private void EndHarvestAnimationEvent()
        {
            StartIdleState();
            OnContinuePlanting?.Invoke();
            OnHarvestIsHarvested?.Invoke(indexEarthCell, pool);
        }
    }
}
