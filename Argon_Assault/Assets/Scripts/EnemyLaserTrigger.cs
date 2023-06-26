using UnityEngine;

public class EnemyLaserTrigger : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] int _damagePerHit = 5;

    [SerializeField] GameObject _healthBar;

    PlayerCollisionController _playerCollisionController;

    private void Start()
    {
        _playerCollisionController = _player.GetComponent<PlayerCollisionController>();
        if (_playerCollisionController is null)
        {
            Debug.LogError("Player Collision Controller not found...");
        }
    }

    private void OnParticleTrigger()
    {
        DamagePlayer();
    }

    private void DamagePlayer()
    {
        ApplyDamage();

        Debug.Log("Health: " + _playerCollisionController.PlayerHealth);
        if (_playerCollisionController.PlayerHealth < 0)
        {
            KillPlayer();
        }
    }

    private void ApplyDamage()
    {
        int playerHealth = _playerCollisionController.PlayerHealth;
        playerHealth -= _damagePerHit;

        // Min health must be '0'
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }

        _healthBar.GetComponent<HealthBarController>().SetHealth(playerHealth);
        _playerCollisionController.PlayerHealth = playerHealth;

    }

    private void KillPlayer()
    {
        _playerCollisionController.StartCrashSequence();
    }

}
