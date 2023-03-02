using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private float _numberOfWaypoints = 5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _waitTimeAtWaypoint = 2f;
    [SerializeField] private Transform _point;

    private Transform[] _wayPoints;
    private Transform _start;
    private Transform _end;
    private float _time;
    private float _distance;
    private int _currentWayPointIndex = 1;
    private float _duration;
    private float _waitTimer;



    private void Awake()
    {
        _wayPoints = new Transform[(int)_numberOfWaypoints];
        for (var i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = Instantiate(_point);
            _wayPoints[i].transform.position = new Vector3(Random.Range(-15, 16), 0.5f, Random.Range(-15, 16));
        }

        _start = _wayPoints[0];
        _end = _wayPoints[1];
    }

    private void Update()
    {
        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
            return;
        }
        _time += Time.deltaTime;
        var progress = _time / _duration;
        var newPosition = Vector3.Lerp(_start.position, _end.position, progress);
        transform.position = newPosition;
        if (newPosition == _end.position)
        {
            ChangePoint();
            SetDuration();
            _waitTimer = _waitTimeAtWaypoint;
        }
    }

    private void SetDuration()
    {
        var distance = Vector3.Distance(_start.position, _end.position);
        _duration = distance / _speed;
    }

    private void ChangePoint()
    {
        _time = 0f;
        _start = _wayPoints[_currentWayPointIndex];
        _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
        _end = _wayPoints[_currentWayPointIndex];
    }
}
