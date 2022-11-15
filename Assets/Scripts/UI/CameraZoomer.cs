using UnityEngine;
using DG.Tweening;
using System;

namespace TestTask.Farm
{
    public class CameraZoomer : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject target;

        [Space]
        [SerializeField] private float maxZoomValue;
        [SerializeField] private float maxZoomIfNotFitCell;

        [Space]
        [SerializeField] private float zoomDuration;       
        [SerializeField] private float cameraRotateDuration;
        [SerializeField] private float delayZoomOut;
        [SerializeField] private float cameraCenteringStartDelay;

        private Vector3 startCameraRotation;
        private float startOrthographicSize;

        public static Action OnCheckVisibilityEarthCells;

        private void OnEnable()
        {
            EarthCellCreator.OnSetExtremeCellsPosition += CenteringCamera;
            Farmer.OnFarmerStartPlanting += ZoomIn;
            Farmer.OnFarmerStopPlanting += ZoomOut;
            VisibilityEarthCells.OnCameraDoesNotSee += ZoomOutIfCellDoesNotFit;
        }

        private void OnDisable()
        {
            EarthCellCreator.OnSetExtremeCellsPosition -= CenteringCamera;
            Farmer.OnFarmerStartPlanting -= ZoomIn;
            Farmer.OnFarmerStopPlanting -= ZoomOut;
            VisibilityEarthCells.OnCameraDoesNotSee -= ZoomOutIfCellDoesNotFit;
        }

        private void CenteringCamera(Vector3 _cellPosition1, Vector3 _cellPosition2)
        {
            Vector3 center = ((_cellPosition2 - _cellPosition1) / 2f) + _cellPosition1;
            mainCamera.transform.DOLookAt(center, cameraRotateDuration).SetDelay(cameraCenteringStartDelay).OnComplete(CameraHasCompletedRotate);

            void CameraHasCompletedRotate()
            {
                SaveCameraStartingData();
                OnCheckVisibilityEarthCells?.Invoke();
            }            
        }

        private void SaveCameraStartingData()
        {
            startCameraRotation = mainCamera.transform.eulerAngles;
            startOrthographicSize = mainCamera.orthographicSize;
        }
     
        private void ZoomIn(Vector3 _earthCellPosition)
        {
            mainCamera.transform.DOLookAt(_earthCellPosition, cameraRotateDuration);
            DOTween.To(() => mainCamera.orthographicSize, value => mainCamera.orthographicSize = value, maxZoomValue, zoomDuration);
        }

        private void ZoomOut()
        {
            mainCamera.transform.DORotate(startCameraRotation, cameraRotateDuration).SetDelay(delayZoomOut);
            DOTween.To(() => mainCamera.orthographicSize, value => mainCamera.orthographicSize = value, startOrthographicSize, zoomDuration).SetDelay(delayZoomOut);
        }     

        private void ZoomOutIfCellDoesNotFit()
        {
            startOrthographicSize = maxZoomIfNotFitCell;
            ZoomOut();
        }
    }
}
