using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class NPCAnimController : MonoBehaviour
{
    enum Animations { WashingClothes, Talking, Crying, Begging, Puking, ArmsCrossed, Writing, FixingCart}
    [SerializeField] Animations animations;

    public List<AudioClip> clipsToPlay;
    public float clipVolume = 1f;
    public float sfxCooldown = 5f;
    private float sfxCooldownTimer = 0;
    Animator anim;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.spatialBlend = 1;
        _audioSource.maxDistance = 6;
        _audioSource.minDistance = 1;
        _audioSource.volume = clipVolume;
        anim.Play(0, -1, Random.value);
    }


    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("AnimIndex", (int)animations);
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
    }

    private void Update()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("GlobalVolume");
        sfxCooldownTimer += Time.deltaTime;
        if (animations == Animations.Talking && !_audioSource.isPlaying)
        {
            if (clipsToPlay.Count > 0 && sfxCooldownTimer>sfxCooldown)
            {
                _audioSource.clip = clipsToPlay[Random.Range(0, clipsToPlay.Count)];
                _audioSource.Play();
                sfxCooldownTimer = 0;
            }
        }
    }
}
