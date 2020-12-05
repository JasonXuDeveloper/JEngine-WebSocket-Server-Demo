let server = require('socket.io');//socket

let io = new server(8001);//创建服务器于7999端口
console.log('服务器已开始运行');
//开始运行服务器
io.on('connection', async function(socket) {
    //连接信息
    console.log("有连接了,sid: "+socket.id);
    
    //监听事件
    socket.on('hi',function(){
        //发送事件
        socket.emit("hi_back");
        console.log("已发送hi_back");
    });
    
    
    //发送事件+数据
    var player = new Object();
    player.id = 666;
    player.name = "傑";
    socket.emit("simulate_auth",player);
});