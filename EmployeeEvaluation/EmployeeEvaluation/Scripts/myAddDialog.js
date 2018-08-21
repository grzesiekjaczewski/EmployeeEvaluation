var mid = '';
var melemntId1 = '';
var melemntId2 = '';
var mcontroler = '';

$(function () {
    $("#dialog-add").dialog({
        resizable: false,
        autoOpen: false,
        height: "auto",
        width: "auto",
        modal: true,
        buttons: {
            "Zapisz": function () {
                $(this).dialog("close");
                var model = {
                    id: mid,
                    name: $('#' + melemntId1).val(),
                    summary: $('#' + melemntId2).val()
                };
                $.ajax({
                    type: "post",
                    url: mcontroler,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(model),
                    datatype: "JSON",
                    cache: false,
                    success: function (data) {
                        location.reload();
                    },
                    error: function (xhr) {
                        alert('coś poszło nie tak');
                    }
                });  
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

function actionAdd(id, elemntId1, elemntId2, controler) {
    mid = id;
    melemntId1 = elemntId1;
    melemntId2 = elemntId2;
    mcontroler = controler;
    $("#dialog-add").dialog("open")
};
