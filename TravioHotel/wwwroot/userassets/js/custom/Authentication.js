$(document).ready(function () {
    
    let emailContainer              = $("#EmailContainer");
    let email_input                 = $('input[name="Email"]');
    let resetForm                   = $('#resetForm');
    let verificationContainer       = $("#verificationContainer");
    let verification_input          = $('input[name="Verification"]');
    let passwordContainer           = $("#passwordContainer");
    let submitBtn                   = $("#verificationButton");
    let Password_input              = $('input[name="Password"]');
    let ConfirmPass_input           = $('input[name="ConfirmPassword"]');
    let successContainer            = $("#successContainer");
    let formContainer               = $("#formContainer");
    $(submitBtn).on("click", function (e) {
        // This Method Will send a email with verification code to the users
        if (submitBtn.text() == "Get Code" || submitBtn.text() == "Send Again") {
            if (email_input.val() == "") {
                e.preventDefault();
                toastr['error']("Please Provide your email address");
                return false;
            }
            else {
                e.preventDefault();
                submitBtn.text("Processing Please wait...");
                submitBtn.prop("disabled", true);

                $.ajax({
                    url: "/Auth/Check_email",
                    type: "Post",
                    data: resetForm.serialize(),
                    success: function (response) {
                        if (response.message == "Success") {
                            e.preventDefault();
                            toastr['success']("Verification Code Sent Check your email");
                            verificationContainer.show();
                            email_input.prop("readonly", true);
                            submitBtn.text("Verify Code");
                            submitBtn.prop("disabled", false);

                        }
                        if (response.message == "Error") {
                            e.preventDefault();
                            toastr["error"]("Failed to send your request for verification");
                            submitBtn.text('Send Again');
                            submitBtn.prop("disabled", false);

                        }
                        if (response.message == "no records") {
                            e.preventDefault();
                            toastr["error"]("No records found for this email");
                            submitBtn.text('Send Again');
                            submitBtn.prop("disabled", false);

                        }
                    }
                });
            }
        } // Sending Verification Code Ends here
        if (submitBtn.text() == "Verify Code" || submitBtn.text() == "Verify Again") {

            e.preventDefault();
            if (verification_input.val() == "") {
                e.preventDefault();
                toastr['error']("Please Provide Verification Code");
                return false;
            }
            else {
                e.preventDefault(); 
                submitBtn.text("Verifying Please Wait...");
                submitBtn.prop("disabled", true);
                $.ajax({

                    url: "/Auth/Verify_Code",
                    type: "Post",
                    data: resetForm.serialize(),
                    success: function (response) {
                        if (response.message == "Success") {
                            submitBtn.prop('disabled', false);
                            submitBtn.text("Reset Password");
                            emailContainer.hide();
                            verificationContainer.hide();
                            passwordContainer.show();
                            toastr['success']("Verification Successfull! Now You can reset your password");
                        }
                        else if (response.message == "Error") {
                            e.preventDefault();
                            toastr['error']("Verification Code is not Valid Please provide A valid verification code");
                            submitBtn.text("Verify Again");
                            submitBtn.prop("disabled", false);
                        }
                    }
                }); //Ajax ends here0
            }
        }
        // The Above function was to verify the code
        // This Function Will reset The Password
        if (submitBtn.text() == "Reset Password" || submitBtn.text() == "Try Again") {
            e.preventDefault();
            if (Password_input.val() == "") {
                e.preventDefault();
                toastr['error']("Password Field Cannot be empty");
            }
            if (Password_input.val().length < 8) {
                e.preventDefault();
                toastr['error']("Password Should be more then 8 Digits or Characters");
            }
            if (ConfirmPass_input.val() == "") {
                e.preventDefault();
                toastr['error']("Confirm Password Field Cannot be empty");
            }
            if (ConfirmPass_input.val().length < 8) {
                e.preventDefault();
                toastr['error']("Password Should be more then 8 Digits or Characters");
            }
           else if (Password_input.val() != ConfirmPass_input.val() && ConfirmPass_input.val().length >= 8) {
                e.preventDefault();
                toastr['error']("Password And Confirm Password Does'nt Matched");
            }
            else {
                e.preventDefault();
                submitBtn.prop('disabled', true);
                submitBtn.text("Reseting Your Password Please Wait ..");
                $.ajax({
                    url: "/Auth/Reset_password",
                    type: "Post",
                    data: resetForm.serialize(),
                    success: function (response) {
                        if (response.message == "Success") {
                            e.preventDefault();
                            passwordContainer.hide();
                            successContainer.show();
                            formContainer.hide();
                            toastr['success']("Your Password Has been Changed!");
                        }
                        else if (response.message == "Error") {
                            e.preventDefault();
                            toastr['error']("Failed to Reset Your Password Try Again");
                            submitBtn.text("Try Again");
                            submitBtn.prop("disabled", false);

                        }
                    }
                });

            }

        }

      
    });
   

});