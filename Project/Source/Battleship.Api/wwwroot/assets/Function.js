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