console.log('Loading isolated JS of file player');

var UserActions = Object.freeze({
    ProceedVideo: 0,
    PauseVideo: 1
});

var DoSendAction = true;

var video;
var play_btn = document.getElementById("play_btn");

// DotNet reference holder
var dnh = null;
export function setDotnetReference(dotnetRef) {
    console.log("received DNR");
    dnh = dotnetRef;
}

export function LoadVideo(src) {
    video = document.getElementById("video_comp");

    video.addEventListener("loadedmetadata", function () {
        console.log('Video metadata loaded. Requesting video init');
        RequestVideoInit();
    });

    video.src = src;
    video.load();

    video.addEventListener("play", OnProceedClick);
    video.addEventListener("pause", OnPauseClick);
}

export function RequestVideoInit() {
    if (dnh) {
        dnh.invokeMethodAsync("InitVideo");
        play_btn.style.visibility = "hidden";
    }
}

export function ClearVideo() {
    video.src = "";
    video.load();
}

export function ProceedVideo() {
    if (video.paused) {
        DoSendAction = false;
        video.play();
    }
}

export function PauseVideo() {
    if (!video.paused) {
        DoSendAction = false;
        video.pause();
    }
}

export function SeekVideo(time) {
    DoSendAction = false;
    seekVideo(video, time);
}


export function OnProceedClick() {
    SendAction(UserActions.ProceedVideo, video.currentTime);
}
export function OnPauseClick() {
    SendAction(UserActions.PauseVideo, video.currentTime);
}

function SendAction(action, ...args) {
    if (dnh == null || DoSendAction == false) {
        DoSendAction = true;
        return;
    }

    console.log("Sending action: " + action + " with args: " + args);
    dnh.invokeMethodAsync('ReceiveAction', action, args);
}

export function initVideo(time, isPlay, doShowControls) {
    try {
        console.log("initVideo");

        seekVideo(video, time);

        if (isPlay) {
            DoSendAction = false;
            var promise = video.play();

            if (promise !== undefined) {
                promise.then(_ => {
                    // Autoplay started!
                    console.log("autoplay");
                    if (doShowControls)
                        video.setAttribute("controls", "");
                }).catch(error => {
                    // Autoplay was prevented.
                    // Show a "Play" button so that user can start playback.
                    console.log("no autoplay!");
                    play_btn.style.visibility = "visible";
                });
            }
        }
        else {
            if (doShowControls)
                video.setAttribute("controls", "");
        }
    }
    catch (error) {
        console.log(error);
    }
}


function seekVideo(videoElement, timeInSeconds) {
    if (timeInSeconds > videoElement.duration)
        return false;
    return new Promise((resolve, reject) => {
        if (!(videoElement instanceof HTMLVideoElement)) {
            reject(new Error("Invalid video element."));
        }
        if (typeof timeInSeconds !== "number" || timeInSeconds < 0) {
            reject(new Error("Invalid seek time."));
        }
        if (videoElement.readyState < 1) {
            // Wait until metadata is loaded to know duration
            videoElement.addEventListener("loadedmetadata", () => {
                if (timeInSeconds > videoElement.duration) {
                    reject(new Error("Seek time exceeds video duration."));
                }
                videoElement.currentTime = timeInSeconds;
            }, { once: true });
        } else {
            if (timeInSeconds > videoElement.duration) {
                reject(new Error("Seek time exceeds video duration."));
            }
            videoElement.currentTime = timeInSeconds;
        }

        // Resolve when seeking is complete
        videoElement.addEventListener("seeked", () => {
            resolve(`Seeked to ${timeInSeconds} seconds`);
        }, { once: true });

        // Optional: handle seek errors
        videoElement.addEventListener("error", () => {
            reject(new Error("An error occurred while seeking."));
        }, { once: true });
    });
}