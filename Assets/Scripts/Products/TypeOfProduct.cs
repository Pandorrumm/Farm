
namespace TestTask.Farm
{
    [System.Serializable]
    public class TypeOfProduct
    {
        public enum EnTypeOfProducts
        {
            TREE,
            CARROT,
            GRASS
        }

        public EnTypeOfProducts product;
    }
}
