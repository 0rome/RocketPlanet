using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullRadius = 10f;          // Радиус притяжения
    public float maxPullForce = 10000f;     // Максимальная сила притяжения
    public float destructionRadius = 1f;    // Радиус уничтожения

    private void FixedUpdate()
    {
        // Находим все объекты в радиусе притяжения
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);

        foreach (var collider in colliders)
        {
            // Проверяем, что у объекта есть Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - rb.transform.position).normalized;
                float distance = Vector2.Distance(transform.position, rb.transform.position);

                // Применяем сильное притяжение, инверсное квадрату расстояния
                float pullForce = maxPullForce / (distance * distance);
                rb.AddForce(direction * pullForce * Time.fixedDeltaTime, ForceMode2D.Force);

                // Уничтожение объектов в радиусе уничтожения
                if (distance < destructionRadius)
                {
                    //Destroy(rb.gameObject);
                }
            }
        }
    }
}
