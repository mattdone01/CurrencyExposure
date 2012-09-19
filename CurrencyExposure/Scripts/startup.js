$(document).ready(function () {
	$("#body").height($(document).height() - 230);
	$(window).resize(function() {
		$("#body").height($(window).height() - 250);
	});
});