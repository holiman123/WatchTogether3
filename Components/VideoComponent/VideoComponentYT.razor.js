console.log('isolated JS');

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

// initAPI();

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


/*function initAPI() {
    if (typeof YT == 'undefined') {
        console.log('init youtube api');
        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        var headTag = document.getElementsByTagName('head')[0];
        headTag.append(tag);
        // var firstScriptTag = document.getElementsByTagName('script')[0];
        // firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
        //var ScriptTag = document.getElementById('youtube_api_sctipt_div');
        //ScriptTag.append(tag);
        console.log('init youtube api finished');
    }
}*/

var player;
export function onYouTubeIframeAPIReady() {
    console.log('create player');
    console.log(player);
    try {
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
    }
    catch (error) {
        console.log(error);
    }
    console.log("player created");
}

export function onPlayerReady(event) {
    // Request youtube tag
    dnr.invokeMethodAsync("LoadYtTag");
}

export function LoadVideo(tag) {
    player.cueVideoById(tag);
}

var prevEventPlayerTime = null;
var prevEvents = [];
export function onPlayerStateChange(event) {
    prevEventPlayerTime = player.getCurrentTime();
    for (let i = 2; i >= 0; i--) {
        prevEvents[i + 1] = prevEvents[i];
    }
    prevEvents[0] = event.data;
    // console.log("State changed: " + prevEvents);


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
    if (player.getPlayerState() != 1) {
        DoSendAction = false;
        player.playVideo();
    }
}
export function PauseVideo() {
    if (player.getPlayerState() != 2) {
        DoSendAction = false;
        player.pauseVideo();
    }
}
export function SeekVideo(time) {
    // If the video is paused do not set DoSendAction to false,
    // because that would cause next event to be ignored.
    var state = player.getPlayerState();
    if (state != 2 && state != -1 && state != 5) {
        DoSendAction = false;
    }
    player.seekTo(time, true);
    console.log('seek youtube');
}


export function SendAction(action, ...args) {
    if (dnr == null || DoSendAction == false) {
        DoSendAction = true;
        return;
    }

    //console.log("Sending action: " + action + " with args: " + args);
    dnr.invokeMethodAsync('ReceiveAction', action, args);
}