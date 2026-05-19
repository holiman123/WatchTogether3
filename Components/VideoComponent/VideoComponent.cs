using Microsoft.AspNetCore.Components;
using WatchTogether3.Data;

namespace WatchTogether3.Components.VideoComponent;

public delegate void VideoActionHandler(VideoComponent videoComponent, double time = 0);

public enum UserActions2
{
    ProceedVideo,
    PauseVideo
}

public interface VideoComponent
{
    [Parameter]
    public VideoFile? Video { get; set; }

    [Parameter]
    public bool DoShowControls { get; set; }


    public event VideoActionHandler OnVideoProceed;
    public event VideoActionHandler OnVideoPause;

    public void Proceed();
    public void Pause();
    public void Seek(double time);
    public void ClearSource();
    public void InitComponent();
}
