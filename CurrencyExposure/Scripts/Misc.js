
//* Save with model binding.
function submitComment() {
	$("#comment_errortext").text("");
	var comment = {
		"BlogId": $("#blogid").val(),
		"Name": $("#comment_name").val(),
		"Email": $("#comment_email").val(),
		"Title": $("#comment_title").val(),
		"Comment": $("#comment_text").val()
	};

	$.ajax("/Blog/SaveComment",
		{
			data: JSON.stringify({ "comment": comment }),
			contentType: "application/json charset=utf-8",
			type: 'POST',
			success: function (result) {
				if (!result.Success)
					$("#comment_errortext").css("color", "red").text(result.ErrorText);
				else {
					location.reload(true);
				}
		}, 
	});
}