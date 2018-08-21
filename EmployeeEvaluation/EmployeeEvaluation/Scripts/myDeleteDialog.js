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
                document.location.href = mcontroler + mid;
            },
            "Anuluj": function () {
                $(this).dialog("close");
            }
        },
        open: function () {
            $(this).closest('.ui-dialog').find('.ui-dialog-buttonpane button:eq(1)').focus();
            $(this).closest('.ui-dialog').find('.ui-dialog-buttonpane button:eq(0)').blur();
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
