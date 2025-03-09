self.onmessage = function (event) {
    if (event.data === "startPolling") {
        fetchNotifications();
        setInterval(fetchNotifications, 5000); // Poll every 5 seconds
    }
};

async function fetchNotifications() {
    try {
        const response = await fetch('/Notification/GetNoti'); // Fetch from backend
        const notifications = await response.json(); // Parse JSON response
        self.postMessage(notifications); // Send data to main thread
    } catch (error) {
        console.error("Error fetching notifications:", error);
    }
}