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
            <p id="map" style="background-color: white; border: 1px solid gray;border-radius:5px; margin:0;">\\querty\\
            </p>
            <ul></ul>
        </div>
        <div id="ftp-files" class="col-12 col-xs-12 col-md-8 px-0"
            style="padding:0 !important;overflow-y: auto; border: 3px solid white;">
            <ul></ul>
        </div>
    </div>
    <div id="rename-modal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Rename</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form class="my-2 my-lg-0">
                            <h6 id="modal-rename-oldname"></h6>
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
</div>
<script src="js/ftp.js"></script>