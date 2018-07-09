var mid = '';
var mname = '';
var mstatement = '';
var mcontroler = '';

$(function () {
    $("#dialog-confirm").dialog({
        resizable: false,
        autoOpen: false,
        height: "auto",
        width: "auto",
        modal: true,
        buttons: {
            "Usuń": function () {
                $(this).dialog("close");
                event.preventDefault();
                document.location.href = mcontroler + mid;
            },
            "Anuluj": function () {
                $(this).dialog("close");
            }
        }
    });
});

function actionDelete(id, name, statement, controler) {
    mid = id;
    mname = name;
    mstatement = statement;
    mcontroler = controler;
    $("#confirmchapter").text('Czy na pewno usunąć ' + mstatement + ' ' + mname + '?');
    $("#dialog-confirm").dialog("open")
};