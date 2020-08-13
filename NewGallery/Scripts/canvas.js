

window.addEventListener("load", () => {
    const canvas = document.querySelector("canvas");
    const ctx = canvas.getContext("2d");

    canvas.width = window.innerWidth;


    ctx.strokeRect(30, 30, canvas.width / 2, canvas.height / 2);

    ctx.fillStyle = "pink"
    ctx.font = "50px Bahnschrift SemiBold";


    ctx.fillText("COVID-19 UPDATE", canvas.width / 2, canvas.height / 2);



});