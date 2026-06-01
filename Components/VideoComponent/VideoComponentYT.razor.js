console.log('Loading isolated JS of YT player');

var UserActions = Object.freeze({
    ProceedVideo: 0,
    PauseVideo: 1
});

var DoSendAction = true;

var dnr = null;
export function setDotnetReference(dotnetRef) {
    console.log("received DNR");
    dnr = dotnetRef;
}

var player;
export function CreateYtPlayer() {
    player = new YT.Player('player_div', {
        height: '390',
        width: '640',
        playerVars: {
            'playsinline': 1,
            'color': 'white'
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
    console.log('YT player created');
}

export function onPlayerReady(event) {
    console.log("Player ready");
    // Request youtube tag
    dnr.invokeMethodAsync("LoadYtTag");
}

export function LoadVideo(id) {
    player.cueVideoById(id);
    console.log("Video loaded: " + id);
}

var prevEventPlayerTime = null;
var prevEvents = [];
export function onPlayerStateChange(event) {
    prevEventPlayerTime = player.getCurrentTime();
    for (let i = 2; i >= 0; i--) {
        prevEvents[i + 1] = prevEvents[i];
    }
    prevEvents[0] = event.data;
    console.log("State changed: " + prevEvents);


    if (prevEvents[0] == 1 &&
        prevEvents[1] == 3 &&
        prevEvents[3] != 5) {
        SendAction(UserActions.ProceedVideo, prevEventPlayerTime);
    }
    else if (prevEvents[0] == YT.PlayerState.PAUSED) {
        SendAction(UserActions.PauseVideo, player.getCurrentTime());
    }
    else if (
        prevEvents[0] == 1 &&
        prevEvents[1] == 3 &&
        prevEvents[2] == -1 &&
        prevEvents[3] == 5) {
        dnr.invokeMethodAsync("InitVideo");
    }
}


export function ClearVideo() {
    player.stopVideo();
    player.clearVideo();
}

export function ProceedVideo() {
    var state = player.getPlayerState();

    if (state == 5 || state == 1)
        return;

    DoSendAction = false;
    player.playVideo();
}
export function PauseVideo() {
    var state = player.getPlayerState();

    if (state == 5 || state == 2)
        return;

    DoSendAction = false;
    player.pauseVideo();
}
export function SeekVideo(time) {
    // If the video is paused do not set DoSendAction to false,
    // because that would cause next event to be ignored.
    var state = player.getPlayerState();

    if (state == 5)
        return;

    if (state != 2 && state != -1) {
        DoSendAction = false;
    }
    player.seekTo(time, true);
}


export function SendAction(action, ...args) {
    if (dnr == null || DoSendAction == false) {
        DoSendAction = true;
        return;
    }

    console.log("Sending action: " + action + " with args: " + args);
    dnr.invokeMethodAsync('ReceiveAction', action, args);
}