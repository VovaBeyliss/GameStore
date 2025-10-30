document.addEventListener("DOMContentLoaded", () => {
    let i = 0;

    document.getElementById("keyboards-scroll").addEventListener("click", () => {
        document.querySelector(".keyboards-zone").scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        })
    })

    document.getElementById("mice-scroll").addEventListener("click", () => {
        document.querySelector(".mice-zone").scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        })
    })

    document.getElementById("microes-scroll").addEventListener("click", () => {
        document.querySelector(".microes-zone").scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        })
    })

    document.getElementById("headphones-scroll").addEventListener("click", () => {
        document.querySelector(".headphones-zone").scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        })
    })

    class Product {
        constructor (name, description, price, photopath) {
            this.name = name;
            this.description = description;
            this.price = price;
            this.photopath = photopath
        }
    }

    const RazerBlackWidowV4Pro = new Product(
        "RAZER BLACKWIDOW V4 PRO",
        "Mechanical gaming keyboard with Razer Green switches and ergonomic wrist rest.",
        "$229.99",
        "img/keyboards/razer-blackwidow.jpg"
    );

    const LogitechG915TKL = new Product(
        "LOGITECH G915 TKL",
        "Wireless mechanical gaming keyboard with low-profile GL switches, LIGHTSYNC RGB, and aircraft-grade aluminum alloy construction.",
        "$229.99",
        "img/keyboards/logitech-g915.webp"
    );

    const CorsairK100RGB = new Product(
        "CORSAIR K100 RGB",
        "Optical-mechanical gaming keyboard with Corsair OPX switches, 4000Hz polling rate, and dynamic per-key RGB backlighting.",
        "$219.99",
        "img/keyboards/corsair-k100.jpg"
    );

    const SteelSeriesApexPro = new Product(
        "STEELSERIES APEX PRO",
        "World's fastest mechanical gaming keyboard with adjustable OmniPoint switches and OLED smart display.",
        "$199.99",
        "img/keyboards/steelseries-apex-pro.jpg"
    );

    const RazerViperV2Pro = new Product(
        "RAZER VIPER V2 PRO",
        "Ultra-lightweight (58g) wireless gaming mouse with Focus Pro 30K optical sensor and up to 80 hours battery life.",
        "$149.99",
        "img/mice/razer-viper-v2-pro.jpg"
    );

    const LogitechGProXSuperlight = new Product(
        "LOGITECH G PRO X SUPERLIGHT",
        "Esports-grade wireless gaming mouse (63g) with HERO 25K sensor, LIGHTSPEED wireless and PTFE feet.",
        "$159.99",
        "img/mice/logitech-g-pro-x.webp"
    );

    const FinalmouseStarlight12 = new Product(
        "FINALMOUSE STARLIGHT-12",
        "Ultralight (42-47g) magnesium alloy gaming mouse with PixArt 3370 sensor and infinity skins for customizable grip.",
        "$189.99",
        "img/mice/finalmouse-starlight.jpg"
    );

    const GloriousModelOWireless = new Product(
        "GLORIOUS MODEL O WIRELESS",
        "Honeycomb wireless gaming mouse (69g) with BAMF sensor, 1000Hz p.r. and RGB lighting.",
        "$79.99",
        "img/mice/glorious-model-o.png"
    );

    const BlueYetiX = new Product(
        "BLUE YETI X",
        "Professional USB condenser microphone with high-res LED metering, Blue VO!CE effects, and multiple pickup patterns.",
        "$169.99",
        "img/microphones/blue-yeti-x.webp"
    );

    const HyperXQuadCast = new Product(
        "HYPERX QUADCAST",
        "USB condenser gaming microphone with anti-vibration shock mount, built-in pop filter, and tap-to-mute sensor.",
        "$139.99",
        "img/microphones/hyperx-quadcast.jpg"
    );

    const ElgatoWave3 = new Product(
        "ELGATO WAVE 3",
        "Premium USB condenser microphone with built-in analog limiter, clipguard technology, and Wave Link software mixing.",
        "$159.99",
        "img/microphones/elgato-wave-3.webp"
    );

    const RazerSeirenV2X = new Product(
        "RAZER SEIREN V2 X",
        "Compact USB condenser microphone with supercardioid pickup pattern and built-in shock mount for crystal-clear voice capture.",
        "$99.99",
        "img/microphones/razer-seiren-v2-x.jpg"
    );

    const SteelSeriesArctisNovaPro = new Product(
        "STEELSERIES ARCTIS NOVA PRO",
        "Premium gaming headset with High Fidelity Drivers, active noise cancellation, and GameDAC Gen 2 for immersive audio.",
        "$349.99",
        "img/headphones/steelseries-arctis-nova-pro.webp"
    );

    const RazerBlackSharkV2Pro = new Product(
        "RAZER BLACKSHARK V2 PRO",
        "Wireless esports headset with TriForce Titanium 50mm drivers, THX Spatial Audio, and ultra-lightweight design.",
        "$179.99",
        "img/headphones/razer-blackshark-v2-pro.webp"
    );

    const BeyerdynamicMMX300 = new Product(
        "BEYERDYNAMIC MMX 300",
        "Professional gaming headset with Tesla drivers, broadcast-quality microphone, and luxurious velour ear pads.",
        "$299.00",
        "img/headphones/beyerdynamic-mmx-300.webp"
    );

    const AstroA50Gen4 = new Product(
        "ASTRO A50 GEN 4",
        "Wireless Dolby Atmos gaming headset with 15-hour battery life, flip-to-mute mic, and premium audio quality.",
        "$299.99",
        "img/headphones/astro-a50.jpg"
    );
 
    document.getElementById("razer-blackwidow-button").addEventListener("click", () => addProductToAccount(RazerBlackWidowV4Pro));
    document.getElementById("logitech-g915-button").addEventListener("click", () => addProductToAccount(LogitechG915TKL));
    document.getElementById("corsair-k100-button").addEventListener("click", () => addProductToAccount(CorsairK100RGB));
    document.getElementById("steelseries-apex-pro-button").addEventListener("click", () => addProductToAccount(SteelSeriesApexPro));
    document.getElementById("razer-viper-v2-pro-button").addEventListener("click", () => addProductToAccount(RazerViperV2Pro));
    document.getElementById("logitech-g-pro-x-button").addEventListener("click", () => addProductToAccount(LogitechGProXSuperlight));
    document.getElementById("finalmouse-starlight-button").addEventListener("click", () => addProductToAccount(FinalmouseStarlight12));
    document.getElementById("glorious-model-o-button").addEventListener("click", () => addProductToAccount(GloriousModelOWireless));
    document.getElementById("blue-yeti-x-button").addEventListener("click", () => addProductToAccount(BlueYetiX));
    document.getElementById("hyperx-quadcast-button").addEventListener("click", () => addProductToAccount(HyperXQuadCast));
    document.getElementById("elgato-wave-3-button").addEventListener("click", () => addProductToAccount(ElgatoWave3));
    document.getElementById("razer-seiren-v2-x-button").addEventListener("click", () => addProductToAccount(RazerSeirenV2X));
    document.getElementById("steelseries-arctis-nova-pro-button").addEventListener("click", () => addProductToAccount(SteelSeriesArctisNovaPro));
    document.getElementById("razer-blackshark-v2-pro-button").addEventListener("click", () => addProductToAccount(RazerBlackSharkV2Pro));
    document.getElementById("beyerdynamic-mmx-300-button").addEventListener("click", () => addProductToAccount(BeyerdynamicMMX300));
    document.getElementById("astro-a50-button").addEventListener("click", () => addProductToAccount(AstroA50Gen4));

    function addProductToAccount(product) {
        i++;
        document.querySelector(".products-count").textContent = i;

        fetch("http://localhost:5243/api/products", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: product.name,
                description: product.description,
                price: product.price,
            })
        })
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
                console.log("Success!");
            }
        })
        .catch(error => {
            console.log(error);
        })
    }
})