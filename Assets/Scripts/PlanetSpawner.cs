using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private Vector2 spawnXRange = new Vector2(-2f,2f);
    [SerializeField] private float deleteDistance = 10f; // ћаксимальное рассто€ние ниже игрока дл€ удалени€ планет

    private float spawnDistance = 8f;
    private Queue<GameObject> activePlanets = new Queue<GameObject>();
    private Transform playerTransform;
    private GameObject lastSpawnedPlanet;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;

        // —павним первую планету на фиксированном рассто€нии от игрока
        spawnDistance = Random.Range(7,8);
        SpawnPlanet(playerTransform.position.y + spawnDistance);
    }

    private void OnEnable()
    {
        Player.OnPlanetTouched += HandlePlanetTouched;
    }

    private void OnDisable()
    {
        Player.OnPlanetTouched -= HandlePlanetTouched;
    }

    private void HandlePlanetTouched()
    {
        // —павним следующую планету на фиксированном рассто€нии от предыдущей
        SpawnPlanet(lastSpawnedPlanet.transform.position.y + spawnDistance);

        // ѕровер€ем и удал€ем старые планеты
        DeleteOldPlanets();
    }

    private void SpawnPlanet(float spawnY)
    {
        float spawnX = Random.Range(spawnXRange.x, spawnXRange.y);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        GameObject newPlanet = Instantiate(planetPrefabs[Random.Range(0, planetPrefabs.Length)], spawnPosition, Quaternion.identity);
        activePlanets.Enqueue(newPlanet);
        lastSpawnedPlanet = newPlanet;
    }

    private void DeleteOldPlanets()
    {
        float deleteThreshold = playerTransform.position.y - deleteDistance;

        // ”дал€ем планеты, которые опустились ниже игрока на `deleteDistance`
        while (activePlanets.Count > 0 && activePlanets.Peek().transform.position.y < deleteThreshold)
        {
            Destroy(activePlanets.Dequeue());
        }
    }
}
