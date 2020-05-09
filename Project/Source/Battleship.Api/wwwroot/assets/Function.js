function regFunction() {

    let email = document.getElementById('email').value;
    let gebruikersnaam = document.getElementById('gebruikersnaam').value;
    let paswoord = document.getElementById('paswoord').value;
    let checkPaswoord = document.getElementById('checkPaswoord').value;
    let user = { email: email, password: paswoord, nickName: gebruikersnaam };
    let url = 'https://localhost:5001/api/Authentication/register';

    
    if (email == "" || gebruikersnaam == "" || paswoord == "" || checkPaswoord == "") {
        sessionStorage.setItem("msg1", "Je hebt minstens 1 vak niet ingevuld.");
        sessionStorage.setItem("msg2", "");
        errorMessage();
        return;
    }

    if (paswoord.length < 6) {
        sessionStorage.setItem("msg1", "");
        sessionStorage.setItem("msg2", "paswoord moet minstens 6 tekens lang zijn");
        errorMessage();
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
                    sessionStorage.setItem("msg1", "");
                    sessionStorage.setItem("msg2", "");
                    errorMessage();
                    return response.json();
                } else {
                    sessionStorage.setItem("msg1", "Er is iets misgelopen....");
                    sessionStorage.setItem("msg2", "");
                    errorMessage();
                    
                }
            })
            .then((user) => {
                let data = [];
                data.push([user.email, user.gebruikersnaam, user.paswoord]);

            });

    } else {
        sessionStorage.setItem("msg1", "");
        sessionStorage.setItem("msg2", "paswoorden komen niet overeen");
        errorMessage();
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
                    sessionStorage.setItem("msg1", "");
                    sessionStorage.setItem("msg2", "");
                    sessionStorage.setItem("msg3", "");
                    sessionStorage.setItem("token", data.token);
                    window.location.href = "https://localhost:5001/index.html";
                    errorMessage();
                });

            } else {
                sessionStorage.setItem("msg1", "");
                sessionStorage.setItem("msg2", "");
                sessionStorage.setItem("msg3", "Foute inloggegevens");
                errorMessage();
                
            }
        })
        .then((user) => {
            let data = [];
            data.push([user.email, user.password]);
        });
}


function errorMessage() {
    let msg1 = sessionStorage.getItem("msg1")
    let msg2 = sessionStorage.getItem("msg2")
    let msg3 = sessionStorage.getItem("msg3")
    let errorBox1 = document.getElementById("errorBox1");
    let errorBox2 = document.getElementById("errorBox2");
    let errorBox3 = document.getElementById("errorBox3");
    errorBox1.value = msg1;
    errorBox2.value = msg2;
    errorBox3.value = msg3;
}

function CheckToken() {
    if (sessionStorage.getItem("token") == null) {
        window.location.href = "https://localhost:5001/login_user.html";
    }
}

function CheckGameID() {
    if (sessionStorage.getItem("GameID") == null) {
       window.location.href = "https://localhost:5001/index.html";
    }
}