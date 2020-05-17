<!--Navbar-->
<nav class="navbar sticky-top navbar-expand-lg navbar-light bg-dark">
        <button class="navbar-toggler navbar-button-collapse" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo01"
            aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <a href="#" class="navbar-toggler navbar-brand">API - Comunicação de dados</a>
        <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
            <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
                <li class="nav-item active">
                    <button id="chat" class="btn navbar-button" data-target="chat" class="nav-link" href="#">Chat <span class="sr-only">(current)</span></button>
                </li>
                <li class="nav-item">
                    <button id="ftp" class="btn navbar-button" data-target="ftp" class="nav-link" href="#">FTP <span class="sr-only">(current)</span></button>
                </li>
            </ul>
            <form class="form-inline my-2 my-lg-0" style="">
                <button type="button" id="settings" class="btn navbar-button" data-target="settings" style="margin-right: 5px;">
                    Definições
                </button>
                <button type="button" id="logout" class="btn navbar-button" data-target="logout">
                    Logout
                </button>
            </form>
        </div>
    </nav>

<!--My Own Javascript-->
<script src="./js/handlers-connected.js"></script>
