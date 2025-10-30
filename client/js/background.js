document.addEventListener("DOMContentLoaded", () => {
    let visitMenu = document.getElementById("background");
    let circles = [];
    let lines = [];
    let mouse = { x: 0, y: 0 };

    let containerWidth = visitMenu.clientWidth;
    let containerHeight = visitMenu.clientHeight;

    for (let i = 0; i < 250; i++) {
        let circle = document.createElement("div");
        circle.classList.add("circle");
        
        circle.x = Math.random() * containerWidth;
        circle.y = Math.random() * containerHeight;
        
        circle.vx = Math.random() * 0.8 - 0.4;
        circle.vy = Math.random() * 0.8 - 0.4;
        
        circle.style.left = `${circle.x}px`;
        circle.style.top = `${circle.y}px`;
        
        visitMenu.appendChild(circle);
        circles.push(circle);
    }

    visitMenu.addEventListener("mousemove", (e) => {
        let rect = visitMenu.getBoundingClientRect();
        mouse.x = e.clientX - rect.left;
        mouse.y = e.clientY - rect.top;
    })

    function animateCircles() {
        circles.forEach(circle => {
            circle.x += circle.vx;
            circle.y += circle.vy;
            
            if (circle.x < 0 || circle.x > containerWidth) circle.vx *= -1;
            if (circle.y < 0 || circle.y > containerHeight) circle.vy *= -1;
            
            circle.style.left = `${circle.x}px`;
            circle.style.top = `${circle.y}px`;
        })
        
        lines.forEach(line => line.remove());
        lines.length = 0;
        
        circles.forEach(circle1 => {
            circles.forEach(circle2 => {
                if (circle1 == circle2) {
                    return;
                }
                
                let distToMouse1 = Math.hypot(mouse.x - circle1.x, mouse.y - circle1.y);
                let distToMouse2 = Math.hypot(mouse.x - circle2.x, mouse.y - circle2.y);
                
                let dx = circle2.x - circle1.x;
                let dy = circle2.y - circle1.y;
                let distance = Math.hypot(dx, dy);
                
                if (distToMouse1 < 120 && distToMouse2 < 120 && distance < 150) {
                    let line = document.createElement("div");
                    line.classList.add("line");
                    
                    line.style.width = `${distance}px`;
                    line.style.left = `${circle1.x}px`;
                    line.style.top = `${circle1.y}px`;
                    
                    let angle = Math.atan2(dy, dx) * 180 / Math.PI;
                    line.style.transform = `rotate(${angle}deg)`;
                    
                    line.style.opacity = 1 - (distance / 150);
                    
                    visitMenu.appendChild(line);
                    lines.push(line);
                }
            })
        })
        
        requestAnimationFrame(animateCircles);
    }
    
    animateCircles();
})