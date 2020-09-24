using Sirenix.OdinInspector;
using UnityEngine;

public class DeadCore : MonoBehaviour
{
    [BoxGroup("Particle")]
    [GUIColor(.6f, .7f, .5f)]
    public GameObject particleObj;

    [BoxGroup("Camera")]
    [GUIColor(.5f, .5f, .5f)]
    public Animator cameraAnim;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            if (collision.gameObject.layer == 11)
                Destroy(collision.gameObject);

            #region DeathProcess

            if (cameraAnim != null)
            {
                cameraAnim.SetTrigger("IsShaking");
            }
            else
            {
                ScoreCore.instance.RecountMissiles();
                SpawnCore.instance.enemyCount--;

                Destroy(this.gameObject, 1f);
            }
            GameObject particle = Instantiate(particleObj, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));

            Destroy(particle, 1f);
        }
        #endregion
    }
}
  

