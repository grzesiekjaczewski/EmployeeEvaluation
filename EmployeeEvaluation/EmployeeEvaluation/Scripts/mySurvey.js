function actionPrev(surveyId) {
    page = $('#page').val();
    page--;
    if (page < 1) page = 1;
    $('#page').val(page);

    var model = {
        id: surveyId,
        name: "bla bla",
        summary: "jak się da"
    };

    $.ajax({
        type: "post",
        url: "/EmpEvaluations/PrevSurveyPage",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {
            //location.reload();
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
}

function actionNext(surveyId) {
    sectionCount = parseInt($('#sectionCount').val());
    questionCount = parseInt($('#questionCount').val());
    max = sectionCount + questionCount;

    sectionCount = parseInt($('#sectionCount').val());
    questionCount = parseInt($('#questionCount').val());
    sectionId = parseInt($('#sectionId').val());
    questionId = parseInt($('#questionId').val());

    page = $('#page').val();
    page++;
    if (page > max) page = max;
    $('#page').val(page);
    $('#userInfo').hide();
    var model = {
        id: surveyId,
        sectionId: sectionId,
        questionId: questionId,
        page: page,
        nextPrev: 'n'
        //name: "bla bla",
        //summary: "jak się da"
    };
    
    $.ajax({
        type: "post",
        url: "/EmpEvaluations/NextSurveyPage",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        datatype: "JSON",
        cache: false,
        success: function (data) {

            $('#section').val(data.surveyUserDataReturn.SectionNo);
            $('#question').val(data.surveyUserDataReturn.QuestionNo);
            $('#sectionCount').val(data.surveyUserDataReturn.TotalSections);
            $('#questionCount').val(data.surveyUserDataReturn.TotalQuestins);
            $('#sectionId').val(data.surveyUserDataReturn.SectionId);
            $('#questionId').val(data.surveyUserDataReturn.QuestinId);
            $('#questionType').val(data.surveyUserDataReturn.QuestionType);
           
            $('#sectionName').html(data.surveyUserDataReturn.SectionName);
            $('#sectionTitle').html(data.surveyUserDataReturn.SectionTitle);
            $('#sectionEmployeeSummary').html(data.surveyUserDataReturn.SectionEmployeeSummary);
            $('#sectionEmployeeScore').html(data.surveyUserDataReturn.SectionEmployeeScore);
            $('#questionName').html(data.surveyUserDataReturn.QuestionName);
            $('#questionDescription').html(data.surveyUserDataReturn.QuestionDescription);
            $('#questionEmployeeScore').html(data.surveyUserDataReturn.QuestionEmployeeScore);
            $('#questionEmployeeComment').html(data.surveyUserDataReturn.QuestionEmployeeComment);

            if (data.surveyUserDataReturn.QuestionType === 1) {
                $('#sectionEmployeeScore0').show();
            }
            else {
                $('#sectionEmployeeScore0').hide();
            }
            
            //location.reload();
        },
        error: function (xhr) {
            alert('coś poszło nie tak');
        }
    });
    if (page > 2) {
        $('#prevDiv').show();
    }
    $('#nextDiv').show();
    $('#startBtn').hide();
 }