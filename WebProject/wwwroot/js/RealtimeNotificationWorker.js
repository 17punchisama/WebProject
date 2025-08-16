self.onmessage = function (event) {
    if (event.data === "startPolling") {
        fetchNotifications();
        setInterval(fetchNotifications, 5000); // Poll every 5 seconds
    }
};

async function fetchNotifications() {
    try {
        const xhr = new XMLHttpRequest();
        xhr.open("GET", "/Notification/GetNoti", true);
        xhr.responseType = "json";

        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                self.postMessage(xhr.response); // Send data to main thread
            } else {
                console.error("Error fetching notifications:", xhr.statusText);
            }
        };

        xhr.onerror = function () {
            console.error("Network error while fetching notifications.");
        };

        xhr.send();
    } catch (error) {
        console.error("Error fetching notifications:", error);
    } 
}