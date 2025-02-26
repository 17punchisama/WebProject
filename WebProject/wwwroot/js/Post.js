const participant = document.querySelector(".participant");
if (participant) { // Check if element exists
    const trigger = document.querySelector(".participant-amount");
    trigger.style.cursor = "pointer";
    trigger.addEventListener("click", function () {
        if (participant.style.display === "none") {
            participant.style.display = "block";
        } else {
            participant.style.display = "none";
        }
    });
}