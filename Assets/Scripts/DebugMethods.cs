using Player;
using UnityEngine;

public class DebugMethods : MonoBehaviour
{
    private PlayerValues _playerValues;

    private void Start()
    {
        _playerValues = FindObjectOfType<PlayerValues>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            _playerValues.Hp = 0;
        }   
    }
}
