$(document).ready(function () {
    // Initialize validation on the entire ASP.NET form
    $("#formLogin").validate({
        // This prevents validation from running on every
        //  form submission by default.
        rules: {

            Username: "required",
            Password: "required"
        },

        messages: {

            Username: "",
            Password: "",

        },

        onsubmit: false

    });

    $("#MessageContainer").ready(function () {
        //$("#MenuContainer").append($("#cssmenu"))
        document.getElementById("MessageContainer").appendChild(document.getElementById("Message"))
    });

    setTimeout(function () {
        $(".Message").fadeOut(1500);
    }, 3000);

    $('#Order').click(function () {
        $(".Message").show();
        setTimeout(function () {
            $(".Message").fadeOut(1500);
        }, 3000);
    });


    $("#Order").click(function (evt) {
        // Validate the form and retain the result.
        var isValid = $("#formLogin").valid();

        // If the form didn't validate, prevent the
        //  form submission.
        if (!isValid)
            evt.preventDefault();
    });
});

if (window.location.href.match("Logout")) {
       
window.onload = nobackbutton;

function nobackbutton() {
    window.location.hash = '/?';
    window.location.hash = '/0/?';
    window.onhashchange = function () {
        window.location.hash = '/?';
    }
}
}