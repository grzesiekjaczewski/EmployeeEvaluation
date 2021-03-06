﻿var mid = '';
var melemntId1 = '';
var melemntId2 = '';
var mcontroler = '';

$(function () {
    $("#dialog-publish-survey").dialog({
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
                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#inProgressDialog").offset().top
                }, 500);
                $('#inProgressDialog').show();
                $('#inProgressDialog').dialog({ modal: true, title: "Trwa publikacja ankiety" });
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

function actionPublishSurvey(id, elemntId1, elemntId2, elemntId3, elemntId4, controler) {
    mid = id;
    melemntId1 = elemntId1;
    melemntId2 = elemntId2;
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#dialog-publish-survey").offset().top
    }, 500);
    $('#' + elemntId1).val($.trim($('#' + elemntId3).text()));
    $('#' + elemntId2).val(moment(new Date()).add('days', 14).format("DD.MM.YYYY"));
    mcontroler = controler;
    $("#dialog-publish-survey").dialog("open");
}
