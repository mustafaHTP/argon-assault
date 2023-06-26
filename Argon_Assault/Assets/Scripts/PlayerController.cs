using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] InputAction _movement;
    [SerializeField] InputAction _fire;
    [SerializeField] InputAction _quitGame;
    [SerializeField] InputAction _returnMainMenu;
    [SerializeField] InputAction _reloadScene;

    [Header("Laser Gun Particles")]
    [Tooltip("Add 2 laser particles")]
    [SerializeField] GameObject[] _lasers;

    [Header("Fire Sound Effect")]
    [SerializeField] AudioClip _fireWeaponClip;

    [Header("Movement Speed")]
    [Tooltip("How fast spaceship moves based upon player input")]
    [SerializeField] float _movementSpeed;

    [Header("Rotation Speed Based On Position")]
    [SerializeField] float _pitchSpeed;
    [SerializeField] float _yawSpeed;

    [Header("Rotation Speed Based On Input")]
    [SerializeField] float _pitchControlSpeed;
    [SerializeField] float _rollControlSpeed;

    [Header("Movement Offsets")]
    [SerializeField] float _horizontalMoveUpperLimit = 10f;
    [SerializeField] float _horizontalMoveLowerLimit = -10f;
    [SerializeField] float _verticalMoveUpperLimit = 10f;
    [SerializeField] float _verticalMoveLowerLimit = -3f;

    Vector2 _movementInput;
    Vector2 _movementInputSmoothVelocity;
    Vector2 _smoothedMovementInput;
    const float _smoothInputSpeed = 0.1f;
    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _movement.Enable();
        _fire.Enable();
        _quitGame.Enable();
        _returnMainMenu.Enable();
        _reloadScene.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _fire.Disable();
        _quitGame.Enable();
        _returnMainMenu.Enable();
        _reloadScene.Enable();
    }

    void Update()
    {
        SmoothInput();
        ProcessTransform();
        ProcessRotation();
        ProcessFire();
        ProcessDebugKeys();
    }

    private void ProcessDebugKeys()
    {
        if (_quitGame.IsPressed())
        {
            Application.Quit();
        }
        else if (_reloadScene.IsPressed())
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else if (_returnMainMenu.IsPressed())
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
    }

    private void ProcessFire()
    {
        if (_fire.IsPressed())
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
            SetLasersActive(true);
        }
        else
        {
            _audioSource.Stop();
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (var item in _lasers)
        {
            var laserEmission = item.GetComponent<ParticleSystem>().emission;
            laserEmission.enabled = isActive;
        }
    }

    /// <summary>
    /// Prevents input stutter in new input system
    /// 
    /// </summary>
    private void SmoothInput()
    {
        _movementInput = _movement.ReadValue<Vector2>();
        _smoothedMovementInput =
            Vector2.SmoothDamp(_smoothedMovementInput, _movementInput,
            ref _movementInputSmoothVelocity, _smoothInputSpeed);
    }

    private void ProcessTransform()
    {
        float moveAmountX = _smoothedMovementInput.x * Time.deltaTime * _movementSpeed;
        float newXPosition = transform.localPosition.x + moveAmountX;
        float clampedXPosition =
            Mathf.Clamp(newXPosition, _horizontalMoveLowerLimit, _horizontalMoveUpperLimit);

        float moveAmountY = _smoothedMovementInput.y * Time.deltaTime * _movementSpeed;
        float newYPosition = transform.localPosition.y + moveAmountY;
        float clampedYPosition =
            Mathf.Clamp(newYPosition, _verticalMoveLowerLimit, _verticalMoveUpperLimit);

        transform.localPosition =
            new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchAmount = transform.localPosition.y * _pitchSpeed + _smoothedMovementInput.y * Time.deltaTime * _pitchControlSpeed;
        float yawAmount = transform.localPosition.x * _yawSpeed;
        //Due to flickering on rolling, removed frame independent roll
        float rollAmount = _smoothedMovementInput.x * _rollControlSpeed;

        transform.localRotation = Quaternion.Euler(pitchAmount, yawAmount, rollAmount);
    }

}
