//var urlData = 'http://192.168.254.127/ShuttleOffServiceAjax/Service1.svc';
//var urlData = 'http://localhost:54458/Service1.svc';
//var urlData = 'http://localhost/ShuttleOffService/Service1.svc';
var urlData = 'http://192.168.254.127/ShuttleOffService/Service1.svc';
//var urlData_Almer = 'http://localhost:50869/Service1.svc'; // Almer Service

/*<===================== Non Dynamic Element Helpers ========================>*/
//Onload
const topMenuBtns = document.getElementsByClassName("rad");
const topMenuPanes = document.getElementsByClassName("pane");
const courtEditMenuBtn = document.getElementsByClassName("radio-btn");
const courtEditMenuPanes = document.getElementsByClassName("crt-edit-p");

//Stores temporary courtData to represent changes made visually
var courtDataTemp;
var myCourts;
var registerState = 1;

//Objects
var CourtData = {
    crtId: 0,
    userId: 0,
    courtName: "",
    crtProvince: "",
    crtCity: "",
    crtCap: "",
    crtAdd: "",
    crtDesc: "",
    crtSched: []
}

var SchedDetails = {
    schedId: "",
    schedCourtId: "",
    schedStartTime: "",
    schedEndTime: "",
    schedDateEff: "",
    schedStatus: ""
}


topMenuBtns[0].checked = true;
courtEditMenuBtn[0].checked = true;

//Top menu panes
for (let i = 0; i < topMenuBtns.length; i++) {
    topMenuBtns[i].oninput = function () {
        if (topMenuBtns[0].checked) {
            topMenuPanes[0].style.left = 0;
            topMenuPanes[1].style.left = "100%";
        }
        if (topMenuBtns[1].checked) {
            topMenuPanes[0].style.left = "-100%";
            topMenuPanes[1].style.left = 0;

            courtDataTemp = JSON.parse(JSON.stringify(CourtData));
        }
    };
}

//Court Edit menu panes
for (let i = 0; i < courtEditMenuBtn.length; i++) {
    courtEditMenuBtn[i].oninput = function () {
        if (courtEditMenuBtn[0].checked) {
            courtEditMenuPanes[0].style.left = "0";
            courtEditMenuPanes[1].style.left = "100%";
            courtEditMenuPanes[2].style.left = "200%"
        }
        if (courtEditMenuBtn[1].checked) {
            courtEditMenuPanes[0].style.left = "-100%";
            courtEditMenuPanes[1].style.left = "0";
            courtEditMenuPanes[2].style.left = "100%;"
        }
    };
}
/*Province & Cities Drop Down Boxes */
setProvinces();
var information = document.getElementsByName("information");
information[1].onchange = setCities;

function setCities() {
    var information = document.getElementsByName("information");
    var citiesByProvince = loadCitiesByProvince();
    var provinceSel = information[1].value;
    if (provinceSel == 0) {
        information[2].innerHTML = "<option>None</option>"
    }
    else {
        var citiesOptions = "";
        for (cityId in citiesByProvince[provinceSel]) {
            citiesOptions += "<option>" + citiesByProvince[provinceSel][cityId] + "</option>";
        }
        information[2].innerHTML = citiesOptions;
    }
}

function loadProvinces() {
    //parsed json data here
    var provinces = ["Bulacan", "Cavite", "Laguna", "NCR", "Pampanga", "Rizal", "Tarlac"];
    return provinces;
}

function loadCitiesByProvince() {
    //parsed json data here
    var CitiesByProvince = {
        Bulacan: ["Malolos", "Meycauayan"],
        Cavite: ["Bacoor", "Cavite", "Imus"],
        Laguna: ["Calamba", "San Pedro", "Santa Rosa"],
        NCR: ["Makati", "Manila", "Quezon", "Valenzuela"],
        Pampanga: ["Angeles", "San Fernando"],
        Rizal: ["Antipolo"],
        Tarlac: ["Tarlac"]
    }

    return CitiesByProvince;
}

function setProvinces() {
    var information = document.getElementsByName("information");
    var new_option = document.createElement("option");
    var provinces = loadProvinces();

    information[1].innerHTML = ""; //Clear Contents
    new_option.value = "";
    new_option.textContent = "None";
    information[1].appendChild(new_option);
    delete new_option; //Destroy
    /*
    <select>
        <option>None<option> <--untouched 
    <select> 
    */
    for (let i = 1; i < provinces.length; i++) {
        new_option = document.createElement("option");
        new_option.value = provinces[i];
        new_option.textContent = provinces[i];
        information[1].appendChild(new_option);
        delete new_option;
    }
}

function openNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "260px";
}

function closeNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "0";
}

//Clear Add New Pane Tab
topMenuBtns[1].addEventListener("click", function () {
    resetDetailsPane();
    
    resetSchedulesPane();
}
)

function resetDetailsPane() {
    var information = document.getElementsByName("information");
    information[0].value = "";
    information[1].value = "NCR";
    setCities();
    information[3].value = "2";
    information[4].value = "";
    information[5].value = "";
}

function resetSchedulesPane() {
    var myCourtSchedulesParent = document.querySelector(".sched-pane");
    myCourtSchedulesParent.innerHTML = "";
    myCourtSchedulesParent.innerHTML = "<h3 style=\"padding-top: 0.5rem;\">No Schedules Added Yet</h3><div onclick = \"showAvailabilityPane()\"id = \"addSched\" class=\"add-circle-container\"><i class=\"fas fa-plus-circle\" id = \"schedAdd\"></i></div>";
    courtEditMenuBtn[0].checked = true;
    courtEditMenuPanes[0].style.left = "0";
    courtEditMenuPanes[1].style.left = "100%";
    courtEditMenuPanes[2].style.left = "200%"
}

//Discard Button
const changeButtons = document.querySelectorAll(".chg-btn");
changeButtons[0].onclick = function () {

    var r = confirm("Are you sure you want to discard your changes?");
    if (r == true) {

        /*
        const topMenu = document.querySelector(".btn-container");
        const topMenuIndicator = document.getElementById("indicator");
        if (topMenu.style.top == "1%")
            topMenu.style.top = "8%";
        topMenuPanes[0].style.left = "0";
        topMenuPanes[1].style.left = "100%";
        */
        topMenuBtns[0].checked = true;

        delete courtDataTemp;
        location.reload();
    }


};

//Availability Pane Buttons
var avButtons = document.getElementsByClassName("av-btn");
//Cancel
avButtons[0].addEventListener("click", closeAvailabilityPane);

function closeAvailabilityPane() {
    courtEditMenuPanes[0].style.left = "-100%";
    courtEditMenuPanes[1].style.left = "0";
    courtEditMenuPanes[2].style.left = "100%";

    const btnBar = document.querySelector(".change-btn-bar");
    const editHeader = document.querySelector(".crt-edit-header");
    const topMenu = document.querySelector(".btn-container");

    if (btnBar.style.bottom == "-15%" && editHeader.style.top == "-10%") {
        btnBar.style.bottom = "0";
        editHeader.style.top = "0";
    }
    if (topMenuBtns[1].checked && topMenu.style.top == "1%")
        topMenu.style.top = "8%";

    setAvailabilityInputs();
}

//Apply
avButtons[1].addEventListener("click", showAddedSched)

/* <===================== Dynamic Element Helpers ========================>*/

$(document).ready(function () {
    myCourts = loadMyCourts();

    //List All Courts
});




//Grab all courtData from query(test)


function loadMyCourts() {
    //Sample query size of all court data
    //(Change these to test courts added)

    let queryLength = 5;                     //let scheduleQueryLength = 1;
    var myCourts = [];                      //Array to store each dictionary object


    var c = localStorage.getItem("UserId");
    var court_length;
    var courtInfoJSON = {
        userID: c
    };
    
    $.ajax({
        type: 'POST',
        url: urlData + '/DisplayCourtOwner',
        data: JSON.stringify(courtInfoJSON),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {

            var value = JSON.parse(data.DisplayCourtOwnerResult);
            court_length = value.length;
            //Loading courtData object to myCourts array
            for (let i = 0; i < value.length; i++) {
                //load court data format here, dictionary sample per object
                var courtData = JSON.parse(JSON.stringify(CourtData));


                //Test Values
                courtData.crtId = value[i].CourtID;
                courtData.courtName = value[i].CName;
                courtData.crtProvince = value[i].CProvince;
                courtData.crtCity = value[i].CCity;
                courtData.crtCap = value[i].CCapacity;
                courtData.crtAdd = value[i].CAddress;
                courtData.crtDesc = value[i].CDesc;

                //================================================
               

                //================================================
                myCourts[i] = courtData;
                setMyCourts(myCourts);
            }
            //console.log(myCourts);
            //return myCourts;
        },
        complete: function () {

            for (let i = 0; i < court_length; i++)
            {
                $.ajax({
                    type: 'POST',
                    url: urlData + '/DisplaySchedDetails',
                    data: JSON.stringify({ court_id: myCourts[i].crtId }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    processdata: true,
                    success: function (data) {

                        var Svalue = JSON.parse(data.DisplaySchedDetailsResult);
                        //Schedules per court
                        //for(let j = 0; j < scheduleQueryLength; j++){
                        for (let j = 0; j < Svalue.length; j++) {
                            var schedDetails = JSON.parse(JSON.stringify(SchedDetails));
                            schedDetails.schedId = Svalue[j].SchedID;
                            schedDetails.schedStartTime = Svalue[j].CStartTime;
                            schedDetails.schedEndTime = Svalue[j].CEndTime;
                            schedDetails.schedStatus = Svalue[j].CStatus;
                            schedDetails.schedDateEff = Svalue[j].CDateEff;

                            //Append to crtSched property
                            myCourts[i].crtSched.push(schedDetails);
                        }
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }
                });
            }

        },
        error: function (result) {
            alert(result.responseText);
        }
    });
    
    
    /*
    //Loading courtData object to myCourts array
    for (let i = 0; i < queryLength; i++) {
        //load court data format here, dictionary sample per object
        var courtData = JSON.parse(JSON.stringify(CourtData));

        //Test Values
        courtData.crtId = i;
        courtData.courtName = "Hilly's - Public Court No." + i;
        courtData.crtProvince = "NCR";
        courtData.crtCity = "Manila";
        courtData.crtCap = 4;
        courtData.crtAdd = "Lorem Ipsum";
        courtData.crtDesc = "Lorem Ipsum";

        //Schedules per court
        //for(let j = 0; j < scheduleQueryLength; j++){
        for (let j = 0; j < Math.random() * 11 + 1; j++) {
            var schedDetails = JSON.parse(JSON.stringify(SchedDetails));

            schedDetails.schedId = j;
            schedDetails.schedStartTime = "14:00:00";
            schedDetails.schedEndTime = "15:00:00";
            schedDetails.schedStatus = "Available"
            schedDetails.schedDateEff = "2020-10-13";

            //Append to crtSched property
            courtData.crtSched.push(schedDetails);
        }

        myCourts[i] = courtData;
    }
    */
    
    return myCourts;
}


function setMyCourts() {
    //My Courts Pane
    const myCourtParent = document.querySelector(".crt-lst");
    const myCourtSchedulesParent = document.querySelector(".sched-pane");

    if (myCourts.length != 0) {
        //Clear Contents
        myCourtParent.innerHTML = "";
        for (let i = 0; i < myCourts.length; i++) {
            myCourtParent.innerHTML += "<div tabindex =\"" +
                i + "\"class=\"crt-lst-cnt\"><div class=\"crt-lst-cnt-wrp\"><span class = \"crt-lst-num\">" +
                i + "</span><div class=\"crt-det\"><h3>" +
                myCourts[i].courtName + "</h3><h4>" + myCourts[i].crtProvince + "," + myCourts[i].crtCity + "</h4 ></div > <span onclick= \"CourtEdit()\" class = \"edit-btn slide-menu\"><i class=\"fas fa-tools\"></i></span><span onclick= \"CourtDelete()\" class = \"trash-btn slide-menu\"><i class=\"fas fa-trash-alt\"></i></span></div></div>";

        }

    }
}

function CourtDelete() {
    var courtPos = parseInt(document.activeElement.children[0].children[0].textContent);
    courtDataTemp = JSON.parse(JSON.stringify(myCourts[courtPos]));
   

    if (courtDataTemp.crtSched.length != 0)
    {
        alert("Cannot Delete Court! There are still Schedules Remained");
    }
    else
    {
       
        var c = courtDataTemp.crtId;

        $.ajax({
            type: 'POST',
            url: urlData + '/DeleteCourtByOwner',
            data: JSON.stringify({ court_id: c }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;
                location.reload();
            },
            error: function (result) {
                alert(result.responseText);
            }
        });

    }

}


/*Edit Mode Helpers*/
function setCourtSchedules(courtPos) {

    //Loads Schedules Based on focused court element
    var myCourtSchedulesParent = document.querySelector(".sched-pane");
 
    if (myCourts[courtPos].crtSched.length != 0) {

        myCourtSchedulesParent.innerHTML = "";

        for (let j = 0; j < myCourts[courtPos].crtSched.length; j++) {

            myCourtSchedulesParent.innerHTML += "<div tabIndex = \"" +
                j + "\" class=\"crt-sched\"><div class=\"crt-sched-wrap\"><span class = \"crt-sched-num\">" +
                j + "</span><div class=\"crt-sched-det\"><h4>Time:&nbsp;" +
                myCourts[courtPos].crtSched[j].schedStartTime + "-" + myCourts[courtPos].crtSched[j].schedEndTime + "</h4><h5>Status:&nbsp" +
                myCourts[courtPos].crtSched[j].schedStatus + "</h5><h5>" +
                myCourts[courtPos].crtSched[j].schedDateEff + "</h5></div><span onclick = \"deleteSchedule()\" class = \"crt-sched-opt crt-sched-del\" ><i class=\"fas fa-trash-alt\"></i></span></div></div>";
        }

        //Add button Container at the end of Court Schedule
        myCourtSchedulesParent.innerHTML += "<div onclick = \"showAvailabilityPane()\"class=\"add-circle-container\">\<i class=\"fas fa-plus-circle\" id = \"schedAdd\"></i>\</div>";
    }
}

function setCourtDetails(courtPos) {
    var courtDetails = document.getElementsByName("information");

    courtDetails[0].value = myCourts[courtPos].courtName;
    courtDetails[1].value = myCourts[courtPos].crtProvince;
    //Reload Cities based on province
    setCities();
    courtDetails[2].value = myCourts[courtPos].crtCity;
    courtDetails[3].value = myCourts[courtPos].crtCap;
    courtDetails[4].value = myCourts[courtPos].crtAdd;
    courtDetails[5].value = myCourts[courtPos].crtDesc;
}

function CourtEdit() {

    registerState = 0;
    const topMenu = document.querySelector(".btn-container");
    topMenu.style.top = "1%";
    topMenuPanes[0].style.left = "-100%";
    topMenuPanes[1].style.left = "0";

    //Each myCourts index corresponds to each element in My Courts List's index in memory
    var courtPos = parseInt(document.activeElement.children[0].children[0].textContent);

    //Create a deep copy of courtData to prevent overwrites to original courtData
    courtDataTemp = JSON.parse(JSON.stringify(myCourts[courtPos]));
    //Load Details
    setCourtDetails(courtPos);
    //Load Schedules 
    setCourtSchedules(courtPos);
}

//Add Court Sched Button
var addSched = document.querySelector("#schedAdd");
addSched.addEventListener("click", showAvailabilityPane);

function showAvailabilityPane() {
    courtEditMenuPanes[0].style.left = "-200%";
    courtEditMenuPanes[1].style.left = "-100%";
    courtEditMenuPanes[2].style.left = "0";

    const btnBar = document.querySelector(".change-btn-bar");
    btnBar.style.bottom = "-15%";
    const editHeader = document.querySelector(".crt-edit-header");
    editHeader.style.top = "-10%";

    const topMenu = document.querySelector(".btn-container");
    topMenu.style.top = "1%";
}

//Delete Schedule Clicked

function deleteSchedule() {



    var myCourtSchedulesParent = document.querySelector(".sched-pane");
    var children = document.getElementsByClassName("crt-sched");
    schedPos = parseInt(document.activeElement.children[0].children[0].textContent);
    var c = courtDataTemp.crtSched[schedPos].schedId;
    if (courtDataTemp.crtSched[schedPos].schedStatus === "Available" || courtDataTemp.crtSched[schedPos].schedStatus === "Past")
    {
        $.ajax({
            type: 'POST',
            url: urlData + '/DeleteScheduleByOwner',
            data: JSON.stringify({ sched_id: c }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;
                //In memory Delete
                courtDataTemp.crtSched.splice(schedPos, 1);

                //Visually Delete

                //If one left
                if (myCourtSchedulesParent.firstChild.className == "crt-sched" &&
                    myCourtSchedulesParent.children[1].className == "add-circle-container") {
                    myCourtSchedulesParent.innerHTML = "";
                    myCourtSchedulesParent.innerHTML = "<h3 style=\"padding-top: 0.5rem;\">No Schedules Added Yet</h3><div onclick = \"showAvailabilityPane()\"id = \"addSched\" class=\"add-circle-container\"><i class=\"fas fa-plus-circle\" id = \"schedAdd\"></i></div>";
                }
                else {
                    myCourtSchedulesParent.removeChild(document.activeElement);
                    //reset tab index and numbering
                    for (let i = 0; i < myCourtSchedulesParent.children.length - 1; i++) {
                        children[i].tabIndex = i;
                        children[i].children[0].children[0].textContent = i;

                    }
                }

            },
            error: function (result) {
                alert(result.responseText);
            }
        });
    }
    else
    {
        alert("Cannot be Deleted! The schedule is still reserved!");
    }

    
    
    
}

/*Availability Pane helpers*/
setAvailabilityInputs();
function setAvailabilityInputs() {
    var dateToday = new Date().toISOString().split('T')[0];

    const time = [
        "00:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00",
        "06:00:00", "07:00:00", "08:00:00", "09:00:00", "10:00:00", "11:00:00",
        "12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00",
        "18:00:00", "19:00:00", "20:00:00", "21:00:00", "22:00:00", "23:00:00"
    ];

    //Restrict Previous date
    schedInfo = document.getElementsByName("a-info");
    schedInfo[2].min = dateToday;
    schedInfo[2].value = dateToday;

    //Fill Start and End Time Dropdowns
    for (let i = 0; i < 24; i++) {
        //Start Time
        schedInfo[0].innerHTML += "<option>" + time[i] + "</option>";
        //End Time
        schedInfo[1].innerHTML += "<option>" + time[i] + "</option>";
    }
}

//Show Added Sched (this is not loaded to the database, just a visual representation)
//Apply Clicked
function showAddedSched() {
    
    var myCourtSchedulesParent = document.querySelector(".sched-pane");

    //Availablity Pane Field Info
    schedInfo = document.getElementsByName("a-info");

    //Schedule Object
    var schedDetails = JSON.parse(JSON.stringify(SchedDetails));

    //Load filled details
    schedDetails.schedId = courtDataTemp.crtSched.length;
    schedDetails.schedStartTime = schedInfo[0].value;
    schedDetails.schedEndTime = schedInfo[1].value;
    schedDetails.schedStatus = "Available";
    schedDetails.schedDateEff = schedInfo[2].value;

    //Update myCourtsTemp sched's List
    courtDataTemp.crtSched.push(schedDetails);

    //Remove End Element only

    //Clear Pane
    if (myCourtSchedulesParent.firstChild.tagName === undefined) {

        myCourtSchedulesParent.innerHTML = "";
    }
    else if (myCourtSchedulesParent.firstChild.tagName != "H3")
    {
        
        myCourtSchedulesParent.removeChild(myCourtSchedulesParent.lastChild);
    }
    else
        myCourtSchedulesParent.innerHTML = "";

    /*
    if (document.getElementById("rare h3").tagName == "H3") {
        console.log("hello");

        myCourtSchedulesParent.innerHTML = "";
    }
        
    */
    //Add newly created Sched
    myCourtSchedulesParent.innerHTML += "<div tabIndex = \"" +
        (courtDataTemp.crtSched.length - 1) + "\" class=\"crt-sched\"><div class=\"crt-sched-wrap\"><span class = \"crt-sched-num\">" +
        (courtDataTemp.crtSched.length - 1) + "</span><div class=\"crt-sched-det\"><h4>Time:&nbsp;" +
        courtDataTemp.crtSched[(courtDataTemp.crtSched.length - 1)].schedStartTime + "-" + courtDataTemp.crtSched[(courtDataTemp.crtSched.length - 1)].schedEndTime + "</h4><h5>Status:&nbsp" +
        courtDataTemp.crtSched[(courtDataTemp.crtSched.length - 1)].schedStatus + "</h5><h5>" +
        courtDataTemp.crtSched[(courtDataTemp.crtSched.length - 1)].schedDateEff + "</h5></div><span onclick = \"deleteSchedule()\" class = \"crt-sched-opt crt-sched-del\" ><i class=\"fas fa-trash-alt\"></i></span></div></div>";

    //Add end element
    myCourtSchedulesParent.innerHTML += "<div onclick = \"showAvailabilityPane()\"class=\"add-circle-container\">\<i class=\"fas fa-plus-circle\" id = \"schedAdd\"></i>\</div>";
    
    closeAvailabilityPane();

    //reset Availability pane
    setAvailabilityInputs();
}

// ADD COURT DETAILS
function RegisterClicked() {
    var UserID = window.localStorage.getItem("UserId");
    /*
    var SchedStartTime = $("#start_time").val();
    var SchedEndTime = $("#end_time").val();
    var SchedDate = $("#sched_date").val();
    */

    var courtInfoJSON = {
        "courtInfo": {
            CName: $("#court_name").val(),
            CDesc: $("#court_description").val(),
            CCapacity: $("#court_capacity").val(),
            CAddress: $("#court_address").val(),
            CProvince: $("#court_province").val(),
            CCity: $("#court_city").val()
        },
        userID: UserID
    };

    if (registerState == 1)
    {
        $.ajax({
            type: 'POST',
            url: urlData + '/AddCourtDetails',
            data: JSON.stringify(courtInfoJSON),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;

                var r = confirm("Are you sure you want to SAVE changes?");

                if (r == true) {

                    if (value.AddCourtDetailsResult == "SUCCESSFUL") {
                        alert("Registered!");

                        
                        if (courtDataTemp.crtSched.length) {

                            GetCourtIdForSched();

                        }
                        else {

                            window.location = "court.html";
                        }

                    }
                    else if (value.AddCourtDetailsResult == "ERROR") {
                        alert("The court is already existing under your account.");
                    }
                }
                
            },
            error: function (result) {
                alert(result.responseText);
            }
        });
    }
    else
    {
        var courtInfos_json = {
            "court_infor": {
                CName: $("#court_name").val(),
                CDesc: $("#court_description").val(),
                CCapacity: $("#court_capacity").val(),
                CAddress: $("#court_address").val(),
                CProvince: $("#court_province").val(),
                CCity: $("#court_city").val(),
                CourtID: courtDataTemp.crtId
            }
        };

        
        
        $.ajax({
            type: 'POST',
            url: urlData + '/UpdateCourtDetailsInOwners',
            data: JSON.stringify(courtInfos_json),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;

                var r = confirm("Are you sure you want to SAVE changes?");

                if (r == true) {

                    if (courtDataTemp.crtSched.length)
                    {
                        scheduleUpdate();
                        alert('Changes Has Been Saved!')
                    }
                    else
                    {
                        alert(value.UpdateCourtDetailsInOwnersResult);
                    }
                    
                   location.reload();
                }


            },
            error: function (result) {
                alert(result.responseText);
            }
        });
        

    }
    
}

function scheduleUpdate() {



    

    for (let i = 0; i < courtDataTemp.crtSched.length; i++)
    {
        var courtInfos_json = {
            "court_infor": {
                CStartTime: courtDataTemp.crtSched[i].schedStartTime,
                CEndTime: courtDataTemp.crtSched[i].schedEndTime,
                CDateEff: courtDataTemp.crtSched[i].schedDateEff,
                CourtID: courtDataTemp.crtId
            }
        };

        $.ajax({
            type: 'POST',
            url: urlData + '/UpdateScheduleDetailsByOwner',
            data: JSON.stringify(courtInfos_json),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;
                
            },
            error: function (result) {
                alert(result.responseText);
            }
        });
    }
}

// GET COURT ID TO CREATE SCHEDULE UPON COURT REGISTRATION
function GetCourtIdForSched() {
    var UserID = window.localStorage.getItem("UserId");

    var courtInfoJSON = {
        "courtInfo": {
            CName: $("#court_name").val(),
            CDesc: $("#court_description").val(),
            CCapacity: $("#court_capacity").val(),
            CAddress: $("#court_address").val(),
            CProvince: $("#court_province").val(),
            CCity: $("#court_city").val()
        },
        userID: UserID
    };

    $.ajax({
        type: 'POST',
        url: urlData + '/GetCourtID',
        data: JSON.stringify(courtInfoJSON),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {
            var value = JSON.parse(data.GetCourtIDResult);
            localStorage.setItem("CourtId", value.CourtID);
            RegisterSchedule();
        },
        error: function (result) {
            alert(result.responseText);
        }
    });
}

// ADD COURT SCHEDULE UPON COURT REGISTRATION
function RegisterSchedule() {

    

    var schedDetails = JSON.parse(JSON.stringify(SchedDetails));


    /*
    schedDetails.schedStartTime = schedInfo[0].value;
    schedDetails.schedEndTime = schedInfo[1].value;
    schedDetails.schedDateEff = schedInfo[2].value;
    */


    //var sTime = schedDetails.schedStartTime;
    //var numStartTime = sTime.replace(/\D/g, '');
    //var numStartTime = "08:00:00";

    //var eTime = schedDetails.schedEndTime;
    //var numEndTime = eTime.replace(/\D/g, '');
    //var numEndTime = "10:00:00";

    //var dateSched = schedDetails.schedDateEff;


    for (let i = 0; i < courtDataTemp.crtSched.length; i++) {
        var sTime = courtDataTemp.crtSched[i].schedStartTime;
        var eTime = courtDataTemp.crtSched[i].schedEndTime;
        var dateSched = courtDataTemp.crtSched[i].schedDateEff;

        var schedInfoJSON = {
            CourtID: localStorage["CourtId"],
            StartTime: sTime,
            EndTime: eTime,
            SchedDate: dateSched
        };

        

        $.ajax({
            type: 'POST',
            url: urlData + '/AddCourtSchedUponCourtReg',
            data: JSON.stringify(schedInfoJSON),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {
                var value = data;

                if (value.AddCourtSchedUponCourtRegResult == "SUCCESSFUL") {
                    //window.location = "court.html";
                }
                else if (value.AddCourtSchedUponCourtRegResult == "ERROR") {
                    alert("Please enter correct court schedule details.");
                }
                location.reload();
            },
            error: function (result) {
                alert(result.responseText);
            }
        });

    }
    
    
    
}


