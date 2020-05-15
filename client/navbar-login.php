<!-- Modal -->
<div class="modal fade" id="modal" tabindex="-1" role="dialog"
        aria-labelledby="TituloModalCentralizado" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="container container-login">
                    <div class="row">
                        <img src="img/api.png" alt="" width="100%">
                    </div>
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <input id="username" class="form-control mr-sm-2" type="search" placeholder="Username">
                            <input id="password" class="form-control mr-sm-2" type="search" placeholder="Password">
                        </form>
                    </div>
                    <div class="row">
                        <!--Alert-->
                        <div class="alert alert-danger" id="danger-alert">
                            <button type="button" class="close" data-dismiss="alert">x</button>
                                <strong>Login Invalido </strong>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: inline">
                    <button type="button" class="btn navbar-button" data-dismiss="modal" style="float: left !important">Fechar</button>
                    <button type="button" id="login" class="btn navbar-button" data-dismiss="modal" data-target="login" style="float: right">Login</button>
                </div>
            </div>
        </div>
    </div>

    

<!--Navbar-->
<nav class="navbar navbar-expand-lg navbar-light bg-dark sticky-top">
        <button class="navbar-toggler navbar-button-collapse" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo01"
            aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <a href="#" class="navbar-toggler navbar-brand">API - Comunicação de dados</a>
        <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
            <form class="form-inline my-2 my-lg-0" style="">
                <button type="button" class="btn navbar-button" data-toggle="modal"
                    data-target="#modal">
                    Login
                </button>
            </form>
        </div>
    </nav>

<!--My Own Javascript-->
<script src="./js/handlers-unplugged.js"></script>

