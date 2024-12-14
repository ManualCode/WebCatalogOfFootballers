const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/football")
    .build();

hubConnection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

function Create(footballer) {
    footballer.birthday = footballer.birthday.split('-').reverse().join('.')
    let objectFootballer = Object.values(footballer);
    let tr = document.createElement("tr");
    tr.id = footballer.id

    for (let index = 1; index < objectFootballer.length; ++index) {
        if (index == objectFootballer.length - 1) continue;

        let td = document.createElement("td");

        if (typeof (objectFootballer[index]) == "object")
            td.textContent = Object.values(objectFootballer[index])[1];
        else
            td.textContent = objectFootballer[index];

        tr.appendChild(td);
    }

    let a_button = document.createElement("a");
    a_button.className = "btn btn-primary";
    a_button.href = `/footballers/edit/${footballer.id}`;
    a_button.textContent = "Править";

    let td_button = document.createElement("td").appendChild(a_button);
    tr.appendChild(td_button);

    return tr;
}

function CheckData(footballer) {
    let val = Object.values(footballer);
    for (let index = 1; index < val.length; ++index) {
        if (typeof (val[index]) == "object")
            val[index] = Object.values(val[index])[1];
        if (val[index] == "" || val[index] == null)
            return true;
    }
    return false;
}

hubConnection.on("Receive", function (footballer) {
    if (CheckData(footballer)) return;
    document.getElementById("tbody-of-footballers").appendChild(Create(footballer));
});

hubConnection.on("ReceiveEdit", function (footballer) {
    if (CheckData(footballer)) return;
    document.getElementById(footballer.id).replaceWith(Create(footballer));
});

hubConnection.on("ReceiveDelete", function (id) {
    document.getElementById(id).remove();
});

if (performance.navigation.type == 2) {
    location.reload(true);
}