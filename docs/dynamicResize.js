var canvas;

function initialize() {
    canvas = document.querySelector("#unity-canvas");
}

document.addEventListener('DOMContentLoaded', function(event){
    initialize();
    resize();
});

function resize() {
    var width = 1080;
    var height = 1920;
    canvasAspectRatio = width / height;

    var windowWidth = window.innerWidth;
    var windowHeight = window.innerHeight;

    var aspectWindowHeight = windowWidth / canvasAspectRatio;
    if(aspectWindowHeight > windowHeight) {
        windowWidth = windowHeight * canvasAspectRatio;
    }
    else {
        windowHeight = aspectWindowHeight;
    }
    canvas.style.width = windowWidth + 'px'
    canvas.style.height = windowHeight + 'px'
}

let resizeTimer;
window.addEventListener('resize', function() {
    // 動的なリサイズは操作後0.2秒経ってから処理を実行する
    clearTimeout(resizeTimer)
    resizeTimer = setTimeout(function () {
        resize();
    }, 200)
})
