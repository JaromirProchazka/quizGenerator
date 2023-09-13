// Script ===================================================================================================================================

next = document.querySelector('good-btn');
nextBad = document.querySelector('bad-btn');


// Utilities ===================================================================================================================================

function addNewElement(location, nodename = 'div', text = '', newId = '', newClass = '', addStyle = '') {
    let el = document.createElement(nodename);
    let txt = document.createTextNode(text);

    if (newClass) { el.classList.add(newClass) }
    if (newId) { el.id = newId }
    if (addStyle) { el.style.cssText = addStyle }
    if (text) {
        let txt = document.createTextNode(text); el.appendChild(txt);
    }

    location.appendChild(el);
    return el
}

class _Link {
    constructor(value, next) {
        this.value = value
        this.next = next
    }
}

class _UnfairQeue {
    constructor(qeueArray) {
        this.ls = qeueArray;
        this.end = 0;
        this.indexArr = this._createIndexList();
        this.base = new _Link(this.ls[this.indexArr.pop()], null);
        this.previous = this.base;

        this.current = this.base;
        for (let i of this.indexArr) {
            let c = new _Link(qeueArray[i], this.current);
            this.current = c;
        }
        this.base.next = this.current;
    }
    _createIndexList() {
        let i = 0;
        let indexArray = [];
        while (true) {
            indexArray.push(i);
            i++;
            if (i >= this.ls.length) {
                break;
            }
        }
        indexArray.sort(() => (Math.random() > 0.5) ? 1 : -1);
        return indexArray
    }
    _moveLink(link, cutLink) {
        // cut out the Link
        let linkConectedTo = link.next;
        this.previous.next = linkConectedTo;
        // insert the Link
        let cutNext = cutLink.next;
        cutLink.next = link;
        link.next = cutNext;
        // console.log('XXX') // XXX
        // console.log(this.link) // XXX
        // console.log('XXX') // XXX
        // console.log(cutLink.value.innerText, cutLink.next.value.innerText, link.next.value.innerText) // XXX

    }
    next(num = 1) {
        const numRange = [...Array(num).keys()]
        for (let n of numRange) {
            this.previous = this.current;
            this.current = this.current.next;
        }
    }
    cheatQueu(moveToNum = 20) {
        let p = this.current.next;
        const moveInc = [...Array(moveToNum - 1).keys()];
        for (let k of moveInc) {
            p = p.next
        }
        console.log(p.value.innerText) // XXX
        this._moveLink(this.previous, p);
    }
}

// Event LISTENERS 
function onClickNext() {
    testArray = printer.getQuestionAnswearLs();
    let questionText = testArray[0].innerText;
    let questionTextNode = document.createTextNode(questionText);

    // New TEST
    let a = testArray[1];
    let answear = a.cloneNode(true);

    // Discard Old TEST
    questionBox.innerHTML = '';
    moreBox.innerHTML = '';
    asnwearBox.innerHTML = '';
    moreBox.style.cssText = 'display: none'
    asnwearBox.style.cssText = 'display: none'

    // ADD New TEST
    questionBox.appendChild(questionTextNode);
    asnwearBox.appendChild(answear);
    more.style.display = 'none';
}

next.addEventListener('click', () => {
    onClickNext();
    counter++;
    counterBox.innerText = '';
    counterText = document.createTextNode(`${counter} / ${questionsCount}`);
    counterBox.appendChild(counterText);
    if (counter > questionsCount) {
        counterBox.style.backgroundColor = 'green';
    }
});

nextBad.addEventListener('click', () => {
    reader.questionsQeue.cheatQueu(12);
    onClickNext();
});