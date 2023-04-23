function getNextQuestion(currentQuestionId) {
    $.ajax({
        url: "/Test/GetNextQuestion",
        data: { currentQuestionId: currentQuestionId, points: points },
        success: function (result) {
            $("#question-container").html(result);
        }
    });
}
