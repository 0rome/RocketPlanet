using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // Обычная скорость сглаживания камеры
    public float fastSmoothSpeed = 0.3f; // Ускоренная скорость при первой фокусировке
    public Vector3 offset = new Vector3(0, 2, -10); // Смещение камеры

    private Transform player;
    private Transform currentPlanet;
    private bool isFocusing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Подписываемся на событие, когда игрок меняет планету
        Player.OnPlanetChange += FocusOnPlanet;
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        Player.OnPlanetChange -= FocusOnPlanet;
    }

    private void FixedUpdate()
    {
        if (currentPlanet != null)
        {
            // Выбор скорости в зависимости от состояния фокусировки
            float currentSmoothSpeed = isFocusing ? fastSmoothSpeed : smoothSpeed;

            // Целевая позиция с учетом смещения
            Vector3 desiredPosition = new Vector3(currentPlanet.position.x, currentPlanet.position.y, transform.position.z) + offset;

            // Интерполяция для плавного движения камеры
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void FocusOnPlanet(Transform planet)
    {
        currentPlanet = planet;
        isFocusing = true;

        // Остановка фокусировки через короткое время
        StartCoroutine(StopFocusingAfterDelay());
    }

    private IEnumerator StopFocusingAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); // Небольшая задержка перед переходом к обычной скорости
        isFocusing = false;
    }
}
