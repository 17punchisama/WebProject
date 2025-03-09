const showMyActivity = document.querySelector('.show-myActivity');
const showJoinedActivity = document.querySelector('.joined-activity');
const popupModal = document.getElementById('popupModal');
const closeBtn = document.getElementById('closeBtn');
const popupActivityList = document.getElementById('popupActivityList');

// ฟังก์ชันแสดงกิจกรรม (สูงสุด 3 รายการ)
function displayActivities(activityArray, container, count, isJoined = false) {
    activityArray.slice(0, count).forEach((item) => {
        let activityBox = document.createElement('div');
        let link = document.createElement('a');
        link.href = `/Post/Index/${item.postId}`
        activityBox.classList.add('activity-item');

        activityBox.innerHTML = `
            <div><i class="fa-solid fa-calendar"></i> ${item.date}</div>
            <div><i class="fa-solid fa-clock"></i> ${item.time}</div>
            <p>| ${item.name} ${isJoined ? `- ${item.owner}'s Schedule` : ''}</p>
        `;
        link.appendChild(activityBox);
        container.appendChild(link);
    });

    // ถ้ามีมากกว่า 3 รายการ ให้เพิ่มปุ่ม "ดูเพิ่มเติม"
    if (activityArray.length > count) {
        let loadMoreBtn = document.createElement('button');
        loadMoreBtn.classList.add('load-more-btn');
        loadMoreBtn.textContent = 'ดูเพิ่มเติม';
        loadMoreBtn.addEventListener('click', () => {
            showAllActivities(activityArray);
        });
        container.appendChild(loadMoreBtn);
    }
}

// ฟังก์ชันแสดงกิจกรรมทั้งหมดใน popup
function showAllActivities(activityArray) {
    popupActivityList.innerHTML = '';  // ล้างรายการเก่า
    activityArray.forEach((item) => {
        let activityBox = document.createElement('div');
        activityBox.classList.add('activity-item');

        activityBox.innerHTML = `
                <div><i class="fa-solid fa-calendar"></i> ${item.date}</div>
                <div><i class="fa-solid fa-clock"></i> ${item.time}</div>
                <p>| ${item.name}</p>
            `;

        popupActivityList.appendChild(activityBox);
    });

    popupModal.style.display = 'block';  // แสดง modal
}

// ปิด popup
closeBtn.addEventListener('click', () => {
    popupModal.style.display = 'none';
});

// แสดงเฉพาะ 3 รายการแรก + ปุ่มถ้ามีมากกว่า 3
//displayActivities(myActivity, showMyActivity, 3);
//displayActivities(joinedActivity, showJoinedActivity, 3, true);
let myActivity = [];
fetch('/Account/GetMyActivity')
    .then(response => response.json())  // Convert the response to JSON
    .then(data => {
        myActivity = data;
        displayActivities(myActivity, showMyActivity, 3);
    })
    .catch(error => {
        console.error('Error fetching data:', error);  // Handle any errors
    });
let joinedActivity = [];
fetch('/Account/GetJoinedActivity')
    .then(response => response.json())  // Convert the response to JSON
    .then(data => {
        console.log(data);
        joinedActivity = data;
        displayActivities(joinedActivity, showJoinedActivity, 3, true)
    })
    .catch(error => {
        console.error('Error fetching data:', error);  // Handle any errors
    });