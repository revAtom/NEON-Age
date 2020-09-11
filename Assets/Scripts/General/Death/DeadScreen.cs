using Sirenix.OdinInspector;
using UnityEngine;

public class DeadScreen : MonoBehaviour {

    [BoxGroup("DeathUI")]
    [GUIColor(.6f, .75f, .56f)]
    public GameObject player;

    [BoxGroup("DeathUI")]
    [GUIColor(.6f, .75f, .5f)]
    public GameObject deathScreen;
    public void DestroyPlayer()
    {
        Destroy(player);
    }
    public void OpenDeathMenu()
    {
        deathScreen.SetActive(true);
    }
}
