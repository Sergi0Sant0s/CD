<style>
  body{
    background-color:rgb(221, 221, 221);
  }
</style>
<script>$('#user-name').text(name);</script>



<div class="chat-container">
  <div class="row rounded-lg overflow-hidden shadow">
    <!-- Users box-->
    <div class="col-12 col-xs-12 col-md-5" style="padding: 0;margin-bottom:35px;">
      <div class="bg-white">
        <div class="px-4 py-2" style="height:100px">
          <div class="row">
            <div class="col-2">
            <img id="user-image" src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" width="50px" alt="">
            </div>
            <div class="col-9 py-2">
            <p class="h5 mb-0 py-1" id="user-name"></p>
            </div>
          </div>
          
          
        </div>
          <!--############################################################################-->
          <!-- CHATS -->
          <div class="messages-box" id="chats">
            <div class="list-group rounded-0"></div>
          </div>
      </div>
    </div>
          <!--############################################################################-->
          <!-- MESSAGES -->
        <div class="col-12 col-xs-12 col-md-7 px-0">
          <div class="px-4 py-5 chat-box bg-white" id="messages"></div>
            <!--############################################################################-->
            <!-- Typing area -->
          <form id="form-messages" class="bg-light">
            <div class="input-group">
              <input type="text" style="border: 1px solid gray !important; border-radius:10px; margin-right:5px" placeholder="Type a message" aria-describedby="button-addon2" class="form-control py-4 bg-light">
              <div class="input-group-append">
                <button id="button-addon2" style="border:1px solid black; width:60px" type="submit" class="btn btn-link"> 
                  <span class="glyphicon glyphicon-chevron-right"></span></button>
              </div>
            </div>
          </form>
      </div>
    </div>
  </div>
</div>

<script src="js/chat.js"></script>
