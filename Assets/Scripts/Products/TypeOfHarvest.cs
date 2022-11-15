
namespace TestTask.Farm
{
    [System.Serializable]
    public class TypeOfHarvest
    {
        public enum EnTypeOfHarvest
        {
            NONE,
            COLLECT,
            MOW
        }

        public EnTypeOfHarvest harvest;
    }
}
