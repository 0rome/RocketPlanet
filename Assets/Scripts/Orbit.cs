using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform planet;      // ������ �� ������ �������
    public float orbitSpeed = 1f; // �������� ��������
    public float orbitRadius = 2f; // ������ ������

    private float orbitAngle;

    void Start()
    {
        if (planet == null)
        {
            Debug.LogError("Planet is not assigned in the Orbit script.");
            return;
        }

        // ��������� ��������� ��������� �� ������
        Vector3 offset = (transform.position - planet.position).normalized * orbitRadius;
        transform.position = planet.position + offset;

        // ������������� ��������� ���� ������
        orbitAngle = Mathf.Atan2(offset.y, offset.x);
    }

    void FixedUpdate()
    {
        if (planet == null) return;

        // ��������� ���� ������ � ����������� �� ��������
        orbitAngle += orbitSpeed * Time.deltaTime;

        // ��������� ����� ��������� �� ������
        float x = planet.position.x + Mathf.Cos(orbitAngle) * orbitRadius;
        float y = planet.position.y + Mathf.Sin(orbitAngle) * orbitRadius;
        transform.position = new Vector3(x, y, transform.position.z);

        // ������������ ������ � ������� ��������
        Vector3 direction = new Vector3(-Mathf.Sin(orbitAngle), Mathf.Cos(orbitAngle), 0);
        transform.up = direction;
    }
}
