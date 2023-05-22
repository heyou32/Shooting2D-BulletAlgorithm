using UnityEngine;

public class PlayerGridPosition : MonoBehaviour
{
    public bool occupied;
    void Awake()
    {
        _position = transform.transform.localPosition;
    }

    void Update()
    {
        if (PlayerGrid.instance != null) _Update();
    }

    void _Update()
    {
        transform.localPosition += new Vector3(_position.x * -_direction, _direction, 0) * PlayerGrid.GetDeviationSpeed() * Time.deltaTime;

        if (transform.localPosition.y < _position.y - PlayerGrid.GetDeviationRange())
        {
            _direction = -_direction;
        }
        else if (transform.localPosition.y > _position.y)
        {
            transform.localPosition = _position;

            _direction = -_direction;
        }
    }

    int _direction = -1;
    Vector3 _position;
}