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
            <ul></ul>
        </div>
        <div id="ftp-files" class="col-12 col-xs-12 col-md-8 px-0"
            style="padding:0 !important;overflow-y: auto; border: 3px solid white;">
            <ul></ul>
        </div>
    </div>
</div>
<script src="js/ftp.js"></script>