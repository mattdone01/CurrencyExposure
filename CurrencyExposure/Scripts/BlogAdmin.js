/// <reference path="libs/jquery-1.8.1.js" />
$(document).ready(function () {
	$("#editor").kendoEditor({
		tools: [
			"bold",
			"italic",
			"underline",
			"strikethrough",
			"fontName",
			"fontSize",
			"foreColor",
			"backColor",
			"justifyLeft",
			"justifyCenter",
			"justifyRight",
			"justifyFull",
			"insertUnorderedList",
			"insertOrderedList",
			"indent",
			"outdent",
			"formatBlock",
			"createLink",
			"unlink",
			"insertImage",
			"subscript",
			"superscript",
			"viewHtml"
		]
	});
		
	$("#grid").kendoGrid({
		dataSource: {
			pageSize: 10,
			transport: {
				read: {
					url: "/Blog/GetArticlesList",
					dataType: "json",
					data: {
						count: 10
					}
				}
			},
		},
		sortable: {
			mode: "single",
			allowUnsort: false
		},
		selectable: true,
		scrollable: false,
		navigatable: true,
		filterable: true,
		columnMenu: true,
		pageable: {
			refresh: true,
			pageSizes: true
		},
		rowTemplate: kendo.template($("#rowTemplate").html()),
		columns: [{
			field: "Title",
			width: 150,
		}, {
			field: "Author",
			width: 80,
		}, {
			field: "Category",
			width: 80,
		}, {
			width: 70,
			field: "CreateDate"
		}
		],
	});
});

function onChange(id) {
	$("#blog_edit_id").val(id);
	$.getJSON('/blog/GetBlogAsJson/' + id, function (data) {
		$("#blog_edit_title").val(data.Title);
		$("#blog_edit_author").val(data.BlogAuthor.Id);
		$("#blog_edit_category").val(data.BlogCategory.Id);
		$("#blog_edit_tags").val(data.Tag);
		$("#blog_edit_summary").text(data.BlogSummary);
		var editor = $("#editor").data("kendoEditor");
		editor.value(data.Article);
	});
}

function saveblogcontents() {
	var blogId = $("#blog_edit_id").val();
	var editor = $("#editor").data("kendoEditor");
	$("#edit_blog_return_message").val("");
	$.ajax({
		type: 'POST',
		url: '/blog/SaveBlog',
		dataType: 'json',
		data: {
			BlogId: blogId,
			Title: $("#blog_edit_title").val(),
			BlogSummary: $("#blog_edit_summary").text(),
			Article: editor.value(),
			Tag: $("#blog_edit_tags").val(),
			BlogCategoryId: $("#blog_edit_category").val(),
			BlogAuthorId: $("#blog_edit_author").val()
		},
		success: function (result) {
			setResultMessage(result, "#edit_blog_return_message");
			var grid = $("#grid").data("kendoGrid");
			grid.dataSource.read();
			grid.refresh();
		}
	});
}

function clearblogcontents() {
	$("#blog_edit_title").val("");
	$("#blog_edit_author").val(1);
	$("#blog_edit_category").val(1);
	$("#blog_edit_tags").val("");
	$("#blog_edit_summary").text("");
	var editor = $("#editor").data("kendoEditor");
	editor.value("");
}

function deleteblog() {
	var blogId = $("#blog_edit_id").val();
	$("#edit_blog_return_message").val("");
	$.ajax({
		type: 'POST',
		url: '/blog/DeleteBlog',
		dataType: 'json',
		data: {
			BlogId: blogId,
		},
		success: function (result) {
			setResultMessage(result, "#edit_blog_return_message");
			var grid = $("#grid").data("kendoGrid");
			grid.dataSource.read();
			grid.refresh();
		}
	});
}

function uploadWindowOpen() {
	var url = "/templates/file_upload_template.html";
	var window = openKendoWindow("File Upload", url, uploadWindowOnOpen);
	window.center();
}

function uploadWindowOnOpen() {
	$("#files").kendoUpload({
		async: {
			saveUrl: "/blog/UploadFiles",
			autoUpload: true,
		},
		upload: function (e) {
			e.data = {
				nodeText: nodeText,
				nodeId: nodeId
			};
		},
		success: null
	});
}

function openKendoWindow(title, url, openCallBack, closeCallBack, size) {
	if (typeof size === 'undefined') {
		size = new Object();
		size.width = "500px";
		size.height = "325px";
	}
			
	$("#window").kendoWindow({
		width: size.width,
		height: size.height,
		title: title,
		actions: ["Close"],
		visible: false,
		iframe: false,
		content: url,
		open: openCallBack,
		close: closeCallBack,
		modal: false //Should this be true?
	});
	var window = $("#window").data("kendoWindow");
	window.title(title);
	window.open();
	return window;
}
