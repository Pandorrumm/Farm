using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TestTask.Farm
{
    public class StartSettingsNumberOfEarthCells : MonoBehaviour
    {
        [SerializeField] private string keyPanel;

        [Space]
        [SerializeField] private Slider numberOfRowSlider;
        [SerializeField] private Slider numberObjectPerRowSlider;

        [Space]
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;

        [Space]
        [SerializeField] private TMP_Text numberOfRowText;
        [SerializeField] private TMP_Text numberObjectPerRowText;

        [Space]
        [SerializeField] private Button startGameButton;

        public static Action<int, int> OnAssignedStartSettings;

        private void Start()
        {
            startGameButton.onClick.AddListener(StartGame);

            AssignStartingSlidersValue();

            UpdateNumberOfRowText(numberOfRowSlider.value);
            numberOfRowSlider.onValueChanged.AddListener(UpdateNumberOfRowText);

            UpdateNumberObjectPerRowSText(numberObjectPerRowSlider.value);
            numberObjectPerRowSlider.onValueChanged.AddListener(UpdateNumberObjectPerRowSText);
        }

        private void UpdateNumberOfRowText(float _value)
        {
            numberOfRowText.text = numberOfRowSlider.value.ToString();
        }

        private void UpdateNumberObjectPerRowSText(float _value)
        {
            numberObjectPerRowText.text = numberObjectPerRowSlider.value.ToString();
        }

        private void AssignStartingSlidersValue()
        {
            numberOfRowSlider.minValue = minValue;
            numberOfRowSlider.maxValue = maxValue;

            numberObjectPerRowSlider.minValue = minValue;
            numberObjectPerRowSlider.maxValue = maxValue;
        }

        private void StartGame()
        {
            OnAssignedStartSettings?.Invoke((int)numberOfRowSlider.value, (int)numberObjectPerRowSlider.value);
            gameObject.SetActive(false);
        }
    }
}
