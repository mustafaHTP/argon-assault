using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    [Header("Crash VFX")]
    [SerializeField] GameObject _hitVfx;
    [SerializeField] GameObject _crashFx;

    [Header("Score and Hitpoint")]
    [SerializeField] int _hitpoint = 30;
    [SerializeField] int _scorePerHit = 5;

    GameObject _vfxParentGameObject;
    Scorer _scorer;

    private void Start()
    {
        AddRigidBody();
        _vfxParentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        if (_vfxParentGameObject is null) Debug.Log("_vfxParent Not Found...");
        _scorer = FindObjectOfType<Scorer>();
    }

    private void AddRigidBody()
    {
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (_hitpoint <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        _scorer.IncreaseScore(_scorePerHit);
        _hitpoint -= _scorePerHit;
        var _hitVfxClone = Instantiate(_hitVfx, transform.position, Quaternion.identity);
        _hitVfxClone.transform.parent = _vfxParentGameObject.transform;
    }

    private void KillEnemy()
    {
        var _crashFxClone = Instantiate(_crashFx, transform.position, Quaternion.identity);
        _crashFxClone.transform.parent = _vfxParentGameObject.transform;
        Destroy(gameObject);
    }

}
