var modal1 = document.getElementById("userlogin");

var modal2 = document.getElementById("usersignup");

var btn1 = document.getElementById("login");

var btn2 = document.getElementById("signup");

var span = document.getElementsByClassName("close")[0];

btn1.onclick = function () {
    modal1.style.display = "block";
}

btn2.onclick = function () {
    modal2.style.display = "block";
}


window.onclick = function (event) {
    if (event.target == modal) {
        modal1.style.display = "none";
        modal2.style.display = "none";
    }
}