$(document).ready(function () {
//Begin Check if it is contact-list-page
	if ($('#contact-list-page').length) {
			var tble = new dataTable();
			$(function () {
				tble.InitilizeAjaxTable('contactsList', '/phonebook/getcontactlist')
			});
	function dataTable() {

		var self = this;

		self.tableListUrl = "";
		self.gridObject = null;
		self.updateTables = [];
		self.updateParams = [];
		self.reloadParameter = null;

		self.InitilizeAjaxTable = function (tableDiv, tableListUrl) {

			self.tableListUrl = tableListUrl;
			self.models = [];
			self.updateTables = self.updateTables;
			self.reloadParameter = null;

			self.gridObject = $("#" + tableDiv).bootgrid({
				rowCount: [5, 10, 25, 50, -1],
				requestHandler: function (request) {
					var model = {};
					model.Current = request.current;
					model.RowCount = request.rowCount;
					model.Search = request.searchPhrase;
					for (var key in request.sort) {
						model.SortBy = key;
						model.SortDirection = request.sort[key];
					}
					return JSON.stringify(model);
				},
				ajaxSettings: {
					method: "POST",
					contentType: "application/json"
				},
				ajax: true,
				url: self.tableListUrl,
				formatters: {
					"commands": function (column, row) {
						return "<button type=\"button\" class=\"btn btn-xs btn-default command-edit\" data-row-id=\"" + row.PersonId + "\"><span class=\"fa fa-pencil\"></span></button> " +
							"<button type=\"button\" class=\"btn btn-xs btn-default command-delete\"  data-row-id=\"" + row.PersonId + "\" data-row-name=\"" + row.FirstName + "&" + row.LastName + "\"><span class=\"fa fa-trash-o\"></span></button>" +
							"<button type=\"button\" class=\"btn btn-xs btn-default command-phones\"  data-row-id=\"" + row.PersonId + "\" data-row-name=\"" + row.FirstName + "&" + row.LastName + "\"><span class=\"fa fa-phone\"></span></button>";
					}
				}
			}).on("loaded.rs.jquery.bootgrid", function () {
				/* Executes after data is loaded and rendered */

				self.gridObject.find(".command-edit").on("click", function (e) {

					alert("You pressed edit on row: " + $(this).data("row-id"));

				}).end().find(".command-delete").on("click", function (e) {

					$('#deletePersonModal').modal('show');

					$('#deletePersonModal').before($('.modal-backdrop'));

					$('#deletePersonModal').css("z-index", parseInt($('.modal-backdrop').css('z-index')) + 1);

					document.getElementById("fullName2").innerText = "are you sure to delete " + $(this).data("row-name") + "?";

					var id = $(this).data("row-id");

					$("#yesDeletephone").click(function () {
						$.ajax({
							type: "get",
							url: "/PhoneBook/DeleteConfirmed",
							data: {
								id: id
							},
							cache: false,
							success: function (data) {
								$('#deletePersonModal').modal('hide');
								$('body').removeClass('modal-open');
								$(".modal-backdrop").remove();
								self.gridObject.bootgrid("reload");
							},
							error: function (jqXHR, textStatus, errorThrown) {
								document.getElementById("errorMessage").innerText = errorThrown;
							}
						});

					});


				}).end().find(".command-phones").on("click", function (e) {

					$('#everyonePhonesModal').modal('show');

					$('#everyonePhonesModal').before($('.modal-backdrop'));

					$('#everyonePhonesModal').css("z-index", parseInt($('.modal-backdrop').css('z-index')) + 1);

					document.getElementById("fullName").innerText = $(this).data("row-name");

					var id = $(this).data("row-id");
					$('#personIdField').val(id);
					$.get('/PhoneBook/GetAllPhone?PersonId=' + id, function (data) {
						$("#phoneList").html(data);
					});
				});
			})
		}
	}

		//show create new phone form
		$('body').on('click', '#add-new-phone', function (e) {
			//$("#add-new-phone").click(function (e) {

			e.preventDefault();
			$('#new-phone-form').show();
			return false;
		});


		//Save new phone
		$('body').on('submit', '#new-phone-form', function (e) {
			e.preventDefault();

			//New phone form validation check
			$('#new-phone-form').validate({

				rules: {
					"phoneField": {
						required: true,
						regex: /^\d{8}$/,
					}
				},
				messages: {
					"phoneField": {
						required: "Please enter your phone number.",
						regex: "Please enter an 8-digit phone number.",
					}
				}
			});

			//Validation Check
			if ($(this).valid()) {

				var phoneField = $("#phoneField").val();
				var personIdField = $("#personIdField").val();
				var flagField = $('input[name=flagField]:checked').val()

				$.ajax({
					type: "Post",
					url: "/PhoneBook/AddPhone",
					data: {
						PhoneNumber: phoneField,
						Flag: flagField,
						PersonIdn: personIdField
					},
					cache: false,
					success: function (jqXHR, textStatus, msgThrown) {
						console.log(msgThrown.statusText);
						//$('#new-phone-form').hide();

						$.get('/PhoneBook/GetAllPhone?PersonId=' + personIdField, function (data) {

							$("#phoneList").html(data);
						});
					},
					error: function (jqXHR, textStatus, errorThrown) {
						
						document.getElementById("errorMessage1").innerText = errorThrown;
					}
				});
			}

			return false;
		});

		//close create phone form
		$('body').on('click', '#cancel-add-phone', function (e) {
			$('#new-phone-form').hide();
			return false;
		});

		//open alert window in order to confirm delete phone
		$('body').on('click', '#del-phone', function (e) {

			var phoneId = $(this).data("row-id");

			var userResponse = confirm("are you sure?");

			if (userResponse === true) {

				$.ajax({
					type: "get",
					url: "/PhoneBook/DeletePhoneConfirmed",
					data: {
						id: phoneId
					},
					cache: false,
					success: function (jqXHR, textStatus, msgThrown) {
						console.log(msgThrown.statusText);
						var id = $("#personIdField1").val();
						//refresh phone list
						$.get('/PhoneBook/GetAllPhone?PersonId=' + id, function (data) {
							$("#phoneList").html(data);
						});
					},
					error: function (jqXHR, textStatus, errorThrown) {
						alert(errorThrown);
					}
				});
			} 
			return false;
		});
	}
//End - Check if it is contact-list-page
});
