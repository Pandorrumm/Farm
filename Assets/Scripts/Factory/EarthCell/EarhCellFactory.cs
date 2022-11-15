using UnityEngine;

namespace TestTask.Farm
{
    public class EarhCellFactory : AbstractFactory
    {
        private GameObject earthCell;
        private Transform parent;

        public EarhCellFactory(GameObject _earthCell, Transform _parent)
        {
            earthCell = _earthCell;
            parent = _parent;
        }

        public override GameObject CreateObject()
        {
            GameObject soil = GameObject.Instantiate(earthCell, parent);
            return soil;
        }
    }
}
