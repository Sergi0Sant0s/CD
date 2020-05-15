$(document).ready(function () {
    //set VARS
    var logout = $('#logout'),
        settings = $('#settings'),
        chat = $('#chat'),
        ftp = $('#ftp'),
        container = $('#content'),
        navbar = $('#navbar'),
        mobileMenu = $('.navbar-toggler');

    logout.on('click', function () {
        var $this = $(this)
        target = $this.data('target');
        //Load logout
        if (target = "logout") {
            container.load('./default.php');
            navbar.load('./navbar-login.php');
        }
    });

    settings.on('click', function () {
        var $this = $(this)
        target = $this.data('target');
        //Load logout
        if (target = "settings") {
            mobileMenu.collapse('hide');
            container.load('./settings.php');
        }
    });

    chat.on('click', function () {
        var $this = $(this)
        target = $this.data('target');
        //Load chat
        if (target = "logout") {
            mobileMenu.collapse('hide');
            container.load('./chat.php');
        }
    });

    ftp.on('click', function () {
        var $this = $(this)
        target = $this.data('target');
        //Load ftp
        if (target = "logout") {
            mobileMenu.collapse('hide');
            container.load('./ftp.php');
        }
    });


});
