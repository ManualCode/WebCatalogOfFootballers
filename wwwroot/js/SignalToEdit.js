const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/football")
    .build();

document.getElementById("sendBtn").addEventListener("click", function () {
    let formData = {
        id: document.querySelector("input[name='Id']").value,
        firstName: document.querySelector("input[name='FirstName']").value,
        lastName: document.querySelector("input[name='LastName']").value,
        gender: { Id: 0, GenderName: document.querySelector("input[name='Gender.GenderName']:checked").value },
        birthday: document.querySelector("input[name='Birthday']").value,
        teamName: { Id: 0, NameTeam: document.querySelector("input[name='TeamName.NameTeam']").value },
        country: { Id: 0, CountryName: document.querySelector("input[name='Country.CountryName']").value }
    };
    hubConnection.invoke("Editor", formData)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

document.getElementById("delete").addEventListener("click", function () {
    let id = document.querySelector("input[name='Id']").value;
    hubConnection.invoke("Deleter", id)
        .catch(function (err) {
            return console.error(err.toString());
        });
});

hubConnection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });