//var urlData = 'http://192.168.254.127/ShuttleOffServiceAjax/Service1.svc';
//var urlData = 'http://localhost:54458/Service1.svc'; //Lois
//var urlData = 'http://localhost/ShuttleOffService/Service1.svc'; // IIS HOST
var urlData = ' http://localhost:50869/Service1.svc' // Almer Service

//Functions onload
function ReserveOnLoad() {

    //setActiveReservations();
    setHistoryReservations();
}


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

    //Array to store each dictionary object
    var reservations = [];

    var reservationData = {
        resId: 0,
        userId: 0,
        courtName: "",
        time: "",
        address: "",
        province: "",
        city: ""
    }
    /*
    for (let i = 0; i < 1; i++) {

        //load reservation data format here, dictionary sample per object
 
        reservationData.resId = i;
        reservationData.userId = i;
        reservationData.courtName = '11111111111111';
        reservationData.time = '11111111111111';
        reservationData.address = '11111111111111';
        reservationData.province = '11111111111111';
        reservationData.city = '11111111111111';
        reservations[i] = reservationData;
    }
    */
    

    console.log(reservations);
    return reservations;
}

function setActiveReservations() {
    var parent = document.querySelector(".pane1-wrap");

    var c = localStorage.getItem("UserId");
    console.log(c);
    
    $.ajax({
        type: 'post',
        url: urlData + '/DisplayReservation',
        data: JSON.stringify({ user_id: c }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {


            if (data.d.length != 0) {
                parent.innerHTML = "";

                var isScheduleExist = 0;
                var j = -1;


                for (let i = 0; i < data.d.length; i++) {

                    if (data.d[i].Status === "Upcoming")
                    {
                        parent.innerHTML += "<div tabindex = " + j + " class=\"pane1-content\"><div class=\"pane1-content-wrap\">"
                            + "<h5>&nbsp;" + data.d[i].Name + " </h5><i class=\"fas fa-map-marker-alt\">&nbsp;"
                            + data.d[i].Province + ", " + data.d[i].City + "</i><i class=\"far fa-clock\">&nbsp;"
                            + data.d[i].StartTime + ' - ' + data.d[i].EndTime +
                            ' (' + data.d[i].DateEffective + ')' + "</i><span id = \"deleteActive\" onclick = \"deleteActiveReservations()\" ><i class=\"far fa-trash-alt\"></i></span></div></div><div class=\"court-address\"><h5>Address</h5><p>"
                            + data.d[i].Address + "</p></div>";

                        j++;

                        localStorage.setItem("reserveID" + j, data.d[i].reserveId);
                        isScheduleExist++;
                    }

                    
                }

                if (isScheduleExist == 0) {

                    parent.innerHTML += "<h2 class=\"empty- panel\">No Active Reservations...</h2>";
                }
            }
            else {
                parent.innerHTML = "";
                parent.innerHTML += "<h2 class=\"empty- panel\">No Active Reservations...</h2>";
            }

        },
        error: function (result) {
            console.log('Mali');
        }
    });
    

}

function deleteActiveReservations() {
    //delete and reload
    var indexPlace = document.activeElement.tabIndex;
    var c = localStorage.getItem("reserveID" + indexPlace);
    console.log(document.activeElement.tabIndex);

    var r = confirm("Are you sure you want to removed this reservation?")

    if (r == true)
    {
        $.ajax({
            type: 'post',
            url: urlData + '/DeleteReserveDetails',
            data: JSON.stringify({ reserve_id: c }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {

                alert(data.d);
                location.reload();
            },
            error: function (result) {
                console.log('Mali');
            }
        });
    }

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
            address: "",
            province: "",
            city: ""
        }
        reservationData.resId = i;
        reservationData.userId = i;
        reservationData.courtName = "Elly" + i;
        reservationData.time = "10PM - 11PM (2020-10-11)";
        reservationData.address = "This is an address";
        reservationData.province = "Province";
        reservationData.city = "City";
        reservations[i] = reservationData;
    }

    return reservations;
}

function setHistoryReservations() {
    var parent = document.querySelector(".pane2-wrap");

    var c = localStorage.getItem("UserId");
    
    $.ajax({
        type: 'post',
        url: urlData + '/DisplayReservation',
        data: JSON.stringify({ user_id: c }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {

            console.log(data.d.length);

            if (data.d.length != 0) {
                parent.innerHTML = "";

                var isScheduleExist = 0;
                var j = 0;

                for (let i = 0; i < data.d.length; i++) {

                    if (data.d[i].Status === "Past") {
                        parent.innerHTML += "<div tabindex = " + j + " class=\"pane1-content\"><div class=\"pane1-content-wrap\">"
                            + "<h5>&nbsp;" + data.d[i].Name + " </h5><i class=\"fas fa-map-marker-alt\">&nbsp;"
                            + data.d[i].Province + ", " + data.d[i].City + "</i><i class=\"far fa-clock\">&nbsp;"
                            + data.d[i].StartTime + ' - ' + data.d[i].EndTime +
                            ' (' + data.d[i].DateEffective + ')' + "</i><span id = \"deleteActive\" onclick = \"deleteHistory()\"><i class=\"far fa-trash-alt\"></i></span></div></div><div class=\"court-address\"><h5>Address</h5><p>"
                            + data.d[i].Address + "</p></div>";

                       

                        localStorage.setItem("histID" + j, data.d[i].reserveId);

                        j++;
                        isScheduleExist++;
                    }

                    
                }

                if (isScheduleExist == 0) {

                    parent.innerHTML += "<h2 class=\"empty- panel\">No Past Reservation..</h2>";
                }
            }
            else {
                parent.innerHTML = "";
                parent.innerHTML += "<h2 class=\"empty- panel\">No Past Reservation..</h2>";
            }

        },
        error: function (result) {
            console.log('Mali');
        }
    });

    
}

function deleteHistory() {
    //delete and reload
    var indexPlace = document.activeElement.tabIndex;
    var c = localStorage.getItem("histID" + indexPlace);
    console.log(indexPlace);



    console.log(c);
    
    var r = confirm("Are you sure you want to removed this in your History?")

    if (r == true) {
        $.ajax({
            type: 'post',
            url: urlData + '/DeleteHistoryDetails',
            data: JSON.stringify({ reserve_id: c }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {

                alert(data.d);
                location.reload();
            },
            error: function (result) {
                console.log('Mali');
            }
        });
    }
    
}