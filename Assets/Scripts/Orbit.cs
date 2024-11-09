using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform planet;      // Ссылка на объект планеты
    public float orbitSpeed = 1f; // Скорость вращения
    public float orbitRadius = 2f; // Радиус орбиты

    private float orbitAngle;

    void Start()
    {
        if (planet == null)
        {
            Debug.LogError("Planet is not assigned in the Orbit script.");
            return;
        }

        // Вычисляем начальное положение на орбите
        Vector3 offset = (transform.position - planet.position).normalized * orbitRadius;
        transform.position = planet.position + offset;

        // Устанавливаем начальный угол орбиты
        orbitAngle = Mathf.Atan2(offset.y, offset.x);
    }

    void FixedUpdate()
    {
        if (planet == null) return;

        // Обновляем угол орбиты в зависимости от скорости
        orbitAngle += orbitSpeed * Time.deltaTime;

        // Вычисляем новое положение на орбите
        float x = planet.position.x + Mathf.Cos(orbitAngle) * orbitRadius;
        float y = planet.position.y + Mathf.Sin(orbitAngle) * orbitRadius;
        transform.position = new Vector3(x, y, transform.position.z);

        // Поворачиваем объект в сторону движения
        Vector3 direction = new Vector3(-Mathf.Sin(orbitAngle), Mathf.Cos(orbitAngle), 0);
        transform.up = direction;
    }
}
