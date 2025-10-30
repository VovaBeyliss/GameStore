document.addEventListener("DOMContentLoaded", () => {
    document.querySelector(".account-button").addEventListener("click", () => {
        location.href = "auth.html";
    })

    document.querySelector(".menu-button").addEventListener("click", () => {
        location.href = "index.html";
    })
})