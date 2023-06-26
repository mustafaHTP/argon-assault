using UnityEngine;

public class AmbienceMusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numberOfMusicPlayers = FindObjectsOfType<AmbienceMusicPlayer>().Length;
        if (numberOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
