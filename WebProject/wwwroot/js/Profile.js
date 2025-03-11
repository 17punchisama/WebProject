document.addEventListener("DOMContentLoaded", function () {
    const input = document.getElementById('profileInput');
    const preview = document.getElementById('profilePreview');

    input.addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const base64String = e.target.result.split(',')[1]; // Get the Base64 part
                preview.src = e.target.result; // Show image preview

                // Send Base64 to the server
                const formData = new FormData();
                formData.append('ProfileImageBase64', base64String);

                fetch('/Account/EditImgProfile', {
                    method: 'POST',
                    body: formData,
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            console.log('ไฟล์อัปโหลดสำเร็จ');
                        } else {
                            console.error('การอัปโหลดไฟล์ล้มเหลว');
                        }
                    })
                    .catch(error => {
                        console.error('เกิดข้อผิดพลาด:', error);
                    });
            };
            reader.readAsDataURL(file); // Read file as Data URL (Base64)
        }
    });
});



function openPopup() {
    document.getElementById("popup").style.display = "block";
}

function closePopup() {
    document.getElementById("popup").style.display = "none";
}

function openEditPassPopup() {
    document.getElementById("edit-pass-pop").style.display = "block";
}
