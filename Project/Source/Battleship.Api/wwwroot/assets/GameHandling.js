﻿function StartGame() {
    let url = "https://localhost:5001/api/games/" + sessionStorage.getItem("GameID") + "/start";
    fetch(url, {

        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
        },
        body: {},
    })
        .then((response) => {
            if (response.status == 200) {
                response.json().then(data => {

                    sessionStorage.setItem("isStarted", data.isSuccess);
                    sessionStorage.setItem("msg", data.message);
                    IsStarted();
                });
            } else {
                throw `error with status ${response.status}`;
            }
        });
        
}

function IsStarted() {

    errorMessage();
    if (sessionStorage.getItem("isStarted") === 'true') {
            sessionStorage.setItem("numberOfShots", 0);
            let knopReady = document.getElementById('Start');
             knopReady.style.visibility = 'hidden';

            const buttons = document.querySelectorAll('.btnComputer');
            buttons.forEach(function (currentBtn)
            {
                currentBtn.addEventListener('click', findCoordinateAndShoot);
            });
    }

}

function findCoordinateAndShoot() {
    let squareId2 = event.target.id;
    sessionStorage.setItem("shotPlayer", squareId2);

    let url = "https://localhost:5001/api/games" + sessionStorage.getItem("GameID") + "/shoot";
    fetch(url, {

        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
        },
        body: {
            "row": squareId2 % 10,
            "column": Math.floor(squareId2 / 10)
        },
    })
        .then((response) => {
            if (response.status == 200) {
                response.json().then(data => {
                    sessionStorage.setItem("shotResult", data.hit);
                    sessionStorage.setItem("msg", data.misfireReason);
                    sessionStorage.setItem("shotFired", data.shotFired);
                    shotAnswer();
                });
            } else {
                throw `error with status ${response.status}`;
            }
        });


}

function shotAnswer() {
    errorMessage();
    if (sessionStorage.getItem("shotFired") === 'true') {
       stats();
       drawShots();
    }
}

function stats() {
    let numberOfShots = sessionStorage.getItem("numberOfShots");
    numberOfShots++;
    sessionStorage.setItem("numberOfShots", numberOfShots);
}

function drawShots() {
    alert("draw");
}