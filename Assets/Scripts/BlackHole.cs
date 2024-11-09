using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullRadius = 10f;          // ������ ����������
    public float maxPullForce = 10000f;     // ������������ ���� ����������
    public float destructionRadius = 1f;    // ������ �����������

    private void FixedUpdate()
    {
        // ������� ��� ������� � ������� ����������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);

        foreach (var collider in colliders)
        {
            // ���������, ��� � ������� ���� Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - rb.transform.position).normalized;
                float distance = Vector2.Distance(transform.position, rb.transform.position);

                // ��������� ������� ����������, ��������� �������� ����������
                float pullForce = maxPullForce / (distance * distance);
                rb.AddForce(direction * pullForce * Time.fixedDeltaTime, ForceMode2D.Force);

                // ����������� �������� � ������� �����������
                if (distance < destructionRadius)
                {
                    //Destroy(rb.gameObject);
                }
            }
        }
    }
}
