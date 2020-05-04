function regFunction() {

    let email = document.getElementById('email').value;
    let gebruikersnaam = document.getElementById('gebruikersnaam').value;
    let paswoord = document.getElementById('paswoord').value;
    let checkPaswoord = document.getElementById('checkPaswoord').value;
    let user = { email: email, password: paswoord, nickName: gebruikersnaam };
    let url = 'https://localhost:5001/api/Authentication/register';

    if (email == "" || gebruikersnaam == "" || paswoord == "" || checkPaswoord == "") {
        alert("Je hebt minstens 1 vak niet ingevuld.");
        return;
    }
    if (paswoord.length < 6) {
        alert("Wachtwoord moet minstens 6 tekens bevatten.");
        return;
    }

    if (paswoord === checkPaswoord) {
        fetch(url,
            {
                method: "POST",
                body: JSON.stringify(user),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
            .then((response) => {
                if (response.status == 200) {
                    sessionStorage.setItem("emailtje", email);
                    window.location.href = "https://localhost:5001/login_user.html";
                    return response.json();
                } else {
                    alert("Er is iets misgelopen....");
                    throw `error with status ${response.status}`;
                }
            })
            .then((user) => {
                let data = [];
                data.push([user.email, user.gebruikersnaam, user.paswoord]);

            });

    } else {
        alert("Wachtwoorden komen niet overeen!");
        return;
    }
}


    function passingEmail() {
        document.loginForm.email.value = sessionStorage.getItem("emailtje");
        }

function logFunction() {

    let email = document.getElementById('email').value;
    let paswoord = document.getElementById('paswoord').value;
    let user = { email: email, password: paswoord };
    let url = 'https://localhost:5001/api/Authentication/token';
    let is_loggedin = false;
    fetch(url,
        {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + sessionStorage.getItem("token")
            }
        })
        .then((response) => {
            if (response.status == 200) {
                response.json().then(data => {
                    sessionStorage.setItem("token", data.token);
                    window.location.href = "https://localhost:5001/index.html";
                });

            } else {
                alert("foute inloggegevens, probeer opnieuw!")
                throw `error with status ${response.status}`;
            }
        })
        .then((user) => {
            let data = [];
            data.push([user.email, user.password]);
        });
}


function CheckToken() {
    if (sessionStorage.getItem("token") == null) {
        window.location.href = "https://localhost:5001/login_user.html";
    }
}

function CheckGameID() {
    //if (sessionStorage.getItem("GameID") == null) {
    //   window.location.href = "https://localhost:5001/index.html";
    //}
}