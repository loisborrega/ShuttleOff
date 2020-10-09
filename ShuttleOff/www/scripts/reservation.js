//var urlData = 'http://192.168.254.127/ShuttleOffServiceAjax/Service1.svc';
var urlData = 'http://localhost:54458/Service1.svc';
//var urlData = 'http://localhost/ShuttleOffService/Service1.svc';

//Functions onload

setActiveReservations();
setHistoryReservations();

function openNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "260px";
}

function closeNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "0";
}

function showActivePane() {
    var p1 = document.getElementById("pane1");
    var p2 = document.getElementById("pane2");
    var indicator = document.getElementById("btn-selection-indicator");

    p1.style.transform = "translate(0,0)";
    p2.style.transform = "translate(100%,0)";
    indicator.style.transform = "translate(0,0)"

}

function showHistoryPane() {
    var p1 = document.getElementById("pane1");
    var p2 = document.getElementById("pane2");
    var indicator = document.getElementById("btn-selection-indicator");

    p1.style.transform = "translate(-100%,0)";
    p2.style.transform = "translate(0,0)";
    indicator.style.transform = "translate(100%,0)"
}

function loadActiveReservations() {

    //Sample query size of all active reservations
    let queryLength = 1;

    //Array to store each dictionary object
    var reservations = [];

    //Loading reservationData object to reservation array
    for (let i = 0; i < queryLength; i++) {

        //load reservation data format here, dictionary sample per object
        var reservationData = {
            resId: 0,
            userId: 0,
            courtName: "",
            time: "",
            address: ""
        }
        reservationData.resId = i;
        reservationData.userId = i;
        reservationData.courtName = "Elly" + i;
        reservationData.time = "10PM - 11PM";
        reservationData.address = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi facilis quidem amet excepturi sint. Dicta, repudiandae! Quaerat a dicta cum iusto libero nam. Placeat, nihil dignissimos laborum temporibus rerum pariatur?"
        reservations[i] = reservationData;
    }

    return reservations;
}

function setActiveReservations() {
    var parent = document.querySelector(".pane1-wrap");
    var reservations = loadActiveReservations();

    if (reservations.length != 0) {
        parent.innerHTML = "";
        for (let i = 0; i < reservations.length; i++) {
            parent.innerHTML += "<div tabindex = " + i + " class=\"pane1-content\"><div class=\"pane1-content-wrap\"><i class=\"fas fa-map-marker-alt\">&nbsp;"
                + reservations[i].courtName + "</i><i class=\"far fa-clock\">&nbsp;"
                + reservations[i].time + "</i><span><i class=\"far fa-trash-alt\"></i></span></div></div><div class=\"court-address\"><h4>Address</h4><p>"
                + reservations[i].address + "</p></div>";
        }
    }
}

function deleteActiveReservations() {
    //delete and reload
}

function loadHistory() {
    //Sample query size of all active reservations
    let queryLength = 4;

    //Array to store each dictionary object
    var reservations = [];

    //Loading reservationData object to reservation array
    for (let i = 0; i < queryLength; i++) {

        //load history reservation data format here, dictionary sample per object
        var reservationData = {
            resId: 0,
            userId: 0,
            courtName: "",
            time: "",
            address: ""
        }
        reservationData.resId = i;
        reservationData.userId = i;
        reservationData.courtName = "Yesterday" + i;
        reservationData.time = "10PM - 11PM";
        reservationData.address = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi facilis quidem amet excepturi sint. Dicta, repudiandae! Quaerat a dicta cum iusto libero nam. Placeat, nihil dignissimos laborum temporibus rerum pariatur?"
        reservations[i] = reservationData;
    }

    return reservations;
}

function setHistoryReservations() {
    var parent = document.querySelector(".pane2-wrap");
    var reservations = loadHistory();

    if (reservations.length != 0) {
        parent.innerHTML = "";
        for (let i = 0; i < reservations.length; i++) {
            parent.innerHTML += "<div tabindex = " + i + " class=\"pane2-content\"><div class=\"pane2-content-wrap\"><i class=\"fas fa-map-marker-alt\">&nbsp;"
                + reservations[i].courtName + "</i><i class=\"far fa-clock\">&nbsp;"
                + reservations[i].time + "</i></div></div><div class=\"court-address\"><h4>Address</h4><p>"
                + reservations[i].address + "</p></div>";
        }
    }
}