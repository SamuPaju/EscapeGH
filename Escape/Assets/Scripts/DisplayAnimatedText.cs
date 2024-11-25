using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class DisplayAnimatedText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // Ссылка на TextMeshPro - Text
    public string[] messages; // Массив строк для отображения
    public float typingSpeed = 0.05f; // Скорость анимации текста
    public float delayBeforeClear = 1f; // Задержка перед исчезновением текста после проигрывания всех строк

    private bool isTextStarted = false; // Отслеживает, был ли текст уже запущен
    private bool isTyping = false;

    [SerializeField] private UnityEvent startText;

    public UnityEvent StartText => startText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTextStarted) // Проверяем тег игрока и статус текста
        {
            isTextStarted = true; // Отмечаем, что текст уже запускался
            StartCoroutine(PlayTextSequence());
        }
    }

    private IEnumerator PlayTextSequence()
    {
        foreach (string message in messages)
        {
            yield return StartCoroutine(AnimateText(message)); // Проигрываем каждую строку по очереди
            yield return new WaitForSeconds(1f); // Задержка между строками
        }

        yield return new WaitForSeconds(delayBeforeClear); // Ожидание перед очисткой текста
        textDisplay.text = ""; // Убираем текст с экрана
        startText.Invoke();
    }

    private IEnumerator AnimateText(string message)
    {
        isTyping = true;
        textDisplay.text = ""; // Очищаем текст перед началом
        foreach (char letter in message)
        {
            textDisplay.text += letter; // Постепенно добавляем символы
            yield return new WaitForSeconds(typingSpeed); // Задержка между символами
        }
        isTyping = false;
    }
}
