namespace Ilumisoft.VolumeControl.Core
{
    using UnityEngine;
    using UnityEngine.Audio;

    [System.Serializable]
    public class VolumeChannel
    {
        [System.NonSerialized]
        private AudioMixer audioMixer;

        private string exposedParameterName;
        private string playerPrefsKey;

        [SerializeField]
        private bool isMuted = false;

        [SerializeField]
        private float volume = 1.0f;

        /// <summary>
        /// Gets or sets the muting
        /// </summary>
        public bool IsMuted
        {
            get => this.isMuted;
            set
            {
                this.isMuted = value;
                ApplyMuting(value);
            }
        }

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume
        {
            get => this.volume;

            set
            {
                this.volume = value;
                ApplyVolume(value);
            }
        }

        /// <summary>
        /// Creates a new VolumeChannel instance
        /// </summary>
        /// <param name="audioMixer">The AudioMixer used by Volume Control</param>
        /// <param name="exposedParameterName">The name of the exposed volume parameter</param>
        /// <param name="playerPrefsKey">The PlayerPrefs key used to store the settings of the channel</param>
        public VolumeChannel(AudioMixer audioMixer, string exposedParameterName, string playerPrefsKey)
        {
            this.audioMixer = audioMixer;
            this.exposedParameterName = exposedParameterName;
            this.playerPrefsKey = playerPrefsKey;
        }

        /// <summary>
        /// Applies the muting to the AudioMixer 
        /// </summary>
        /// <param name="value"></param>
        private void ApplyMuting(bool value)
        {
            if (this.isMuted)
            {
                ApplyVolume(0.0f);
            }
            else
            {
                ApplyVolume(this.volume);
            }
        }

        /// <summary>
        /// Applies the given volume to the AudioMixer
        /// </summary>
        /// <param name="volume"></param>
        private void ApplyVolume(float volume)
        {
            //If the channel is muted, volume is set to zero
            if(this.IsMuted)
            {
                volume = 0.0f;
            }

            //Convert volume to decibel
            float dBValue = Utils.ConvertToDecibel(volume);

            //Set volume
            this.audioMixer.SetFloat(this.exposedParameterName, dBValue);      
        }

        /// <summary>
        /// Serializes the settings and stores them in the PlayerPrefs
        /// </summary>
        public void SaveToPlayerPrefs()
        {
            string value = JsonUtility.ToJson(this);

            PlayerPrefs.SetString(this.playerPrefsKey, value);

            PlayerPrefs.Save();
        }

        /// <summary>
        /// Loads the settings from the PlayerPrefs
        /// </summary>
        public void LoadFromPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(this.playerPrefsKey))
            {
                string value = PlayerPrefs.GetString(this.playerPrefsKey);

                JsonUtility.FromJsonOverwrite(value, this);

                ApplyVolume(this.Volume);
            }
        }
    }
}