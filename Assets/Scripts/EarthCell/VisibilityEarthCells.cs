using System.Collections.Generic;
using UnityEngine;
using System;

namespace TestTask.Farm
{
    public class VisibilityEarthCells : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        [Space]
        [SerializeField] private float minDistance;

        private List<EarthCell> earthCells = new List<EarthCell>();

        public static Action OnCameraDoesNotSee;

        private void OnEnable()
        {
            EarthCellCreator.OnCreateEarthCells += GetEarthCells;
            CameraZoomer.OnCheckVisibilityEarthCells += CheckVisibilityEarthCells;
        }

        private void OnDisable()
        {
            EarthCellCreator.OnCreateEarthCells -= GetEarthCells;
            CameraZoomer.OnCheckVisibilityEarthCells -= CheckVisibilityEarthCells;
        }

        private bool IsVisible(Camera _cameara, GameObject _target)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(_cameara);
            var point = _target.transform.position;

            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point) < minDistance)
                {
                    return false;
                }
            }

            return true;
        }

        private void GetEarthCells(List<EarthCell> _earthCells)
        {
            earthCells = _earthCells;
        }

        private void CheckVisibilityEarthCells()
        {
            for (int i = 0; i < earthCells.Count; i++)
            {
                if (!IsVisible(mainCamera, earthCells[i].gameObject))
                {
                    OnCameraDoesNotSee?.Invoke();
                    return;
                }
            }
        }
    }
}
