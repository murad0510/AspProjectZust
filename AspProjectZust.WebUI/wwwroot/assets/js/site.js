
var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

async function Start() {
    try {
        await connection.start();

        console.log("SignalR Connected");
    } catch (e) {
        console.log(e);
    }
}


Start()

connection.on("Slam", function (s) {
    console.log(s);
})