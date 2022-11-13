using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private WaveInfo[] _waveInfo;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _timeToSpawn;

    [SerializeField] private float _minRadius, _maxRadius;
    private List<Enemy> _enemies = new List<Enemy>();
    private float _timer;
    private int _allEnemyCount;
    public int KilledEnemyCount { get; private set; }

    private void Start() {
        foreach (var wave in _waveInfo) {
            foreach (var waveCreatureInfo in wave.Creatures) {
                waveCreatureInfo.Init();
                _allEnemyCount += waveCreatureInfo.Count;
            }
        }
    }

    private void Update() {
        if (!GameManager.Instance.IsPlaying) return;

        _timer += Time.deltaTime;
        if (_timer > _timeToSpawn) {
            _timer = 0;
            
            //CreateEnemy(_enemyPrefab);
        }
        foreach (var wave in _waveInfo) {
            foreach (var waveCreatureInfo in wave.Creatures) {
                waveCreatureInfo.UpdateCall(Time.deltaTime);
            }
        }
        if(KilledEnemyCount >= _allEnemyCount) {
            GameManager.Instance.WinGame();
        }
    }

    private void CreateEnemy(Enemy enemy) {
        Vector2 randomVector = Random.insideUnitCircle;
        Vector2 randomPoint = randomVector.normalized * Random.Range(_minRadius, _maxRadius);

        Vector3 spawnPosition = _playerTransform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
        Enemy newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        newEnemy.Setup(_playerTransform, this);
        _enemies.Add(newEnemy);
    }

    public void Spawn(Enemy enemy) {
        CreateEnemy(enemy);
    }

    private void OnDrawGizmos() {
        if (_playerTransform) {
            Handles.color = Color.red;
            Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _minRadius);
            Handles.color = Color.white;
            Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _maxRadius);
        }
    }

    public void CallDie(Enemy enemy) {
        if (_enemies.Contains(enemy)) {
            _enemies.Remove(enemy);
            KilledEnemyCount++;
        }
    }

    public Enemy[] GetNearestEnemyTransform(Vector3 point, int number) {
        _enemies = _enemies.OrderBy(enemy => Vector3.Distance(point, enemy.transform.position)).ToList();

        int minCount = Mathf.Min(number, _enemies.Count);
        Enemy[] enemies = new Enemy[minCount];

        for (int i = 0; i < minCount; i++) {
            enemies[i] = _enemies[i];
        }

        return enemies;
    }

    //public Enemy[] GetNearestEnemyTransform(Vector3 point) {
    //    //_enemies = _enemies.OrderBy(enemy => Vector3.Distance(point, enemy.transform.position)).ToList();

    //    for (int i = 0; i < _enemies.Count; i++) {
    //        float minDistance = Vector3.Distance(point, _enemies[i].transform.position);
    //        for (int j = 0; j < _enemies.Count; j++) {
    //            float newDistance = Vector3.Distance(point, _enemies[j].transform.position);
    //            if (newDistance < minDistance) {
    //                minDistance = newDistance;
    //                Enemy enemy = _enemies[i];
    //                _enemies[i] = _enemies[j];
    //                _enemies[j] = enemy;
    //            }
    //        }
    //    }

    //    int minCount = Mathf.Min(1, _enemies.Count);
    //    Enemy[] enemies = new Enemy[minCount];

    //    for (int i = 0; i < minCount; i++) {
    //        enemies[i] = _enemies[i];
    //    }

    //    return _enemies.ToArray();
    //}
}