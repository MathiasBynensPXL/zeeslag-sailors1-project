function StartGame() {
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
                return response.json();
            } else {
                throw `error with status ${response.status}`;
            }
    })
}