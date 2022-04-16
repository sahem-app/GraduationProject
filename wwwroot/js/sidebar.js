let mytog = document.getElementsByClassName('mytoggler')[0];
let myside = document.getElementsByClassName('side')[0];
let lis =  Array.from(document.querySelectorAll(".side ul li"));
let mynav = document.getElementsByClassName("mynav")[0];
document.onclick = function () {
    if (myside.classList.contains("open")) {
        myside.classList.toggle("open");
        mynav.style.opacity = "1";
        mytog.style.visibility = "visible";
    }
}
myside.onclick = function (e) {
    e.stopPropagation(); 
}
mytog.onclick = function (e) {
    e.stopPropagation();
    myside.classList.toggle("open");
    mynav.style.opacity = "0.2";
    mytog.style.visibility = "hidden";
}

lis.forEach(function (ele) {
    ele.onclick = function () {
        lis.forEach(function (ele) {
            ele.classList.remove("active");
        })
        this.classList.add("active");
    }
})