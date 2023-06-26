using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float _destructionDelayAmount = 3f;
    void Start()
    {
        Destroy(gameObject, _destructionDelayAmount);
    }
}
