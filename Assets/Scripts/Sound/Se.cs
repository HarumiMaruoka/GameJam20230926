using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Se : MonoBehaviour
{

    [SerializeField] Sound soundManager;
    [SerializeField] AudioClip clip;
    [SerializeField, Min(0)] float waitTime = 0.5f;

    bool isPlaying = false;

    void Start()
    {
    }

    void Update()
    {
        if (isPlaying)
        {
            return;
        }

        StartCoroutine(nameof(SeTimer));
    }

    IEnumerator SeTimer()
    {
        isPlaying = true;

        yield return new WaitForSeconds(waitTime);

        soundManager.PlaySe(clip);

        isPlaying = false;
    }

}
