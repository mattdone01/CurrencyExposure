(function (d, s, id) {
	var js, fjs = d.getElementsByTagName(s)[0];
	if (d.getElementById(id)) return;
	js = d.createElement(s);
	js.id = id;
	js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1";
	fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function ReInitBlogs() {
	twttr.widgets.load();
	FB.XFBML.parse();
}

function ReInitHome() {
	twttr.widgets.load();
}

function onAjaxBegin(e) {
	$("#pageLoading").css("display", "");
}
function onAjaxFailure(ajaxContext) {
	$("#pageLoading").css("display", "none");
	var response = ajaxContext.get_response();
	$("#section_body").append(response);
}
function onAjaxEnd(e) {
	$("#pageLoading").css("display", "none");
	var mypage = $("#mypageid").val();
	_gaq.push(['_trackPageview', '/' + mypage]);
}

function formatDate(datetime) {
	var dateObj = new Date(datetime);
	return dateObj.toDateString();
}

function on_blog_comment_begin() {
	$('#blog_comment_submit').attr('disabled', 'disabled');
	$("#blog_comment_submit_loading").css("display", "inline");
}

function blog_comment_submitted(result) {
	var response = setResultMessage(result.responseText, "#blog_comment_return_message");
	$("#blog_comment_submit_loading").css("display", "none");
	if(response.RenderedPartialViewUpdate.length > 0) {
		$("#blogcommentlistdiv").empty();
		$("#blogcommentlistdiv").append(response.RenderedPartialViewUpdate);
	}
}

function on_email_submit_begin() {
	$('#emailsubmit').attr('disabled', 'disabled');
	$("#emailsubmit").unbind("hover");
	$("#tiptip_holder").empty();
	$("#email_submit_loading").css("display", "inline");
}

function email_submitted(result) {
	setResultMessage(result.responseText, "#email_submit_return_message");
	$("#email_submit_loading").css("display", "none");
}

function on_contactus_begin() {
	$('#submitcomment').attr('disabled', 'disabled');
	$("#contactus_submit_loading").css("display", "inline");
}

function contact_submitted(result) {
	setResultMessage(result.responseText, "#contact_us_return_message");
	$("#contactus_submit_loading").css("display", "none");
}

function setResultMessage(response, resultDivId) {
	var myResponse = response;
	if (typeof (response.Status) === 'undefined')
		myResponse = $.parseJSON(response);
	
	var textColor = myResponse.Status ? "green" : "red";
	$(resultDivId).empty();
	$(resultDivId)
		.append("<p>" + myResponse.Message + "</p>")
		.css("color", textColor)
		.css("display", "block")
		.css("padding-left", "10px")
		.css("line-height", ".1");
	$(resultDivId).closest('form').find("input[type=text], input[type=email], textarea").val("");
	return myResponse;
}