using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    //TODO
    [SerializeField]
    private AudioSource _cameraAudioSource;

    [SerializeField]
    private GameObject _audioSourcePrefab;

    [SerializeField]
    private int _audioSourcePoolSize;

    private List<AudioSource> _audioSourcePool;


    private void Awake()
    {
        _audioSourcePool = new List<AudioSource>(_audioSourcePoolSize);
        for (int i = 0; i < _audioSourcePoolSize; i++)
        {
            AudioSource instance = Instantiate(_audioSourcePrefab).GetComponent<AudioSource>();
            _audioSourcePool.Add(instance);
            instance.gameObject.SetActive(false);
        }
    }


    public void PlayAtPosition(Vector3 targetPos, AudioClip clip)
    {
        AudioSource pooledSource = _audioSourcePool.FirstOrDefault(source => !source.gameObject.activeInHierarchy);

        if (pooledSource != null)
        {
            StartCoroutine(ClipPlayingRoutine(targetPos, clip, pooledSource));
        }
    }


    private IEnumerator ClipPlayingRoutine(Vector3 targetPos, AudioClip clip, AudioSource pooledSource)
    {
        pooledSource.transform.position = targetPos;
        pooledSource.clip = clip;
        pooledSource.gameObject.SetActive(true);
        pooledSource.Play();

        while (pooledSource.isPlaying)
        {
            yield return null;
        }

        pooledSource.gameObject.SetActive(false);
    }
}