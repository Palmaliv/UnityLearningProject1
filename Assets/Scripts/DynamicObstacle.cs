using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetVector;
    [Min(0.01f)][SerializeField] private float _period = 1f;
    private Vector3 _startingPosition;

    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float rawSine = Mathf.Sin(Time.time / _period);
        float sineWave =  rawSine / 2 + 0.5f;

        transform.position = _startingPosition + _offsetVector * sineWave;
    }
}
