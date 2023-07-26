using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Audio
{
    public class WaterSound : MonoBehaviour {
 
        public List<AudioSource> waterSounds;
        public AudioSource closestWaterSound;
        public float waterVolume = 1f;
        private GameObject player;
        public float volumeReductionSpeed = .5f;
 
        void Start () {
            //Find all of the Water AudioSources 
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("WaterSound")) {
                waterSounds.Add(obj.GetComponent<AudioSource>());
            }
 
            player = FindObjectOfType<PlayerControl>().gameObject;      
        }
     
        void Update () {
            DetermineClosestWaterSound();       
        }
 
        void DetermineClosestWaterSound() {
            float distance = float.MaxValue;
            closestWaterSound = null;
            //Determine which water sound is the closest
            foreach(AudioSource waterSound in waterSounds)
            {
                waterSound.volume = PlayerPrefs.GetFloat("GlobalVolume");
                float newDistance = Vector3.Distance(player.transform.position, waterSound.transform.position);
                if (newDistance < distance) {
                    distance = newDistance;
                    closestWaterSound = waterSound;
                }
            }
 
            DisableDistantWaterSounds(closestWaterSound);
        }
 
        //Scale up the closest water sound's volume until it's reached 1.
        //Scale down the volume of any water sound that isn't closest.
        void DisableDistantWaterSounds(AudioSource closestWaterSource) {
            foreach(AudioSource waterSound in waterSounds) {
                if(waterSound != closestWaterSource) {
                    waterSound.volume -= volumeReductionSpeed * Time.deltaTime;
                } else {
                    waterSound.volume += volumeReductionSpeed * Time.deltaTime;
                }
            }
        }
    }
}