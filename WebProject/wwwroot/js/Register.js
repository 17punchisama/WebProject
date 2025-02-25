document.addEventListener("submit", function (event) {
    var password = document.getElementById("Password").value;
    var confirmPassword = document.getElementById("ConfirmPassword").value;
    var errorMessage = document.getElementById("error-message");

    if (password !== confirmPassword) {
        errorMessage.innerText = "Passwords do not match!";
        event.preventDefault();
    }
});