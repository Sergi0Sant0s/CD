$(document).ready(function () {
    var obj = [];
    var chatSelected;
    GetChats();
    Connect();


    $('#form-messages input').on('keypress', function (e) {

        if (e.which == 13) {
            if ($(this).val() != "" && chatSelected != "" && localStorage.hasOwnProperty("token")) {
                //
                var msg = {
                    type: 1,
                    token: localStorage.getItem("token"),
                    chat: chatSelected,
                    message: $(this).val(),
                    time: new Date()
                };
                //Send to server
                sendMessage(JSON.stringify(msg));
            }
            $(this).val('');
            return false;
        }
    });

    $('#form-messages button').click(function () {
        $('#form-messages input').trigger($.Event("keypress", { which: 13 }));
    });


    function GetChats() {
        var link = "http://" + uri + "/getallmessages";

        $.ajax({
            url: link,
            headers: { 'Authorization': localStorage.getItem('token') },
            crossDomain: true,
            type: "POST",
            success: function (result) {
                obj = result;

                if (result.length > 0)
                    InsertChats();
            },
            error: function (xhr, status, error) {
            }
        });
    }

    function InsertChats() {
        var max = obj.length;

        for (let i = 0; i < max; i++) {
            if (i == 0) {
                newChat(1, obj[i].id, obj[i].name, obj[i].description);
                let lengthMessages = obj[i].messages.length;
                for (let m = 0; m < lengthMessages; m++) {
                    if (obj[i].messages[m].username != user)
                        newMessageSender("https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg", obj[i].messages[m].name, obj[i].messages[m].message, obj[i].messages[m].date);
                    else
                        newMessageReceiver(obj[i].messages[m].message, obj[i].messages[m].date);
                }
            }
            else
                newChat(0, obj[i].id, obj[i].name, obj[i].description);
        }
        if (max > 0)
            chatSelected = obj[0].id;
        var objDiv = document.getElementById("messages");
        objDiv.scrollTop = objDiv.scrollHeight;
    }

    function RemoveActive() {
        $('#chats button').removeClass('active');
        $('#chats button').removeClass('text-white');
        $('#chats button').addClass('text-muted');
    }

    function newChat(active, id, name, description) {

        //Root element
        var root = document.createElement("button");
        root.classList.add("list-group-item");
        root.classList.add("list-group-item-action");
        root.classList.add("list-group-item-light");
        root.classList.add("rounded-0");
        root.classList.add("px-5");
        if (active == 1) {
            root.classList.add("active");
            root.classList.add("text-white");
        }
        else {
            root.classList.add("text-muted");
        }
        root.addEventListener("click", function () {
            //Reset Messages;
            $('#messages').empty();
            RemoveActive();
            root.classList.remove("text-muted");
            root.classList.add("active");
            root.classList.add("text-white");

            for (let i = 0; i < obj.length; i++) {
                if (obj[i].id == id) {
                    for (let m = 0; m < obj[i].messages.length; m++) {
                        if (obj[i].messages[m].username !== user)
                            newMessageSender("https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg", obj[i].messages[m].name, obj[i].messages[m].message, obj[i].messages[m].date);
                        else
                            newMessageReceiver(obj[i].messages[m].message, obj[i].messages[m].date);
                    }
                }
            }
            chatSelected = id;
            var objDiv = document.getElementById("messages");
            objDiv.scrollTop = objDiv.scrollHeight;
        });


        //First Div Element
        var div1 = document.createElement("div");
        div1.classList.add("media");

        //Second div
        var div2 = document.createElement("div");
        div2.classList.add("media-body");
        div2.classList.add("ml-4");

        //third div
        var div3 = document.createElement("div");
        div3.classList.add("d-flex");
        div3.classList.add("align-items-center");
        div3.classList.add("justify-content-between");
        div3.classList.add("mb-1");

        //Name Heading
        var heading = document.createElement("h6");
        heading.classList.add("mb-0");
        heading.innerHTML = name;

        //Description p
        var descriptionElement = document.createElement("p");
        descriptionElement.classList.add("font-italic");
        descriptionElement.classList.add("mb-0");
        descriptionElement.classList.add("text-small");
        descriptionElement.innerHTML = description;

        //Appends
        root.appendChild(div1);
        div1.appendChild(div2);
        div2.appendChild(div3);
        div2.appendChild(descriptionElement);
        div3.appendChild(heading);

        //Insert new Chat
        document.getElementById("chats").appendChild(root);

    }

    function newMessageSender(image, name, message, time) {
        //First Div
        var root = document.createElement("div");
        root.classList.add("media");
        root.classList.add("w-50");

        //User image
        var img = document.createElement("img");
        img.classList.add("rounded-circle");
        img.src = image;
        img.attr = "user";
        img.style.width = "50px";

        //Second Div
        var div2 = document.createElement("div");
        div2.classList.add("media-body");
        div2.classList.add("ml-3");

        //User name
        var nameElement = document.createElement("p");
        nameElement.classList.add("text-small");
        nameElement.classList.add("mb-1");
        nameElement.classList.add("mt-2");
        nameElement.classList.add("font-weight-bold");
        nameElement.innerHTML = name;

        //Third Div
        var div3 = document.createElement("div");
        div3.classList.add("bg-light");
        div3.classList.add("rounded");
        div3.classList.add("py-1");
        div3.classList.add("px-3");
        div3.classList.add("mb-1");
        div3.style.border = "1px solid rgb(219, 219, 219)";

        //Message
        var messageElement = document.createElement("p");
        messageElement.classList.add("text-small");
        messageElement.classList.add("mb-0");
        messageElement.classList.add("text-muted");
        messageElement.innerHTML = message;

        //Time Message
        var timeMessage = document.createElement("p");
        timeMessage.classList.add("small");
        timeMessage.classList.add("text-muted");
        timeMessage.innerHTML = time;

        //Appends
        root.appendChild(img);
        root.appendChild(div2);
        div2.appendChild(nameElement);
        div2.appendChild(div3);
        div2.appendChild(timeMessage);
        div3.appendChild(messageElement);

        document.getElementById("messages").appendChild(root);
    }

    function newMessageReceiver(message, time) {
        //First Div
        var root = document.createElement("div");
        root.classList.add("media");
        root.classList.add("w-50");
        root.classList.add("ml-auto");

        //Second Div
        var div2 = document.createElement("div");
        div2.classList.add("media-body");

        //Third Div
        var div3 = document.createElement("div");
        div3.classList.add("bg-primary");
        div3.classList.add("rounded");
        div3.classList.add("py-2");
        div3.classList.add("px-2");
        div3.classList.add("mb-1");

        //Message
        var messageElement = document.createElement("p");
        messageElement.classList.add("text-small");
        messageElement.classList.add("mb-0");
        messageElement.classList.add("text-white");
        messageElement.innerHTML = message;

        //Time Message
        var timeMessage = document.createElement("p");
        timeMessage.classList.add("small");
        timeMessage.classList.add("text-muted");
        timeMessage.innerHTML = time;

        //Appends
        root.appendChild(div2);
        div2.appendChild(div3);
        div2.appendChild(timeMessage);
        div3.appendChild(messageElement);

        document.getElementById("messages").appendChild(root);
    }

    //WebSocket connect
    function Connect() {
        var link = "ws://" + uri + "/ws";
        socket = new WebSocket(link);
        var msg = {
            type: 'authenticate',
            token: localStorage.getItem("token")
        };

        socket.onopen = function (event) {
            sendMessage(JSON.stringify(msg));
        };
        //
        socket.onclose = function (event) {
            var container = $('#content');
            container.load('pages/default.php');
            navbar.load('pages/navbar-login.php');
        };
        //
        socket.onmessage = function (event) {
            var aux = JSON.parse(event.data);
            if (aux.hasOwnProperty("type")) {
                if (aux.type == 200
                    && aux.hasOwnProperty("name")
                    && aux.hasOwnProperty("username")
                    && aux.hasOwnProperty("chat")
                    && aux.hasOwnProperty("message")
                    && aux.hasOwnProperty("date")) {
                    //Insert on structure
                    var max = obj.length;

                    for (let i = 0; i < max; i++) {
                        if (obj[i].id == aux.chat) {

                            obj[i].messages.push({
                                chat: aux.chat,
                                username: aux.username,
                                name: aux.name,
                                message: aux.message,
                                date: aux.date
                            });
                            if (chatSelected == aux.chat) {
                                if (aux.username != user)
                                    newMessageSender("https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg", aux.name, aux.message, aux.date);
                                else
                                    newMessageReceiver(aux.message, aux.date);
                                //
                                var objDiv = document.getElementById("messages");
                                objDiv.scrollTop = objDiv.scrollHeight;
                            }
                            break;
                        }
                    }
                }

                else if (aux.type == 200 && aux.hasOwnProperty("message")) {
                    appendItem(list, aux.message);
                }
            }
        };
        socket.onerror = function (event) {
            console.log("error: " + event.data);
        };
    }

    function sendMessage(message) {
        socket.send(message);
    }
});
