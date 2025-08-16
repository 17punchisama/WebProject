document.addEventListener("DOMContentLoaded", function () {
    const input = document.getElementById('profileInput');
    const preview = document.getElementById('profilePreview');

    input.addEventListener('change', function () {
        const file = this.files[0]; // Get the selected file
        if (file) {
            // Display the image preview
            const reader = new FileReader();
            reader.onload = function (e) {
                preview.src = e.target.result; // Display the image preview
            };
            reader.readAsDataURL(file); // Read file as Data URL to preview

            // Send file using FormData without converting it to base64
            const formData = new FormData();
            formData.append('ProfileImage', file); // Append the file to FormData

            // Send the file to the server
            fetch('/Account/EditImgProfile', {
                method: 'POST',
                body: formData, // Send formData containing the file
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        console.log('File uploaded successfully');
                    } else {
                        console.error('File upload failed');
                    }
                })
                .catch(error => {
                    console.error('Error occurred:', error);
                });
        }
    });
});

// Functions for handling popups (for password change or other actions)
function openPopup() {
    document.getElementById("popup").style.display = "block"; // Show popup
}

function closePopup() {
    document.getElementById("popup").style.display = "none"; // Hide popup
}

function openEditPassPopup() {
    document.getElementById("edit-pass-pop").style.display = "block"; // Show edit password popup
}
