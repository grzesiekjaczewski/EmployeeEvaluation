var sectionId = 0;
var questionId = 0;
var totalSections = 0;
var totalQuestions = 0;
var totalSectionQuestions = 0;
var questionType = 0;
var questionNo = 0;
var questionSectionNo = 0;
var sectionNo = 0;
var questionEmployeeScore = 0;
var employeeComment = "";

var model;

function actionStartSurvey(surveyId, controller) {
    $('#startSectionBtn').hide();
    $('#firstNextSectionBtn').show();
    $('#nextSectionQuestionBtn').hide();
    $('#prevSectionBtn').hide();
       
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionFirstQuestion(surveyId, controller) {
    $('#SectionPart').hide();
    $('#Deadline').hide();
    $('#QuestionPart').show();
    $('#HeadPart').hide();

    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            employeeScore(data);
            questionNo++;

            if (questionSectionNo < totalSectionQuestions) {
                $('#nextQuestionBtn').show();
                $('#nextSectionBtn').hide();
                $('#completeSurveyBtn').hide();
            }
            else {
                if (sectionNo < totalSections) {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').show();
                    $('#completeSurveyBtn').hide();
                }
                else {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').hide();
                    $('#completeSurveyBtn').show();
                }
            }

            $('#prevQuestionBtn').hide();
            $('#sectionHeaderBtn').hide();

            $("input[name='employeeScore']").change(function () {
                desableNextQuestion(false);
            });

            $("#QuestionCommentText").on('change keyup paste', function () {
                desableNextQuestion(false);
            });

        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionNextQuestion(surveyId, controller) {
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            employeeScore(data);
            questionNo++;

            if (questionSectionNo < totalSectionQuestions) {
                $('#nextQuestionBtn').show();
                $('#nextSectionBtn').hide();
                $('#completeSurveyBtn').hide();
            }
            else {
                if (sectionNo < totalSections) {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').show();
                    $('#completeSurveyBtn').hide();
                }
                else {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').hide();
                    $('#completeSurveyBtn').show();
                }
            }

            if (questionSectionNo === 1) {
                if (sectionNo === 1) {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').hide();
                }
                else {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').show();
                }
            }
            else {
                $('#prevQuestionBtn').show();
                $('#sectionHeaderBtn').hide();
            }

            $("input[name='employeeScore']").change(function () {
                desableNextQuestion(false);
            });

            $("#QuestionCommentText").on('change keyup paste', function () {
                desableNextQuestion(false);
            });

        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}



function actionPrevQuestion(surveyId, controller) {
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            employeeScore(data);
            questionNo--;

            $('#nextQuestionBtn').show();
            $('#nextSectionBtn').hide();
            $('#completeSurveyBtn').hide();

            if (questionSectionNo > 1) {
                $('#prevQuestionBtn').show();
                $('#sectionHeaderBtn').hide();
                $('#completeSurveyBtn').hide();
            }
            else {
                if (sectionNo > 1) {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').show();
                    $('#completeSurveyBtn').hide();
                }
                else {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').hide();
                    $('#completeSurveyBtn').hide();
                }
            }

            $("input[name='employeeScore']").change(function () {
                desableNextQuestion(false);
            });

            $("#QuestionCommentText").on('change keyup paste', function () {
                desableNextQuestion(false);
            });

        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionNextSection(surveyId, controller) {
    $('#sectionName').html("");
    $('#sectionTitle').html("");

    $('#SectionPart').show();
    $('#QuestionPart').hide();
    $('#HeadPart').show();
    
    $('#startSectionBtn').hide();
    $('#firstNextSectionBtn').hide();
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            $('#prevSectionBtn').show();
            $('#nextSectionQuestionBtn').show();
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionSectionFirstQuestion(surveyId, controller) {

    $('#SectionPart').hide();
    $('#QuestionPart').show();
    $('#HeadPart').hide();

    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            employeeScore(data);
            questionNo++;

            if (questionSectionNo < totalSectionQuestions) {
                $('#nextQuestionBtn').show();
                $('#nextSectionBtn').hide();
                $('#completeSurveyBtn').hide();
            }
            else {
                if (sectionNo < totalSections) {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').show();
                    $('#completeSurveyBtn').hide();
                }
                else {
                    $('#nextQuestionBtn').hide();
                    $('#nextSectionBtn').hide();
                    $('#completeSurveyBtn').show();
                }
            }

            $('#prevQuestionBtn').hide();
            $('#sectionHeaderBtn').show();

            $("input[name='employeeScore']").change(function () {
                desableNextQuestion(false);
            });

            $("#QuestionCommentText").on('change keyup paste', function () {
                desableNextQuestion(false);
            });

        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionPrevSection(surveyId, controller) {
    $('#SectionPart').hide();
    $('#QuestionPart').show();
    $('#HeadPart').hide();

    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            employeeScore(data);
            questionNo--;


            $('#nextQuestionBtn').hide();
            $('#nextSectionBtn').show();
            $('#completeSurveyBtn').hide();

            if (questionSectionNo > 1) {
                $('#prevQuestionBtn').show();
                $('#sectionHeaderBtn').hide();
                $('#completeSurveyBtn').hide();
            }
            else {
                if (sectionNo > 1) {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').show();
                    $('#completeSurveyBtn').hide();
                }
                else {
                    $('#prevQuestionBtn').hide();
                    $('#sectionHeaderBtn').hide();
                    $('#completeSurveyBtn').hide();
                }
            }

            $("input[name='employeeScore']").change(function () {
                desableNextQuestion(false);
            });

            $("#QuestionCommentText").on('change keyup paste', function () {
                desableNextQuestion(false);
            });
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });    
}


function actionSectionHeader(surveyId, controller) {
    $('#sectionName').html("");
    $('#sectionTitle').html("");

    $('#SectionPart').show();
    $('#QuestionPart').hide();
    $('#HeadPart').show();

    $('#startSectionBtn').hide();
    $('#firstNextSectionBtn').hide();
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            complite(data);
            $('#prevSectionBtn').show();
            $('#nextSectionQuestionBtn').show();
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionCompleteSurvey(surveyId, controller, destination) {
    initialise(surveyId);
    $.ajax({
        type: "post",
        url: controller,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            document.location.href = destination;
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}





function initialise(surveyId) {
    questionSelection = $('input[name="employeeScore"]:checked').val();
    employeeComment = $('#QuestionCommentText').val();
    model = {
        id: surveyId,
        sectionId: sectionId,
        questionId: questionId,
        questionSelection: questionSelection,
        questionEmployeeComment: employeeComment
    };
}


function complite(data) {
    $('#sectionName').html(data.surveyUserDataReturn.SectionName);
    $('#sectionTitle').html(data.surveyUserDataReturn.SectionTitle);
    $('#questionName').html(data.surveyUserDataReturn.QuestionName);
    $('#questionDescription').html(data.surveyUserDataReturn.QuestionDescription);
    $('#QuestionCommentText').html(data.surveyUserDataReturn.QuestionEmployeeComment);

    questionType = data.surveyUserDataReturn.QuestionType;
    questionEmployeeScore = data.surveyUserDataReturn.QuestionEmployeeScore;
    sectionId = data.surveyUserDataReturn.SectionId;
    sectionNo = data.surveyUserDataReturn.SectionNo;
    questionId = data.surveyUserDataReturn.QuestionId;
    totalSections = data.surveyUserDataReturn.TotalSections;
    totalQuestions = data.surveyUserDataReturn.TotalQuestions;
    totalSectionQuestions = data.surveyUserDataReturn.TotalSectionQuestions;
    questionSectionNo = data.surveyUserDataReturn.QuestionSectionNo;
    employeeComment = data.surveyUserDataReturn.QuestionEmployeeComment;
}

function employeeScore(data) {
    if (data.surveyUserDataReturn.QuestionType === 1) {
        $('#questionEmployeeScore0').show();
        $('#QuestionCommentDiv').hide();
        if (data.surveyUserDataReturn.QuestionEmployeeScore === 0) {
            $('input[name="employeeScore"]').prop('checked', false);
            desableNextQuestion(true);
        }
        else {
            $("input[name='employeeScore'][value='" + data.surveyUserDataReturn.QuestionEmployeeScore + "']").prop('checked', true);
            desableNextQuestion(false);
        }
    }
    else {
        $('#questionEmployeeScore0').hide();
        $('#QuestionCommentDiv').show();
        if (data.surveyUserDataReturn.QuestionEmployeeComment === null) {
            $('#QuestionCommentText').html("");
            desableNextQuestion(true);
        }
        else {
            $('#QuestionCommentText').html(data.surveyUserDataReturn.QuestionEmployeeComment);
            desableNextQuestion(false);
        }
    }
}

function desableNextQuestion(desable) {
    if (desable) {
        $("#nextQuestionBtn").addClass('disabled');
        $("#nextSectionBtn").addClass('disabled');
        $("#completeSurveyBtn").addClass('disabled');
    }
    else {
        $("#nextQuestionBtn").removeClass('disabled');
        $("#nextSectionBtn").removeClass('disabled');
        $("#completeSurveyBtn").removeClass('disabled');
    }
}



