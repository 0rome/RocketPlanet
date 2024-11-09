using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event System.Action OnPlanetTouched;
    public static event System.Action<Transform> OnPlanetChange;

    public Transform currentPoint;
    public GameObject FireEffect;
    public GameObject[] Effects;

    private GameObject soundManager;
    private GameObject scoreManager;
    private float maxStretch = 3.0f;
    private float launchForceMultiplier = 10.0f;
    private float orbitSpeed = 1.25f;
    private float orbitRadius = 1.5f;
    private float minLaunchDistance = 1f;
    private float orbitAngle = 0f;
    private bool isDragging = false;
    private bool isFlying = false;
    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        mainCamera = Camera.main;
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        rb = GetComponent<Rigidbody2D>();
        if (currentPoint != null)
        {
            SetOrbitPosition(currentPoint); // ������������� ��������� ������� �� ������
        }
    }
    private void SetCameraSize()
    {
        if (currentPoint != null)
        {
            // �������� ������ ������� ����� ��������
            float size = currentPoint.GetComponent<Collider2D>().bounds.size.x;

            // ������������� ������ ������, ��������������� ������� �����
            mainCamera.orthographicSize = size + 5;
        }
    }
    private void FixedUpdate()
    {
        if (!isDragging && currentPoint != null && !isFlying)
        {
            OrbitAroundPoint();
        }
        if (!isFlying && !isDragging && currentPoint != null)
        {
            UpdateOrbit();
        }
        if (isFlying)
        {
            // ���������, ��� �������� ���������� ������, ����� ������������ ������
            if (rb.velocity.magnitude > 0.1f)
            {
                // �������� ���� ����������� �������� � ������������ ������
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            }
        }
    }
    private void SetOrbitRadius()
    {
        if (currentPoint != null)
        {
            // �������� ������ ������ ����� �������� �� ��� X (��� Y, ���� �����)
            float size = currentPoint.GetComponent<Collider2D>().bounds.size.x;

            // ������������� ������ ������ ��� 1.5 ������� �����
            orbitRadius = 1f + 0.5f * size;

            orbitSpeed = 1.25f / size;
        }
    }
    private void OrbitAroundPoint()
    {
        // ��������, ��� ������ �������� ������ ������� �������� � � ���������� ��������
        if (currentPoint != null)
        {
            orbitAngle += orbitSpeed * Time.deltaTime;

            // ׸��� ����� ������ ������ � ����������� ������ ������� ������ ��������
            float x = currentPoint.position.x + Mathf.Cos(orbitAngle) * orbitRadius;
            float y = currentPoint.position.y + Mathf.Sin(orbitAngle) * orbitRadius;

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
    private void UpdateOrbit()
    {
        if (currentPoint != null)
        {
            // ��������� ���� ������
            orbitAngle += orbitSpeed * Time.deltaTime;

            // ������������ ������� �� ������
            float x = currentPoint.position.x + orbitRadius * Mathf.Cos(orbitAngle);
            float y = currentPoint.position.y + orbitRadius * Mathf.Sin(orbitAngle);
            transform.position = new Vector3(x, y, transform.position.z);

            // ������������ ����������� �������� �� ������
            Vector2 tangentDirection = new Vector2(-Mathf.Sin(orbitAngle), Mathf.Cos(orbitAngle));
            float angle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) * Mathf.Rad2Deg;

            // ������������ ������ � ������� ��������
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        }
    }
    private void OnMouseDown()
    {
        if (currentPoint != null && !isFlying)
        {
            isDragging = true;
            rb.isKinematic = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && currentPoint != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = currentPoint.position - mousePosition; // ����������� ����������� ��� ���������������� �������

            FireEffect.SetActive(false);

            // ������������ ���������
            if (offset.magnitude > maxStretch)
            {
                offset = offset.normalized * maxStretch;
            }

            // ������������� ������� ������ ������������ ����� ����� ��������
            transform.position = currentPoint.position - offset;

            // ������������ ������ �������������� ����������� ���������
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            float stretchDistance = Vector3.Distance(transform.position, currentPoint.position);
            FireEffect.SetActive(true);
            if (stretchDistance >= minLaunchDistance)
            {
                soundManager.GetComponent<SoundManager>().Planet(1);

                Vector3 launchDirection = (currentPoint.position - transform.position).normalized;
                float clampedDistance = Mathf.Min(stretchDistance, maxStretch);

                rb.isKinematic = false;

                // ���������� �������� ��� ������� ��������
                rb.velocity = Vector2.zero;

                // ��������� ���� ��������
                rb.AddForce(launchDirection * clampedDistance * launchForceMultiplier, ForceMode2D.Impulse);

                // ��������� ������ � ��������� � ����� ������
                isFlying = true;
                isDragging = false;
            }
            else
            {
                SetOrbitPosition(currentPoint); // ������������ �� ������, ���� ��������� �������������
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AnchorPoint"))
        {
            scoreManager.GetComponent<ScoreManager>().TouchPlanet();
            soundManager.GetComponent<SoundManager>().Planet(0);
            Instantiate(Effects[1], new Vector2(transform.position.x,transform.position.y +1), Quaternion.identity);


            other.tag = "Untagged"; // �������� ��� �����, ����� �� ������������ �� ���
            currentPoint = other.transform;

            OnPlanetTouched?.Invoke();  // �������� ������� ��� ������� �������
            OnPlanetChange?.Invoke(currentPoint);

            // ������������� ������� ������ �� ������ ����� ����� ���������������
            StartOrbitFromContactPoint(currentPoint);

            // ������������� ������ ������ � ����������� �� ������� �����
            SetOrbitRadius();
            SetCameraSize();

            isFlying = false;

            // �������� ���������� ��������� ��� ��������
            Debug.Log($"Player started orbiting around {currentPoint.name} at angle {orbitAngle} and position {transform.position}");
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            Instantiate(Effects[0], transform.position, Quaternion.identity);

            scoreManager.GetComponent<ScoreManager>().Death();

            Destroy(gameObject);
        }
        if (collision.collider.tag == "Wall")
        {
            soundManager.GetComponent<SoundManager>().Planet(2);
        }
    }
    void SetOrbitPosition(Transform anchorPoint)
    {
        if (anchorPoint != null)
        {
            // ������������ �������� ������ �� ���� � ��������, ����� �������� ������ � ���������
            Vector2 orbitOffset = new Vector2(Mathf.Cos(orbitAngle), Mathf.Sin(orbitAngle)) * orbitRadius;
            transform.position = (Vector2)anchorPoint.position + orbitOffset;

            // ������� ��� ��������
            Debug.Log($"Player positioned at {transform.position} based on orbit angle {orbitAngle * Mathf.Rad2Deg} degrees.");
        }
    }

    private void StartOrbitFromContactPoint(Transform anchorPoint)
    {
        // ��������� ������ �� ������ ������� � ������ � ������ ��������
        Vector2 directionToPlayer = (transform.position - anchorPoint.position).normalized;

        // ������� ��������� ���� ������ ��� ����� �������� �������
        orbitAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);

        // ������������� ������� ������ �� ������, ��������� ��������� ����
        SetOrbitPosition(anchorPoint);

        rb.isKinematic = true;
        isFlying = false;
    }
}
