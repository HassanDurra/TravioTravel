$(document).ready(function () {
    let ContactForm = $("#contactForm");
    let email = $('input[name="email"]');
    let name = $('input[name="name"]');
    let subject = $('input[name="subject"]');
    let message = $('textarea[name="message"]');

    $(ContactForm).submit(function (e) {

        if (name.val() == "") {
            e.preventDefault();
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Name is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;
        }
        else if (email.val() == "") {
            e.preventDefault();
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Email is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;

        }
        else if (email.val() != "" && !email.val().match(/^\S+@\S+\.\S+$/)) {
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
        else if (subject.val() == "") {
            e.preventDefault();
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Subject is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;

        }
        else if (message.val() == "") {
            e.preventDefault();
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Message is Required',
                icon: 'warning',
                confirmButtonText: 'Fill it!'
            });
            return false;
        }
        else if (message.val().length >1200) {
            e.preventDefault();
            Swal.fire({
                title: 'You Missing Something!',
                text: 'Message is length cannot be more then 1200 characters',
                icon: 'warning',
                confirmButtonText: 'Close it!'
            });
            return false;
        }
        else {

        }


    });
})