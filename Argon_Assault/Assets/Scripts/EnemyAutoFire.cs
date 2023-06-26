using UnityEngine;

public class EnemyAutoFire : MonoBehaviour
{
    [SerializeField] GameObject[] _lasers;

    void Start()
    {
        ActivateLasers();
    }

    void ActivateLasers()
    {
        foreach (GameObject laser in _lasers)
        {
            laser.GetComponent<ParticleSystem>().Play();
        }
    }
}
