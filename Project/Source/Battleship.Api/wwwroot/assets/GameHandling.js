function StartGame() {
    let url = "https://localhost:5001/api/games/" + sessionStorage.getItem("GameID") + "/start";
    fetch(url,
    {

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
            sessionStorage.setItem("shots_hit", 0);
            sessionStorage.setItem("shots_miss", 0);


            let knopReady = document.getElementById('Start');
            knopReady.style.visibility = 'hidden';

            let knopReset = document.getElementById('btnReset');
            knopReset.style.visibility = 'hidden';

            let boatlist = document.getElementById('BoatList');
            boatlist.style.visibility = 'hidden';

            let stats = document.getElementById('Stats');
            stats.style.visibility = 'visible';

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

    let url = "https://localhost:5001/api/games/" + sessionStorage.getItem("GameID") + "/shoot";
    fetch(url,
    {

        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
        },
        body: JSON.stringify({
            "row": squareId2 % 10,
            "column": Math.floor(squareId2 / 10)
        }),
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
    let shots_fired = document.getElementById("stat_fired");
    shots_fired.value = numberOfShots;

    let shot_hit = sessionStorage.getItem('shots_hit');
    let shit_miss = sessionStorage.getItem('shots_miss');

    if (sessionStorage.getItem("shotResult") === 'true') {
        shot_hit++;
        sessionStorage.setItem("shots_hit", shot_hit);
    } else {
        shit_miss++;
        sessionStorage.setItem("shots_miss", shit_miss);
    }
    let hit = document.getElementById("stat_hit");
    let miss = document.getElementById("stat_missed");
    let ratio = document.getElementById("stat_ratio");

    hit.value = shot_hit;
    miss.value = shit_miss;
    let sommetje = shot_hit / numberOfShots;
    ratio.value = sommetje.toFixed(2);
}

function drawShots() {
    let coordinaat = sessionStorage.getItem("shotPlayer");
    let isAHit = sessionStorage.getItem("shotResult") === 'true';
    let id = document.getElementById(coordinaat);
    if (isAHit) {
        id.className = "Hit";
    } else {
        id.className = "Miss";
    }
}