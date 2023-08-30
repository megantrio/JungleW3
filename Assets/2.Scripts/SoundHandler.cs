using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public void PlayEvent(string _eventName)
    {
        AkSoundEngine.PostEvent(_eventName, this.gameObject);
    }
}
