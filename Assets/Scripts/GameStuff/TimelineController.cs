using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public BoolValue playHouseTimeline;

    void OnEnable()
    {
        if(playHouseTimeline.value)
        {
            playableDirector.Play();
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        playHouseTimeline.value = false;
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
