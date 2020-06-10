using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
    private Camera _mainCamera;

    private int _cameraMode = 1;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _mainCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        var playerPosition = _player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, -1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _cameraMode = CycleThroughModes(_cameraMode);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _cameraMode = CycleThroughModes(_cameraMode, true);
        }
        switch (_cameraMode)
        {
            case 0:
                _mainCamera.orthographicSize = 1;
                break;
            case 1:
                _mainCamera.orthographicSize = 3;
                break;
            case 2:
                _mainCamera.orthographicSize = 6;
                break;
            case 3:
                _mainCamera.orthographicSize = 9;
                break;
        }
    }

    private static int CycleThroughModes(int currentMode, bool reverse = false)
    {
        if (reverse) currentMode--;
        else currentMode++;

        if (currentMode > 3)
        {
            currentMode = 0;
        }
        else if(currentMode < 0)
        {
            currentMode = 3;
        }

        return currentMode;
    }
}
