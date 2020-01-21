using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private EnemySpawn spawnManager = null;
    [SerializeField] private Text scoreText = null;

    private float _score = 0;

    private void Awake()
    {
        spawnManager.OnDestroyEnemy = OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(EnemyData destroyedEnemy)
    {
        _score += destroyedEnemy.HpPoints;
        scoreText.text = $"{_score}";
    }
}