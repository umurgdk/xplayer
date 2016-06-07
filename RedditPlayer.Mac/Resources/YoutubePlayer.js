document.body.innerHTML = '<div id="player"></div>';
document.querySelector('head').innerHTML = '';

// var firstScriptTag = document.getElementsByTagName('script')[0];
// firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
var playerReady = false;

var whenGetReady = [];
    
function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
      height: '390',
      width: '640',
      videoId: '{{videoId}}',
      events: {
        'onReady': onPlayerReady,
        'onStateChange': onPlayerStateChange
      }
    });
}
 
function onPlayerReady(event) {
    playerReady = true;
    
    whenGetReady.forEach(function (cachedCall) {
        cachedCall.func.apply(null, cachedCall.args);
    });

    whenGetReady = [];
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING) {
    }
}

function makeOnReady(func) {
    return function () {
        if (playerReady) {
            return func.apply(null, arguments);
        }

        whenGetReady.push({ func: func, args: arguments });
    }
}

var playVideo = makeOnReady(function () {
    player.playVideo();
});

var pauseVideo = makeOnReady(function () {
    player.pauseVideo();
});
    
var stopVideo = makeOnReady(function () {
    player.stopVideo();
});

var mute = makeOnReady(function () {
    player.mute();
});

var unmute = makeOnReady(function () {
    player.unMute();
});

var isMuted = function () {
    if (!playerReady) {
        return false;
    }

    return player.isMuted() ? 'true' : 'false';
};

var setVolume = makeOnReady(function (volume) {
    player.setVolume(Math.round(Math.min(volume, 1) * 100));
});

function getElapsedSeconds() {
    if (!playerReady)
        return '0';

    return player.getCurrentTime().toString();
}

var tag = document.createElement('script');
tag.src = "https://www.youtube.com/iframe_api";
document.head.appendChild(tag);