using UnityEngine;

namespace TestTask.Farm
{
    public class SelectPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject selectProductPanel;

        private void OnEnable()
        {
            EarthCell.OnSelectEarthCell += OpenSelectProductPanel;
            ItemUIView.OnCloseSelectPanel += CloseSelectProductPanel;
        }

        private void OnDisable()
        {
            EarthCell.OnSelectEarthCell -= OpenSelectProductPanel;
            ItemUIView.OnCloseSelectPanel -= CloseSelectProductPanel;
        }

        private void OpenSelectProductPanel()
        {
            selectProductPanel.SetActive(true);
        }

        private void CloseSelectProductPanel()
        {
            selectProductPanel.SetActive(false);
        }
    }
}
