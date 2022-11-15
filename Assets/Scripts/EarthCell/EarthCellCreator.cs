using System.Collections.Generic;
using UnityEngine;
using System;

namespace TestTask.Farm
{
    public class EarthCellCreator : MonoBehaviour
    {
        [SerializeField] private EarthCellsController earthCellController;
        [SerializeField] private GameObject cellPrefab;

        [Space]
        [SerializeField] private float spacing;
        [SerializeField] private float enlargedSpacing;

        [Space]
        [SerializeField] private int numberEarthCellsWithoutEnlargedSpacing;

        [Space]
        [SerializeField] private Transform parent;
        [SerializeField] private Transform startPosition;

        private int numberOfRows;
        private int objectsPerRow;

        private AbstractFactory abstractFactory;
        private List<EarthCell> earthCells = new List<EarthCell>();

        public static Action<List<EarthCell>> OnCreateEarthCells;
        public static Action<Vector3, Vector3> OnSetExtremeCellsPosition;

        private void OnEnable()
        {
            StartSettingsNumberOfEarthCells.OnAssignedStartSettings += GetNumbersOfEarthCell;
        }

        private void OnDisable()
        {
            StartSettingsNumberOfEarthCells.OnAssignedStartSettings -= GetNumbersOfEarthCell;
        }

        private void Start()
        {
            abstractFactory = new EarhCellFactory(cellPrefab, parent);          
        }

        private void GetNumbersOfEarthCell(int _numberOfRows, int _objectsPerRow)
        {
            numberOfRows = _numberOfRows;
            objectsPerRow = _objectsPerRow;

            if (numberOfRows > numberEarthCellsWithoutEnlargedSpacing && objectsPerRow > numberEarthCellsWithoutEnlargedSpacing)
            {
                spacing = enlargedSpacing;
            }

            CreatingEarthCelll();
        }

        private void CreatingEarthCelll()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int y = 0; y < objectsPerRow; y++)
                {
                    Vector3 _startingPosition = new Vector3(startPosition.position.x + y * spacing,
                                                            startPosition.position.y - 0.4f,
                                                            startPosition.position.z - i * spacing);

                    CreateCell(_startingPosition);
                }
            }

            OnCreateEarthCells?.Invoke(earthCells);

            OnSetExtremeCellsPosition?.Invoke(earthCells[0].gameObject.transform.position, earthCells[earthCells.Count - 1].gameObject.transform.position);         
        }

        private void CreateCell(Vector3 _position)
        {
            var obj = abstractFactory.CreateObject();
            obj.transform.position = _position;

            earthCells.Add(obj.GetComponent<EarthCell>());

            if (obj.TryGetComponent<EarthCell>(out var earthCell))
            {
                earthCell.Initialize(earthCells.IndexOf(obj.GetComponent<EarthCell>()), earthCellController);
            }           
        }
    }
}
