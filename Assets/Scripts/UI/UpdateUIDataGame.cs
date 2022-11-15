using UnityEngine;
using TMPro;
using DG.Tweening;

namespace TestTask.Farm
{
    public class UpdateUIDataGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberCarrotText;
        [SerializeField] private TMP_Text numberExperienceText;

        [Space]
        [SerializeField] private float updateDurationProductText;
        [SerializeField] private float updateDurationExperienceText;

        private int numberOfCarrot;
        private int numberOfExperience;

        private void OnEnable()
        {
            GameCounter.OnAddedProduct += UpdateNumberOfProduct;
            GameCounter.OnAddedExperiance += UpdateNumberOfExperience;
        }

        private void OnDisable()
        {
            GameCounter.OnAddedProduct -= UpdateNumberOfProduct;
            GameCounter.OnAddedExperiance -= UpdateNumberOfExperience;
        }

        private void Start()
        {
            UpdateNumberOfProduct(0, TypeOfProduct.EnTypeOfProducts.CARROT);
            UpdateNumberOfExperience(0);
        }

        private void UpdateNumberOfProduct(int _value, TypeOfProduct.EnTypeOfProducts _product)
        {
            switch (_product)
            {
                case TypeOfProduct.EnTypeOfProducts.CARROT:
                    DOTween.To(() => numberOfCarrot, x => numberOfCarrot = x, numberOfCarrot + _value, updateDurationProductText)
                        .OnUpdate(() => numberCarrotText.text = numberOfCarrot.ToString());
                    break;
                case TypeOfProduct.EnTypeOfProducts.GRASS:
                    break;
            }
        }

        private void UpdateNumberOfExperience(int _value)
        {
            DOTween.To(() => numberOfExperience, x => numberOfExperience = x, numberOfExperience + _value, updateDurationExperienceText)
                .OnUpdate(() => numberExperienceText.text = numberOfExperience.ToString());
        }
    }
}
