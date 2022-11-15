using UnityEngine;

namespace TestTask.Farm
{
    [CreateAssetMenu(menuName = "TestTask/Farm/Product")]
    public class ProductInfo : ScriptableObject
    {
        [SerializeField] private TypeOfProduct product;
        public TypeOfProduct typeOfProduct => product;

        [SerializeField] private int numberOfPiecesInEarthCell;
        public int NumberOfPiecesInEarthCell => numberOfPiecesInEarthCell;

        [SerializeField] private int givesExperience;
        public int GivesExperience => givesExperience;

        [SerializeField] private float maturationTime;
        public float MaturationTime => maturationTime;

        [SerializeField] private FarmProducts productPrefab;
        public FarmProducts ProductPrefab => productPrefab;

        [SerializeField] private TypeOfHarvest typeHarvest;
        public TypeOfHarvest TypeHarvest => typeHarvest;
    }
}
