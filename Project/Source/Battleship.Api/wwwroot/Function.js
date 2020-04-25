function CheckToken() {
    if (sessionStorage.getItem("token") != null) {
        return;
    }
    window.location.href = "https://localhost:5001/login_user.html";
}