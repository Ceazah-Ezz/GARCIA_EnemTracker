using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

//Generic class that handles the array/list data
public class EnemList<T> where T : UnityEngine.Object //Enemy list yay
{
    private List<T> enemyList = new List<T>();

    public void EnemSpawn(T enem) 
    {
        enemyList.Add(enem);
        Debug.Log($"Enemy added! Total count: {enemyList.Count}");
    }

    public void EnemRemove()
    {
        if (enemyList.Count > 0)
        {
            T Lastenem = enemyList[enemyList.Count - 1];
            enemyList.RemoveAt(enemyList.Count - 1);
            Object.Destroy(Lastenem);
            Debug.Log($"Removed enemy! Total count: {enemyList.Count}");
        }
    }

    public void ClearAll()
    {
        foreach ( T enem in enemyList )
        {
            if ( enem != null )
            {
                Object.Destroy(enem);
            }
        }
        enemyList.Clear();
        Debug.Log("Enemies wiped out!");
    }

    public List<T> GetAllItems() //Gives list safely without corruption
    {
        return enemyList;
    }

    public int Amnt => enemyList.Count; //Used for counting enemies
}

public class EnemTracker : MonoBehaviour
{
    [Header ("Properties")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private Transform enemSpawnpoint;
    public GameObject enemPrefab;
    private EnemList<GameObject> enemyList;

    void Awake()
    {
        enemyList = new EnemList<GameObject>();
    }

    private void Update()
    {
        counterText.text = $"Current enemies: {enemyList.Amnt}"; //Always updates enemy counter
    }

    //Methods are added in order for the buttons to function properly
    //This is for organization purposes
    public void AddEnemy()
    {
        if (enemPrefab != null && enemSpawnpoint != null)
        {
            GameObject Spawn = Instantiate(enemPrefab, enemSpawnpoint.position, enemSpawnpoint.rotation);
            enemyList.EnemSpawn(Spawn);
        }
    }

    public void RemoveEnemy()
    {
        enemyList.EnemRemove();
    }

    public void ClearAllEnemies()
    {
        enemyList.ClearAll();
    }
}
