using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TestTask.Farm
{
    [RequireComponent(typeof(Button))]
    public class RestartScene : MonoBehaviour
    {
        private Button restartButton;

        private void Start()
        {
            restartButton = GetComponent<Button>();

            restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
