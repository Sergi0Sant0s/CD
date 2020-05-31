<div class="chat-container">
    <div class="row rounded-lg overflow-hidden shadow" style="padding: 20px">
        <div class="col-12 col-xs-12 col-md-4 ftp-buttons">
            <button id="folder-new" class="btn btn-primary">Criar</button>
            <button id="folder-rename" class="btn btn-primary">Renomear</button>
            <button id="folder-delete" class="btn btn-primary">Eliminar</button>
        </div>
        <div class="col-12 col-xs-12 col-md-8 ftp-buttons">
            <button id="file-rename" class="btn btn-primary">Renomear</button>
            <button id="file-delete" class="btn btn-primary">Eliminar</button>
            <button id="file-download" class="btn btn-primary">Download</button>
            <button id="file-upload" class="btn btn-primary">Upload</button>
        </div>
        <!-- Users box-->
        <div id="ftp-folders" class="col-12 col-xs-12 col-md-4"
            style="padding:0 !important; overflow-y: scroll; border: 3px solid white;">
            <p id="map" style="background-color: white; border: 1px solid gray;border-radius:5px; margin:0;">
            </p>
            <ul></ul>
        </div>
        <div id="ftp-files" class="col-12 col-xs-12 col-md-8 px-0"
            style="padding:0 !important;overflow-y: auto; border: 3px solid white;">
            <ul></ul>
        </div>
    </div>


    <!--MODALS-->

    <!--FOLDERS-->
    <div id="rename-folder-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Renomear pasta</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-folder-rename-oldname"></h6>
                            <input id="modal-rename-folder-newname" class="form-control mr-sm-2" type="search"
                                placeholder="Novo nome">
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-rename-folder" type="button" class="btn btn-primary">Renomear</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <div id="new-folder-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Nova Pasta</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-newfolder-path"></h6>
                            <input id="modal-new-folder-newname" class="form-control mr-sm-2" type="text"
                                placeholder="Nome">
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-new-folder" type="button" class="btn btn-primary">Criar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <div id="delete-folder-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar Pasta</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-deletefolder-path"></h6>
                            <h6 style="color:red"><span>Pasta a eliminar: </span><span
                                    id="modal-deletefolder-name"></span></h6>
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-delete-folder" type="button" class="btn btn-danger">Sim</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Nao</button>
                </div>
            </div>
        </div>
    </div>

    <!-- FILES -->
    <div id="rename-file-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar Ficheiro</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-file-rename-oldname"></h6>
                            <input id="modal-rename-file-newname" class="form-control mr-sm-2" type="search"
                                placeholder="Novo nome">
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-rename-file" type="button" class="btn btn-primary">Renomear</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
    <div id="delete-file-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar Pasta</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-deletefile-path"></h6>
                            <h6 style="color:red"><span>Ficheiro a eliminar: </span><span
                                    id="modal-deletefile-name"></span></h6>
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-delete-file" type="button" class="btn btn-danger">Sim</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Nao</button>
                </div>
            </div>
        </div>
    </div>
    <div id="upload-file-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar Pasta</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <input type="file" id="file-upload-button" class="btn btn-primary"></input>
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="modal-submit-upload-file" type="button" class="btn btn-primary">Confirmar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="js/ftp.js"></script>