
let body = document.querySelector(".test");

function lol() {
    fetch("/Post/RealTimeUpdate")
        .then(response => response.text())
        .then(txt => {
            body.innerHTML = txt;
        })
        .catch(error => console.error('Error:', error));
}

setInterval(lol,5000);