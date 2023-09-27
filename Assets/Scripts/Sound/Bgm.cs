using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
        [SerializeField] Sound soundManager;
        [SerializeField] AudioClip clip;

        void Start()
        {
            soundManager.PlayBgm(clip);
        }

}
