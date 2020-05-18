$(document).ready(function () {
    //set VARS
    var login = $('#login'),
        container = $('#content'),
        navbar = $('#navbar'),
        username = $('#username'),
        password = $('#password'),
        modal = $('#modal'),
        mobileMenu = $('.navbar-toggler'),
        alertMessage = $('#danger-alert');

    //Hide Alert
    $("#danger-alert").hide();

    //Check if exists token
    if (localStorage.getItem("token") !== null)
        Valid();
    else {
        //load container
        container.load('./default.php');
        localStorage.clear();
    }

    login.on('click', function () {
        var $this = $(this)
        target = $this.data('target');

        if (target == "login" && username.val() != "" && password.val() != "") {
            const link = uri + "/login?username=" + username.val() + "&password=" + password.val();
            const data =
            {
                username: username.val(),
                password: password.val()
            }

            $.ajax({
                url: link,
                data: data,
                crossDomain: true,
                type: "POST",
                success: function (result) {
                    if (result.hasOwnProperty("authentication") && result.authentication == true) {
                        user = result.username;
                        name = result.name;
                        //Close modal
                        modal.modal('toggle');
                        mobileMenu.collapse('hide');
                        //Save Token
                        localStorage.setItem("token", result.token);
                        //New Navbar
                        navbar.load('./navbar.php');
                        container.load('./chat.php');
                        //Charge Chats
                        //ChargeChats();
                    }
                    else {
                        alertMessage.fadeTo(2000, 500).slideUp(500, function () {
                            alertMessage.slideUp(500);
                        });
                    }
                },
                error: function (xhr, status, error) {
                    alertMessage.fadeTo(2000, 500).slideUp(500, function () {
                        alertMessage.slideUp(500);
                    });
                }
            });

            //Clean inputs
            username.val("");
            password.val("");
        }
        else {
            alertMessage.fadeTo(2000, 500).slideUp(500, function () {
                alertMessage.slideUp(500);
            });
            //Clean inputs
            username.val("");
            password.val("");
        }
        return false;
    });

    function Valid() {
        const link = uri + "/validtoken";

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                debugger;
                if (result.authenticate == true) {
                    user = result.user;
                    name = result.name;
                    mobileMenu.collapse('hide');
                    //New Navbar
                    navbar.load('./navbar.php');
                    container.load('./chat.php');
                    //Charge Chats
                    //ChargeChats();
                }
                else {
                    //load container
                    container.load('./default.php');
                    localStorage.clear();
                }
            },
            error: function (xhr, status, error) {
                //load container
                container.load('./default.php');
                localStorage.clear();
            }
        });
    }
});

$(document).ready(function () {

});
