document.addEventListener("DOMContentLoaded", () => {
    let username = document.getElementById("username");
    let email = document.getElementById("email");
    let password = document.getElementById("password");

    let array = [username, email, password];
    
    document.querySelector(".submit-btn").addEventListener("click", () => {
        let is_valid = true;

        for (let i = 0; i < array.length; i++) {
            if (array[i].value == "") {
                array[i].style.border = "1px solid red";
                is_valid = false;
            } else {
                array[i].style.border = "none";
            }
        }

        if (is_valid) {
            fetch("http://localhost:5243/api/authorization" ,{
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    username: username.value,
                    email: email.value,
                    password: password.value
                })
            })
            .then(response => {
                if (!response.ok) {
                    switch (response.status) {
                        case 400:
                            alert("Something wrong with request syntax, version of brawser and other. Reload a page or ask the programmer of this web-application!");
                            break;
                        case 401:
                            alert("Problem with authorization. Remake your data(main problem), reload a page or ask the programmer of this web-application!");
                            break;
                        case 404:
                            alert("Server can be asleep. Reload a page or ask the programmer of this web-application!");
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
                    localStorage.setItem("userIdForData", String(data.userId));
                    location.href = "account.html";
                }
            })
            .catch(error => {
                console.log(error);
            })
        }
    })
})