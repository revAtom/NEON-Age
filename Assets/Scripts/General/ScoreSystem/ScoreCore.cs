using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScoreCore : MonoBehaviour
{
    [BoxGroup("ScoreText")]
    [GUIColor(.3f, .8f, .6f)]
    public TMP_Text scoreText, hightScoreText, missileDestroyedText, maxMissileDestroyedText;

    int hightScore, timeValue, missileDestroyed, maxMissileDestroyed;

    public static ScoreCore instance;

    [BoxGroup("General")]
    [GUIColor(.6f, .5f, .8f)]
    public bool isPlaying = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        missileDestroyed = PlayerPrefs.GetInt("MissileDestroyed");
        maxMissileDestroyed = PlayerPrefs.GetInt("MaxMissileDestroyed");

        hightScore = PlayerPrefs.GetInt("HightScore");
    }
    private void Update()
    {
        if (isPlaying)
        {
            timeValue = (int)Time.time;
        }
        else
        {
            scoreText.text ="Score: " + timeValue.ToString();
            hightScoreText.text ="Record Score: " + hightScore.ToString();

            missileDestroyedText.text = missileDestroyed.ToString() + " Missile destroyed";
            maxMissileDestroyedText.text = maxMissileDestroyed.ToString() + " Record missile destroyed";
        }

        if (hightScore <= timeValue)
        {
            hightScore = timeValue;

            PlayerPrefs.SetInt("HightScore", hightScore);
        }
        if(maxMissileDestroyed > missileDestroyed)
        {
            maxMissileDestroyed = missileDestroyed;

            PlayerPrefs.SetInt("MaxMissileDestroyed", maxMissileDestroyed);
        }
    }

    [Button]
    void DelleteAll()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("DELETED");
    }
}
