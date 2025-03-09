const notificationWorker = new Worker('/js/RealtimeNotificationWorker.js');

notificationWorker.onmessage = function (event) {
    const notifications = event.data;
    const container = document.querySelector(".noti-container .notread");
    container.innerHTML = ""; // Clear previous notifications

    const red_dot = document.querySelector(".red-dot");
    if (notifications.length > 0) {
        red_dot.style.display = "block";
        notifications.forEach(notification => {
            const notiDiv = document.createElement("div");
            notiDiv.classList.add("noti");

            const title = document.createElement("h5");
            if (notification.type) {
                title.innerText = "คุณได้เข้าร่วม";
            }
            else {
                title.innerText = "กิจกรรมถูกลบ";
            }


            const message = document.createElement("p");
            message.innerText = notification.postName;

            notiDiv.appendChild(title);
            notiDiv.appendChild(message);
            if (notification.type) {
                const link = document.createElement("a");
                link.href = `/Notification/Index/${notification.id}`;
                link.appendChild(notiDiv);
                container.appendChild(link);
            }
            else {
                container.appendChild(notiDiv);
            }
        });
    }
    else {
        red_dot.style.display = "none";
    }
};

notificationWorker.postMessage("startPolling");