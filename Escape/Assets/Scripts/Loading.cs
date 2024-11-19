using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class Loading : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText; // Ссылка на текст "Downloading..."
    [SerializeField] private string baseMessage = "Downloading";

    private void Start()
    {
        StartCoroutine(AnimateDots());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator AnimateDots()
    {
        int dotCount = 0;
        while (true)
        {
            // Обновляем текст с анимацией троеточия
            loadingText.text = baseMessage + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // Ограничиваем количество точек до трех
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Пока сцена загружается, показываем экран загрузки
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                // Когда загрузка почти завершена, активируем сцену
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
