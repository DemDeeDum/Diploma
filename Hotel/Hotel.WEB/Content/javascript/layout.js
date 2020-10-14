'use strict';

setMenu();

//To set page as the active one
function setMenu() {
    document.getElementById(document.getElementsByClassName("top-menu")[0]
        .getAttribute("id")).classList.add('active-item');
}


setRequestEvents();
//To unset the field checked for the radio button group where an each radio has a different name
function setRequestEvents() {

    for (let li of document.getElementsByClassName('containerOfChecks')) {
        if (isRadioAny(li.getElementsByTagName('input')))
            li.addEventListener('change', function (event) { 
                for (let radio of this.getElementsByTagName('input')) {
                    if (event.target != radio)
                        radio.checked = false;
                }
            });
    }
}

function isRadioAny(array) {
    for (let input of array) {
        if (input.getAttribute('type') == 'radio')
            return true;
    }
    return false;
}

//$('#number-input').change(function (e) {
//    document.getElementById('number-validation').innerHTML = "213123";   
//});

