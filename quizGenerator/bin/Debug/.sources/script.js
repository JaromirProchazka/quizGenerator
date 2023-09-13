function ShowAnswear(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
      for (let el of n.querySelectorAll(".question_box")) {
        el.style.display = "block";
        el.querySelector(".question_answer").style.display = "block";
      }
    } else {
      n.querySelector(".question_answer").style.display = "block";
    }
  }
function ShowQuestion(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
        n = n.querySelector(".section_heading");
    }
    n.style.display = "block";
}
function HideQuestion(id) {
    n = document.querySelector("#" + id);
    if (n.classList.contains("heading_sections")) {
        for (let el of n.querySelectorAll(".question_box")) {
        el.style.display = "none";
        el.querySelector(".question_answer").style.display = "none";
        }
        n = n.querySelector(".section_heading");
    } else {
        n.querySelector(".question_answer").style.display = "none";
        console.log("problem");
    }
    n.style.display = "none";
}