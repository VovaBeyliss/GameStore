document.addEventListener("DOMContentLoaded", () => {
    let userIdForData = localStorage.getItem("userIdForData");
    let totalPrice = 0;

    if (!userIdForData) {
        alert("Problem with your Id!");
    }
    
    fetch(`http://localhost:5243/api/account/${userIdForData}`)
        .then(response => {
            if (!response.ok) {
                switch (response.status) {
                    case 400:
                        alert("Something wrong with request syntax, version of brawser and other. Reload a page or ask the programmer of this web-application!");
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
                document.getElementById("username-display").textContent = data.username;
                document.getElementById("email-display").textContent = data.email;
            }
        })
        .catch(error => {
            document.getElementById("username-display").textContent = "Error with loading username";
            document.getElementById("email-display").textContent = "Error with loading email";

            console.error(error);
        });

    fetch(`http://localhost:5243/api/products/${userIdForData}`)
        .then(response => {
            if (!response.ok) {
                switch (response.status) {
                    case 400:
                        alert("Something wrong with request syntax, version of brawser and other. Reload a page or ask the programmer of this web-application!");
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
            let container = document.getElementById("products-container");
            container.innerHTML = "";

            console.log(data.products);

            if (data.success && data.products && data.products.length > 0) {
                data.products.forEach(product => {
                    let card = document.createElement("div");
                    totalPrice += parseFloat(product.price.substring(1)) * parseFloat(product.count);
                    card.className = "product-card";
                    card.innerHTML = `
                        <h3 class="product-name">${product.name || "Unnamed Product"}</h3>
                        <p class="product-description">${product.description || "No description available"}</p>
                        <div class="product-details">
                            <span class="product-price">${product.price || "0.00"}</span>
                            <span class="product-count">Quantity: ${product.count || 1}</span>
                        </div>
                    `;
                    container.appendChild(card);
                });
            } else {
                container.innerHTML = `
                    <div class="no-products">
                        You don't have any products yet. Start shopping now!
                    </div>
                `;
            }

            console.log("Success!");
        })
        .catch(error => {
            document.getElementById("products-container").innerHTML = `
            <div class="no-products">
                Error loading products. Please try again later.
            </div>
            `;

            console.log(error);
        })

    document.getElementById("buy-all-btn")?.addEventListener("click", () => {
        document.getElementById('total-price').textContent = `$${totalPrice.toFixed(2)}`;
        document.getElementById('purchase-modal').style.display = 'flex';
    })

    document.getElementById("confirm-purchase")?.addEventListener("click", () => {
        alert(`Purchase confirmed! Thank you for using my web application. 
            This web application backend is coded with C# ASP.NET & EF CORE. 
            Your products will be delivered to you in idk days)
            Total: $${totalPrice.toFixed(2)}`);
            
        document.getElementById('purchase-modal').style.display = 'none';

        location.href = "index.html";
    })

    document.getElementById("cancel-purchase")?.addEventListener("click", () => {
        document.getElementById('purchase-modal').style.display = 'none';
    })

    document.getElementById("logout-btn").addEventListener("click", () => {
        window.location.href = "index.html";
    })
})