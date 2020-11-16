using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LevelTransition : MonoBehaviour
{
    public int levelIndex;

    public bool fadeScreen = true;
    public float fadeTime = 0.3f;
    public Color fadeColour = Color.black;

    public void TransitionEventTrigger()
    {
        StartCoroutine(TransitionToLevel(levelIndex));
    }

    private IEnumerator TransitionToLevel(int levelIndex)
    {
        if (fadeScreen)
        {
            SteamVR_Fade.Start(Color.clear, 0);

            Color tColor = fadeColour;
            SteamVR_Fade.Start(tColor, fadeTime);
        }
        yield return new WaitForSeconds(fadeTime + 0.1f);
        
        if (levelIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            //Debug.Log("Restarting this level");
            //SceneManager.LoadScene(levelIndex);
        }

    }
}
