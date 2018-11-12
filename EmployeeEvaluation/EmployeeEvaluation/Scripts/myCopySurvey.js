var mid = '';
var melemntId1 = '';
var mcontroler = '';

$(function () {
    $("#dialog-copy-survey").dialog({
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
                    name: $('#' + melemntId1).val()
                };
                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#inProgressDialog").offset().top
                }, 500);
                $('#inProgressDialog').show();
                $('#inProgressDialog').dialog({ modal: true, title: "Trwa kopiowanie ankiety" });
                $.ajax({
                    type: "post",
                    url: mcontroler,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(model),
                    datatype: "JSON",
                    cache: false,
                    success: function (data) {
                        if (data.result === "OK") {
                            $('#inProgressDialog').hide();
                            location.reload();
                        }
                        else {
                            $('#inProgressDialog').hide();
                            $('#inProgressDialog').dialog("close");
                            $("#errorMessage").html(data.message);
                            $("#errorDialog").dialog();
                        }

                    },
                    error: function (xhr) {
                        $('#inProgressDialog').hide();
                        $('#inProgressDialog').dialog("close");
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

function actionCopySurvey(id, elemntId1, elemntId2, controler) {
    mid = id;
    melemntId1 = elemntId1;

    $([document.documentElement, document.body]).animate({
        scrollTop: $("#dialog-copy-survey").offset().top
    }, 500);
    $('#' + elemntId1).val($.trim($('#' + elemntId2).text()));

    mcontroler = controler;
    $("#dialog-copy-survey").dialog("open");
}
