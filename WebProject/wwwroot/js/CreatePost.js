document.addEventListener("DOMContentLoaded", function () {
    const checkboxes = document.querySelectorAll(".tag-container .tag-checkbox");

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener("change", function () {
            const checkedCount = document.querySelectorAll(".tag-container .tag-checkbox:checked").length;

            if (checkedCount > 2) {
                this.checked = false; // Prevent checking more than 2
            }
        });
    });
});