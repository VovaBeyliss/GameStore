document.addEventListener("DOMContentLoaded", () => {
    let avatarInput = document.getElementById('avatar');
    let avatarPreview = document.getElementById('avatarPreview');
    
    let username = document.getElementById("username");
    let email = document.getElementById("email");
    let password = document.getElementById("password");

    let array = [username, email, password, avatarInput];
    
    document.querySelector(".submit-btn").addEventListener("click", () => {
        let is_valid = true;

        for (let i = 0; i < array.length; i++) {
            if (i == 3) {
                if (avatarInput.files.length == 0) {
                    avatarPreview.style.border = "1px solid red";
                    is_valid = false;
                } else {
                    avatarPreview.style.border = "2px solid rgba(255, 255, 255, 0.2)";
                }
            }

            if (array[i].value == "") {
                array[i].style.border = "1px solid red";
                is_valid = false;
            } else {
                array[i].style.border = "none";
            }
        }

        if (is_valid) {
            // let avatarPath = "img/avatars/" + avatarInput.files[0].name;

            fetch("http://localhost:5243/api/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    username: username.value, 
                    email: email.value, 
                    password: password.value, 
                    // photopath: avatarPath
                })
            })
            .then(response => {
                if (!response.ok) {
                    switch (response.status) {
                        case 400:
                            alert("Something wrong with request syntax, version of brawser and other. Reload a page or ask the programmer of this web-application!");
                            break;
                        case 404:
                            alert("Something wrong with web-application, server or brawser. Reload a page or ask the programmer of this web-application!");
                            break;
                        case 409:
                            alert("Conflict with data. Remake it, because your data have already been in database!");
                            break;
                        case 500:
                            alert("Problem in server. Reload a page or ask the programmer of this web-application!");
                            break;
                    }

                    return response.text().then(text => { throw new Error(text) });
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    localStorage.setItem("userIdForAddingProductsToDb", String(data.userId));
                    localStorage.removeItem("isAuth");
                    location.href = "index.html";
                    console.log("Success!");
                }
            })
            .catch(error => {
                console.log(error);
            })
        }
    })

    avatarInput.addEventListener('change', function(e) {
        let file = e.target.files[0];
        if (file) {
            let reader = new FileReader();
            
            reader.onload = function(e) {
                avatarPreview.innerHTML = `<img src="${e.target.result}" alt="Avatar Preview">`;
                avatarPreview.style.border = "2px solid rgba(255, 255, 255, 0.2)";
            }
            
            reader.readAsDataURL(file);
        } else {
            avatarPreview.innerHTML = "";
            avatarPreview.style.border = "1px solid red";
        }
    })
})