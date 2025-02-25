document.addEventListener('DOMContentLoaded', function () {
    console.log("DOM content loaded");

    let createBox = document.getElementById('create-box');
    let penIcon = document.querySelector('.fa-solid.fa-pen');

    penIcon.onclick = function () {
        console.log("Pen icon clicked");

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
                window.location.href = 'page_create_post.html';
            };
            document.getElementById('revenue-btn').onclick = function () {
                console.log("Clicked Revenue Button");
                window.location.href = 'page_create_com_post.html';
            };
        }
    };

    let accountNav = document.querySelector(".account-nav");
    let userIcon = document.querySelector('.fa-solid.fa-user');

    userIcon.onclick = function () {
        console.log("User icon clicked");

        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        } else {
            accountNav.style.display = 'block';
        }
    };
});