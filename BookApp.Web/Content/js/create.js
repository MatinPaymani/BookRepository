   /**
      *  Form validation
    */
    $(document).ready(function () {

        $.validator.addMethod("regex", function (value, element, regexpr) {
            return regexpr.test(value);
		}, "Please enter a valid data.");

        /**
          *contacted-form validation
        */
        $('#contacted-form').validate({

			rules: {
				"firstNameField": {
					required: true,
				},
				"lastNameField": {
					required: true,
				},
                "streetField": {
                    required: true,
				},
				"unitField":
				{
					required: true,
				},
                "phoneField": {
                    required: true,
                    regex: /^\d{8}$/,
                },
                "zipField": {
                    required: true,
                    regex: /^\d{4}$|^$/,
                },
            },
			messages: {
				"lastNameField": {
					required: "Last name is required!",
				},
				"firstNameField": {
					required: "First name is required!",
				},
                "streetField": {
                    required: "Please enter street.",
				},
				"unitField": {
					required: "Please enter unit.",
				},
                "phoneField": {
					required: "Please enter your phone number.",
					regex: "Please enter an 8-digit phone number.",
                },
                "zipField": {
                    required: "Zip code is required!",
                    regex: "It must be 4 digits!",
                },
            }
        });

		if ($('#contacted-form').length !== 0) {

			$("#contacted-form").on('submit', function (e) {

				e.preventDefault();

				if ($(this).valid()) {

					var firstNameField = $("#firstNameField").val();
					var lastNameField = $("#lastNameField").val();
					var streetField = $("#streetField").val();
					var unitField = $("#unitField").val();
					var phoneField = $("#phoneField").val();
					var zipField = $("#zipField").val();
					var flagField = $("#flagField").val();
					
					
					$.ajax({
						type: "Post",
						url: "/PhoneBook/Create",
						data: {
							FullName: { FirstName: firstNameField, LastName: lastNameField },
							Street: streetField,
							Unit: unitField,
							PhoneNumber: phoneField,
							PostNumber: zipField,
							Flag: flagField,
							PersonId: 0
						},
						cache: false,
						success: function (jqXHR, textStatus, msgThrown) {
							alert(msgThrown.statusText);
							$(location).attr("href", "/PhoneBook/");
						},
						error: function (jqXHR, textStatus, errorThrown) {
							//console.log(errormessage);
							document.getElementById("errorMessage").innerText = errorThrown;
						}
					});
				}
			});
		}
	});