using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // ������� �������� ����������� ������
    public float fastSmoothSpeed = 0.3f; // ���������� �������� ��� ������ �����������
    public Vector3 offset = new Vector3(0, 2, -10); // �������� ������

    private Transform player;
    private Transform currentPlanet;
    private bool isFocusing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // ������������� �� �������, ����� ����� ������ �������
        Player.OnPlanetChange += FocusOnPlanet;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        Player.OnPlanetChange -= FocusOnPlanet;
    }

    private void FixedUpdate()
    {
        if (currentPlanet != null)
        {
            // ����� �������� � ����������� �� ��������� �����������
            float currentSmoothSpeed = isFocusing ? fastSmoothSpeed : smoothSpeed;

            // ������� ������� � ������ ��������
            Vector3 desiredPosition = new Vector3(currentPlanet.position.x, currentPlanet.position.y, transform.position.z) + offset;

            // ������������ ��� �������� �������� ������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void FocusOnPlanet(Transform planet)
    {
        currentPlanet = planet;
        isFocusing = true;

        // ��������� ����������� ����� �������� �����
        StartCoroutine(StopFocusingAfterDelay());
    }

    private IEnumerator StopFocusingAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); // ��������� �������� ����� ��������� � ������� ��������
        isFocusing = false;
    }
}
