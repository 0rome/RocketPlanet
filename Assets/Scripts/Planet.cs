using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Planet : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isAnimating = false;
    private float scale;
    [SerializeField] private float scaleMultiplier = 0.8f; // ���������� �� 90% �� ��������� �������
    [SerializeField] private float animationDuration = 0.2f; // ����� ��� ���������� � �����������

    private void Start()
    {
        scale = Random.Range(1f, 2f);
        transform.localScale = new Vector3(scale, scale, scale);
        originalScale = transform.localScale; // ���������� ������� ��������� ������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAnimating && other.CompareTag("Player"))
        {
            StartCoroutine(AnimateScale());
        }
    }

    private IEnumerator AnimateScale()
    {
        isAnimating = true;
        Vector3 targetScale = originalScale * scaleMultiplier;
        float elapsedTime = 0f;

        // ����������
        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        elapsedTime = 0f;

        // ������� � ��������� �������
        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false;
    }
}
  
