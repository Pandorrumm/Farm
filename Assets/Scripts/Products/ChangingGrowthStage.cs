using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TestTask.Farm
{
    public class ChangingGrowthStage : MonoBehaviour
    {
        [SerializeField] private List<GameObject> stagesGrowth = new List<GameObject>();

        [Space]
        [SerializeField] private Image maturationSlider;

        [Space]
        [SerializeField] private float maturingChangeSpeed;

        [Space]
        [SerializeField] private FarmProducts farmProducts;

        private int currentStageGrowth;
        private float maturationTime;
        private List<float> durationsStage = new List<float>();
        private bool isMaturing = true;
        private float currentMaturingValue = 0;

        public void Initialize(float _maturationTime)
        {
            maturationTime = _maturationTime;

            StartChangingGrowthStage();
        }

        private void GetDurationStage()
        {
            float time = 0f;
            durationsStage.Add(time);

            for (float i = 0; i < stagesGrowth.Count - 1; i++)
            {
                time += 1f / (stagesGrowth.Count - 1);
                durationsStage.Add(time);
            }
        }

        private void StartChangingGrowthStage()
        {           
            AssignDefaultValue();
            GetDurationStage();
            StartCoroutine(MoveSliderTime());
        }

        private void AssignDefaultValue()
        {
            maturationSlider.fillAmount = 0;
            isMaturing = true;
            currentMaturingValue = 0;
            currentStageGrowth = 0;
            durationsStage.Clear();
        }

        private IEnumerator MoveSliderTime()
        {
            while (isMaturing)
            {
                currentMaturingValue += maturingChangeSpeed / maturationTime;

                if (currentMaturingValue >= durationsStage[currentStageGrowth])
                {
                    ChangingStage(currentStageGrowth);

                    currentStageGrowth++;

                    currentMaturingValue = durationsStage[currentStageGrowth - 1];

                    if (currentStageGrowth == durationsStage.Count)
                    {
                        ProductIsGrown();

                        isMaturing = false;                       
                        yield return null;
                    }                 
                }

                maturationSlider.fillAmount = currentMaturingValue;
                yield return new WaitForSeconds(maturingChangeSpeed);
            }

            yield return null;
        }

        private void ChangingStage(int _value)
        {
            for (int i = 0; i < stagesGrowth.Count; i++)
            {
                stagesGrowth[i].SetActive(false);
            }

            stagesGrowth[_value].SetActive(true);
        }

        private void ProductIsGrown()
        {
            farmProducts.ProductIsGrown();
        }
    }
}
