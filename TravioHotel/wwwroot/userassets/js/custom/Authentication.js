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
                Swal.fire({
                    title: 'You missing Something!',
                    text: 'Please Provide your email address',
                    icon: 'warning',
                    confirmButtonText: 'Try Again!'
                });
               
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
                            Swal.fire({
                                title: 'Code sent!',
                                text: 'Verification Code Sent Check your email',
                                icon: 'success',
                                confirmButtonText: 'Close it!'
                            });
                            
                            verificationContainer.show();
                            email_input.prop("readonly", true);
                            submitBtn.text("Verify Code");
                            submitBtn.prop("disabled", false);

                        }
                        if (response.message == "Error") {
                            e.preventDefault();
                            Swal.fire({
                                title: 'An Error Occured!',
                                text: 'Failed to send your request for verification',
                                icon: 'error',
                                confirmButtonText: 'Try Again!'
                            });
                            
                            submitBtn.text('Send Again');
                            submitBtn.prop("disabled", false);

                        }
                        if (response.message == "no records") {
                            e.preventDefault();
                            Swal.fire({
                                title: 'Email Not Found!',
                                text: 'No records found for this email',
                                icon: 'error',
                                confirmButtonText: 'Try Again!'
                            });
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
                Swal.fire({
                    title: 'You Missing Something!',
                    text: 'Please Provide Verification Code',
                    icon: 'warning',
                    confirmButtonText: 'Fill it!'
                });
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
                            Swal.fire({
                                title: 'Verfication Success!',
                                text: 'Verification Successfull! Now You can reset your password',
                                icon: 'success',
                                confirmButtonText: 'Close it!'
                            });
                            
                        }
                        else if (response.message == "Error") {
                            e.preventDefault();
                            Swal.fire({
                                title: 'Verfication Not Valid!',
                                text: 'Verification Code is not Valid Please provide A valid verification code',
                                icon: 'error',
                                confirmButtonText: 'Try Again!'
                            });
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
                Swal.fire({
                    title: 'You Missing Something!',
                    text: "Password Field Cannot be empty",
                    icon: 'warning',
                    confirmButtonText: 'Fill it!'
                });
                return false;
              
            }
            if (Password_input.val().length < 8) {
                e.preventDefault();
                Swal.fire({
                    title: 'You Missing Something!',
                    text: "Password Should be more then 8 Digits or Characters",
                    icon: 'warning',
                    confirmButtonText: 'Fill it!'
                });
                return false;

            }
            if (ConfirmPass_input.val() == "") {
                e.preventDefault();
                Swal.fire({
                    title: 'You Missing Something!',
                    text: "Confirm Password Field Cannot be empty",
                    icon: 'warning',
                    confirmButtonText: 'Fill it!'
                });
                return false;
       
            }
            if (ConfirmPass_input.val().length < 8) {
                e.preventDefault();
                Swal.fire({
                    title: 'You Missing Something!',
                    text: "Password Should be more then 8 Digits or Characters",
                    icon: 'warning',
                    confirmButtonText: 'Fill it!'
                });
                return false;
              
            }
           else if (Password_input.val() != ConfirmPass_input.val() && ConfirmPass_input.val().length >= 8) {
                e.preventDefault();
                Swal.fire({
                    title: 'You Missing Something!',
                    text: "Password And Confirm Password Does'nt Matched",
                    icon: 'warning',
                    confirmButtonText: 'Match it!'
                });
                return false;
               
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
                            Swal.fire({
                                title: 'Password Reseted!',
                                text: 'Your Password Has been Changed!',
                                icon: 'success',
                                confirmButtonText: 'Close it!'
                            });

                         
                        }
                        else if (response.message == "Error") {
                            e.preventDefault();
                            Swal.fire({
                                title: 'We Are Sorry!',
                                text: 'Failed to Reset Your Password Try Again',
                                icon: 'error',
                                confirmButtonText: 'Try Again!'
                            });
                           
                            submitBtn.text("Try Again");
                            submitBtn.prop("disabled", false);
                            return false;

                        }
                    }
                });

            }

        }

      
    });
   // Login For Authentications
    let Email = $("#Email");
    let Password = $("#Password");
    let LoginForm = $("#LoginForm");
    let rememberMe = $("#rememberMe")
    $(LoginForm).submit(function (e) {
        isValid = true;
        if (Email.val() == "") {
            e.preventDefault();
            isValid = false;
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Email is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;
       
        }
        if (Email.val() != "" && !Email.val().match(/^\S+@\S+\.\S+$/)) {
            e.preventDefault();
            
            isValid = false;
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Invalid Email Pattern eg.Example@gmail.com',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;
       
    }
        if (Password.val() == "") {
        e.preventDefault();
            
            isValid = false;
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Password is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;
        }
    if (isValid == true) {

    }
})

});