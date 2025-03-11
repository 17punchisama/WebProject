document.addEventListener('DOMContentLoaded', function () {
    console.log("DOM content loaded");

    let createBox = document.getElementById('create-box');
    let penIcon = document.querySelector('.fa-solid.fa-pen');

    penIcon.onclick = function () {
        console.log("Pen icon clicked");
        closeSearchBox()
        if (createBox.style.display === 'block') {
            createBox.style.display = 'none';
        } else {
            createBox.style.display = 'block';
            createBox.innerHTML = `
                        <h2>สร้างกิจกรรมของคุณ</h2>
                        <div>
                            <button id="no-revenue-btn">
                                <i class="fa-solid fa-people-pulling"></i> กิจกรรมประเภทไม่มีรายได้
                            </button>
                            <button id="revenue-btn">
                                <i class="fa-solid fa-hand-holding-dollar"></i> กิจกรรมประเภทมีรายได้
                            </button>
                        </div>`;

            document.getElementById('no-revenue-btn').onclick = function () {
                console.log("Clicked No Revenue Button");
                window.location.href = '/Post/Create';
            };
            document.getElementById('revenue-btn').onclick = function () {
                console.log("Clicked Revenue Button");
                window.location.href = 'page_create_com_post.html';
            };
        }
    };

    let accountNav = document.querySelector(".account-nav");
    let userIcon = document.querySelector('.fa-solid.fa-user');
    let notiIcon = document.querySelector('.fa-solid.fa-bell');
    let notiCon = document.querySelector(".noti-container");

    userIcon.onclick = function () {
        console.log("User icon clicked");

        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        } else {
            accountNav.style.display = 'block';
        }
    };

    notiIcon.onclick = function () {
        console.log("User icon clicked");

        if (notiCon.style.display === 'block') {
            notiCon.style.display = 'none';
        } else {
            notiCon.style.display = 'block';
        }
    };
    // ฟังก์ชันสำหรับปิดกล่องค้นหา
    function closeSearchBox() {
        searchBox.style.display = 'none';
    }

    // ฟังก์ชันสำหรับปิดกล่องสร้างกิจกรรม
    function closeCreateBox() {
        createBox.style.display = 'none';
    }
    const searchIcon = document.getElementById('search-icon');
    const searchBox = document.querySelector("#search-box");
    searchBox.id = 'search-box';
    searchIcon.onclick = function () {
        closeCreateBox(); // ปิดกล่องสร้างกิจกรรมก่อน
        if (searchBox.style.display === 'block') {
            searchBox.style.display = 'none';
        } else {
            searchBox.style.display = 'block';
        }
    };
    document.addEventListener('keydown', function (e) {
        const searchInput = document.getElementById('search-input');
        if (e.key === 'Enter' && searchInput && searchBox.style.display === 'block') {
            const query = searchInput.value.trim();
            if (query) {
                // เปลี่ยน URL ไปยังหน้าผลลัพธ์การค้นหาพร้อม query
                window.location.href = `/Home/Search?input=${encodeURIComponent(query)}`;
            }
        }
    });

    const listItems = document.querySelectorAll("#search-box ul li");
    const search_input = document.querySelector("#search-box input");
    listItems.forEach(li => {
        li.addEventListener('click', function () {
            search_input.value = li.textContent;
        });
    });
});