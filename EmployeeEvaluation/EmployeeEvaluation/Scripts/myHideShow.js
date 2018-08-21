function hideMe(id) {
    var div = document.getElementById(id);
    div.style.display = 'none';
}

function hideAndClearMe(divId, fieldId) {
    var div = document.getElementById(divId);
    div.style.display = 'none';
    var field = document.getElementById(fieldId);
    field.value = '';
}

function hideAndClearMe(divId, fieldId1, fieldId2) {
    var div = document.getElementById(divId);
    div.style.display = 'none';
    var field1 = document.getElementById(fieldId1);
    field1.value = '';
    var field2 = document.getElementById(fieldId2);
    field2.value = '';
}

function showMe(id) {
    var div = document.getElementById(id);
    div.style.display = 'block';
    div.style.visibility = 'visible';
}

//function showAndHideMe(divId1, divId2) {
//    showMe(divId1);
//    hideMe(divId2);
//}


