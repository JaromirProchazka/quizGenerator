function ShowAnswear(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
        n.querySelector(".heading_section_contents").style.display = "block";
        qs = n.querySelectorAll(".question_box");
        for (let i = 0; i < qs.length; i++) {
            qs[i].style.display = "block";
            qs[i].querySelector(".question_answer").style.display = "block";
        }
    } else {
        n.querySelector(".question_answer").style.display = "block";
    }
}

function ShowQuestion(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
        n.querySelector(".section_heading").style.color = "tomato";
        n = n.querySelector(".section_heading");
    } else {
        n.querySelector(".question_name").style.color = "tomato";
    }
    n.style.display = "block";
}

function HideQuestion(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
        n.querySelector(".section_heading").style.color = "black";
        n.querySelector(".heading_section_contents").style.display = "none";
        qs = n.querySelectorAll(".question_box");
        for (let i = 0; i < qs.length; i++) {
            qs[i].style.display = "none";
            qs[i].querySelector(".question_answer").style.display = "none";
        }
        n = n.querySelector(".section_heading");
    } else {
        n.querySelector(".question_answer").style.display = "none";
        n.querySelector(".question_name").style.color = "black";
    }
    n.style.display = "none";
}