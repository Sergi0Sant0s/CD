$(document).ready(function () {
    GetFolderByPath("\\");

    $('#folder-new').click(function () {
        NewFolder('teste1', '\\teste\\teste');
    });

    $('#folder-rename').click(function () {
        $('#ftp-folders button').each(function (index, item) {
            if ($(item).hasClass("selected")) {
                var text = $(item).text();
                if (text != "\\" && text != 'Up') {
                    $('#modal-rename-oldname').text('Nome antigo: ' + text);
                    $('#rename-modal').modal('toggle');
                }
                else {
                    alert("Não é possivel renomear o nome da pasta");
                }
            }
        });
    });

    $('#modal-submit-rename-folder').click(function () {
        var newName = $('#modal-rename-folder-newname').text();
        if (newName != '') {
            $('#ftp-folders button').each(function (index, item) {
                if ($(item).hasClass("selected")) {
                    var path = $(item).prop('rel');
                    debugger;
                }
            });
        }
        else
            alert('O nome não pode ser vazio');
    });


    $('#folder-delete').click(function () {
        alert('qwerty 3');
    });

    $('#file-rename').click(function () {
        alert('qwerty 4');
    });

    $('#file-delete').click(function () {
        alert('qwerty 5');
    });

    $('#file-download').click(function () {
        alert('qwerty 6');
    });

    $('#file-upload').click(function () {
        NewFile('novo ficheiro.jpg', 'este é o path');
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



});
