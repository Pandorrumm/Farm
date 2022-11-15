using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Farm
{
    public class EarthCellsController : MonoBehaviour
    {
        [SerializeField] private Farmer farmer;

        public bool isEnableEarthCells { get; private set; }

        private void OnEnable()
        {
            ItemUIView.OnCloseSelectPanel += CheckClickEarthCell;
            Farmer.OnContinuePlanting += CheckClickEarthCell;
        }

        private void OnDisable()
        {
            ItemUIView.OnCloseSelectPanel -= CheckClickEarthCell;
            Farmer.OnContinuePlanting -= CheckClickEarthCell;
        }

        private void Start()
        {
            isEnableEarthCells = true;
        }

        private void CheckClickEarthCell()
        {
            if (!farmer.isFreeFromWork)
            {
                isEnableEarthCells = false;
            }
            else
            {
                isEnableEarthCells = true;
            }
        }
    }
}
