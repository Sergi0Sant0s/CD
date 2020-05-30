$(document).ready(function () {
    GetFolderByPath("\\");
    UploadFileAsync("\\");

    /* FOLDERS */

    $('#folder-new').click(function () {
        var modal = $('#new-folder-modal');
        $('#modal-newfolder-path').text('Path: ' + $('#map').text());
        modal.modal('toggle');

    });

    $('#modal-submit-new-folder').click(function () {
        var newName = $('#modal-new-folder-newname');
        var modal = $('#new-folder-modal');
        if (newName.val() != '') {
            NewFolderAsync($('#map').text(), newName.val());
            modal.modal('toggle');
            newName.val('');

        }
        else
            alert('O nome não pode ser vazio');
    });

    $('#folder-rename').click(function () {
        $('#ftp-folders button').each(function (index, item) {
            if ($(item).hasClass("selected")) {
                var text = $(item).text();
                if (text != "\\" && text != 'Up') {
                    $('#modal-folder-rename-oldname').text('Nome atual: ' + text);
                    $('#rename-folder-modal').modal('toggle');//
                }
                else {
                    alert("Não é possivel renomear o nome da pasta");
                }
            }
        });
    });

    $('#modal-submit-rename-folder').click(function () {
        var newName = $('#modal-rename-folder-newname');
        var modal = $('#rename-folder-modal');
        debugger;
        if (newName.val() != '') {
            $('#ftp-folders button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    var path = $(item).attr('rel');
                    RenameFolderAsync(path, newName.val());
                    modal.modal('toggle');
                    newName.val('');
                }
            });

        }
        else
            alert('O nome não pode ser vazio');
    });

    $('#folder-delete').click(function () {
        $('#ftp-folders button').each(function (index, item) {
            if ($(item).hasClass("selected")) {
                var text = $(item).text();
                if (text != "\\" && text != 'Up') {
                    $('#modal-deletefolder-path').text('Caminho: ' + $(item).attr('rel'));
                    $('#modal-deletefolder-name').text(text);
                    $('#delete-folder-modal').modal('toggle');//
                }
                else {
                    alert("Não é possivel renomear o nome da pasta");
                }
            }
        });
    });

    $('#modal-submit-delete-folder').click(function () {
        var modal = $('#delete-folder-modal');
        $('#ftp-folders button').each(function (index, item) {
            if ($(item).hasClass("selected")) {
                var path = $(item).attr('rel');
                DeleteFolderAsync(path);
                modal.modal('toggle');
            }
        });
    });

    /* FILES */

    $('#file-rename').click(function () {
        if ($('#ftp-files button').length != 0) {
            $('#ftp-files button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    var text = $(item).text();
                    if (text != "\\" && text != 'Up') {
                        $('#modal-file-rename-oldname').text('Nome atual: ' + text);
                        $('#rename-file-modal').modal('toggle');//
                    }
                    else {
                        alert("Não é possivel renomear o nome da pasta");
                    }
                }
            });
        }
        else
            alert("Não existem ficheiros disponiveis nesta pasta.");

    });

    $('#modal-submit-rename-file').click(function () {
        var newName = $('#modal-rename-file-newname');
        var modal = $('#rename-file-modal');

        if (newName.val() != '') {
            $('#ftp-files button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    var path = $(item).attr('rel');
                    var name = $(item).text();
                    var extension = '';
                    while (name[name.length - 1] != '.') {
                        var temp = name.substring(name.length - 1);
                        extension = temp + extension;
                        name = name.substring(0, name.length - 1);
                    }
                    var newNameTemp = newName.val() + '.' + extension;
                    RenameFileAsync(path, newNameTemp);
                    modal.modal('toggle');
                    newName.val('');
                }
            });

        }
        else
            alert('O nome não pode ser vazio');
    });

    $('#file-delete').click(function () {
        if ($('#ftp-files button').length != 0) {
            $('#ftp-files button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    var text = $(item).text();
                    if (text != "\\" && text != 'Up') {
                        $('#modal-deletefile-path').text('Caminho: ' + $(item).attr('rel'));
                        $('#modal-deletefile-name').text(text);
                        $('#delete-file-modal').modal('toggle');//
                    }
                    else {
                        alert("Não é possivel renomear o nome da pasta");
                    }
                }
            });
        }
        else
            alert("Não existem ficheiros disponiveis nesta pasta.");
    });

    $('#modal-submit-delete-file').click(function () {
        var modal = $('#delete-file-modal');
        $('#ftp-files button').each(function (index, item) {
            if ($(item).hasClass("selected")) {
                var path = $(item).attr('rel');
                DeleteFileAsync(path);
                modal.modal('toggle');
            }
        });
    });

    $('#file-download').click(function () {
        if ($('#ftp-files button').length != 0) {
            $('#ftp-files button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    DownloadFileAsync($(item).attr('rel'));
                }
            });
        }
        else
            alert("Não existem ficheiros disponiveis nesta pasta.");
    });

    $('#file-upload').click(function () {

    });

    /*
    0 - up
    1 - folder
    2 - subfolder
    */
    function NewFolder(type, name, path) {
        var li = $('<li></li>');
        var button = $('<button></button>');
        button.attr('rel', path);
        if (type == 2)
            button.addClass('subfolder');
        var img;
        if (type == 0)
            img = $('<img>').attr('src', 'img/ftp/arrow-back.svg');
        else
            img = $('<img>').attr('src', 'img/ftp/folder.svg');
        var span = $('<span></span>').text(name);

        //Appends
        li.append(button);
        button.append(img);
        button.append(span);

        $('#ftp-folders ul').append(li);
        if (type != 1) {
            li.dblclick(function () {
                debugger;
                GetFolderByPath(path);
            });
        }


        button.click(function () {
            $('#ftp-folders button').removeClass('selected');
            $(this).addClass('selected');

            if (type == 1 || type == 2) {
                var link = "http://" + uri + "/getfilesbypath?path=" + path;

                $.ajax({
                    url: link,
                    headers: { 'Authorization': localStorage.getItem('token') },
                    crossDomain: true,
                    type: "POST",
                    success: function (result) {
                        var count = result.files.length;

                        //Reset Files
                        $('#ftp-files ul').empty();

                        //Files
                        for (let i = 0; i < count; i++) {
                            NewFile(result.files[i].name, result.files[i].path);
                        }
                        $('#ftp-files button').first().addClass('selected');
                    },
                    error: function (xhr, status, error) {
                    }

                });
            }
        });

    }

    function NewFile(name, path) {
        var li = $('<li></li>');
        var button = $('<button></button>');
        button.attr('rel', path);
        var img = $('<img>').attr('src', 'img/ftp/file.png');
        var span = $('<span></span>').text(name);

        //Appends
        li.append(button);
        button.append(img);
        button.append(span);

        $('#ftp-files ul').append(li);
        button.click(function () {
            $('#ftp-files button').removeClass('selected');
            $(this).addClass('selected');
        });
    }

    function GetFolderByPath(path) {
        var link = "http://" + uri + "/getbypath?path=" + path;
        var savePath = path;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                //Reset
                $('#ftp-folders ul').empty();
                $('#ftp-files ul').empty();

                var countFolders = result.folders.length;
                var countFiles = result.files.length;

                $('#map').text(path);

                var aux = '';

                if (path != '\\') {
                    //If last character is an bar
                    if (path[path.length - 1] == '\\')
                        path = path.substring(0, path.length - 1);

                    //Remove last folder
                    while (path[path.length - 1] != '\\') {
                        var temp = path.substring(path.length - 1);
                        aux = temp + aux;
                        path = path.substring(0, path.length - 1);
                    }
                    //Create Up Folder
                    NewFolder(0, "Up", path);
                    NewFolder(1, aux, path);
                }
                else {
                    NewFolder(1, "\\", path);
                }


                //Folders
                for (let i = 0; i < countFolders; i++) {
                    NewFolder(2, result.folders[i].name, result.folders[i].path);
                }

                if (savePath == '\\')
                    $('#ftp-folders button').first().addClass('selected');
                else
                    $('#ftp-folders button:eq(1)').first().addClass('selected');

                //Files
                for (let i = 0; i < countFiles; i++) {
                    NewFile(result.files[i].name, result.files[i].path);
                }
                $('#ftp-files button').first().addClass('selected');
            },
            error: function (xhr, status, error) {
            }
        });
    }


    /* FOLDERS */
    function RenameFolderAsync(path, newName) {
        var link = "http://" + uri + "/renamefolder?folderPath=" + path + "&&" + "newName=" + newName;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                GetFolderByPath($('#map').text());
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a renomear a pasta");
            }
        });
    }

    function NewFolderAsync(path, newName) {
        var link = "http://" + uri + "/newfolder?path=" + path + "\\" + newName;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                GetFolderByPath($('#map').text());
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a renomear a pasta");
            }
        });
    }

    function DeleteFolderAsync(path) {
        var link = "http://" + uri + "/deletefolder?path=" + path;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "DELETE",
            success: function (result) {
                GetFolderByPath($('#map').text());
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a eliminar a pasta");
            }
        });
    }

    /* FILES */

    function RenameFileAsync(path, newName) {
        var link = "http://" + uri + "/renamefile?path=" + path + "&&" + "newName=" + newName;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                GetFolderByPath($('#map').text());
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a renomear a pasta");
            }
        });
    }

    function DeleteFileAsync(path) {
        var link = "http://" + uri + "/deletefile?path=" + path;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "DELETE",
            success: function (result) {
                GetFolderByPath($('#map').text());
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a eliminar a pasta");
            }
        });
    }

    function DownloadFileAsync(path) {

        var link = "http://" + uri + "/downloadfile?fullPath=" + path;

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            contentType: "application/octet-stream",
            success: function (data) {
                // `file download`
                $("a")
                    .attr({
                        "href": data.file,
                        "download": "file.txt"
                    })
                    .html($("a").attr("download"))
                    .get(0).click();
                console.log(JSON.parse(JSON.stringify(data)));
            },
            error: function (xhr, status, error) {
                alert("Ocorreu um erro a efetuar o download");
            }
        }).then((response) => {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            //window.location.href = url;
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'video.mp4');
            document.body.appendChild(link);
            link.click();
        });
    }
});

function UploadFileAsync(path) {
    $("#formulario").submit(function () {
        var formData = new FormData(this);
        var link = "http://" + uri + "/uploadfile?fullPath=" + path;
        debugger;

        $.ajax({
            url: link,
            type: 'POST',
            headers: { 'Authorization': localStorage.getItem('token') },
            data: formData,
            success: function (data) {
                alert(data)
            },
            cache: false,
            contentType: false,
            processData: false,
            xhr: function () { // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Avalia se tem suporte a propriedade upload
                    myXhr.upload.addEventListener('progress', function () {
                        /* faz alguma coisa durante o progresso do upload */
                    }, false);
                }
                return myXhr;
            }
        });
    });
}
