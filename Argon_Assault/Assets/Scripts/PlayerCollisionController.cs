using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Special Effects")]
    [SerializeField] AudioClip _crashSfx;
    [SerializeField] ParticleSystem _crashVfx;

    [Header("Player Controller Script")]
    [SerializeField] PlayerController _playerController;

    [Header("Health Bar")]
    [SerializeField] GameObject _healthBar;

    [Header("Boosters")]
    [SerializeField] GameObject[] _boosters;

    [HideInInspector] public int PlayerHealth;

    AudioSource _audioSource;
    float _sceneLoadDelayAmount = 1f;
    int _maxHealth = 100;

    private void Start()
    {
        PlayerHealth = _maxHealth;
        _healthBar.GetComponent<HealthBarController>().SetMaxHealth(PlayerHealth);
        _healthBar.GetComponent<HealthBarController>().SetHealth(PlayerHealth);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    public void StartCrashSequence()
    {
        _playerController.enabled = false;

        //crash effect
        _crashVfx.Play();
        _audioSource.PlayOneShot(_crashSfx);

        DisableBoostersOnCrash();
        DisablePlayerShip();
        Invoke(nameof(ReloadLevel), _sceneLoadDelayAmount);
    }

    private void DisableBoostersOnCrash()
    {
        foreach (var item in _boosters)
        {
            item.SetActive(false);
        }
    }

    private void DisablePlayerShip()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i).gameObject;

            var childMeshRenderer = child.GetComponent<MeshRenderer>();
            if (childMeshRenderer != null)
            {
                childMeshRenderer.enabled = false;
            }

            var childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                childCollider.enabled = false;
            }
        }
    }

    void ReloadLevel()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

}
