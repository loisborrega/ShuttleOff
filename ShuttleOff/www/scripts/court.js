//Non Dynamic Elements Helpers
//Top menu panes
const topMenuBtns = document.getElementsByClassName("rad");
const topMenuPanes = document.getElementsByClassName("pane");

for (let i = 0; i < topMenuBtns.length; i++) {
    topMenuBtns[i].oninput = function () {
        if (topMenuBtns[0].checked) {
            topMenuPanes[0].style.left = 0;
            topMenuPanes[1].style.left = "100%";
        }
        if (topMenuBtns[1].checked) {
            topMenuPanes[0].style.left = "-100%";
            topMenuPanes[1].style.left = 0;
        }
    };
}

//Court Edit menu panes
const courtEditMenuBtn = document.getElementsByClassName("radio-btn");
const courtEditMenuPanes = document.getElementsByClassName("crt-edit-p");
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

//Fill Start and End Time Dropdowns
const info = document.getElementsByName("a-info");
const time = [
    "12AM", "1AM", "2AM", "3AM", "4AM", "5AM",
    "6AM", "7AM", "8AM", "9AM", "10AM", "11AM",
    "12PM", "1PM", "2PM", "3PM", "4PM", "5PM",
    "6PM", "7PM", "8PM", "9PM", "10PM", "11PM"
];
for (let i = 0; i < 24; i++) {
    info[0].innerHTML += "<option>" + time[i] + "</option>";
    info[1].innerHTML += "<option>" + time[i] + "</option>";
}

/*Province & Cities Drop Down Boxes */
setProvinces();
var information = document.getElementsByName("information");
information[1].oninput = function () {
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
/*
function resetSelection() {
    document.getElementById("province").selectedIndex = 0;
    document.getElementById("city").selectedIndex = 0;
}
*/

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

//Helper Functions
function openNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "260px";
}

function closeNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "0";
}


const changeButtons = document.querySelectorAll(".chg-btn");
changeButtons[0].onclick = function () {

    const topMenu = document.querySelector(".btn-container");
    if (topMenu.style.top == "1%")
        topMenu.style.top = "8%";
    topMenuPanes[0].style.left = "0";
    topMenuPanes[1].style.left = "100%";
};

var addSchedBtn = document.getElementsByClassName("av-btn");
addSchedBtn[0].onclick = function () {
    courtEditMenuPanes[0].style.left = "-100%";
    courtEditMenuPanes[1].style.left = "0";
    courtEditMenuPanes[2].style.left = "100%";

    const btnBar = document.querySelector(".change-btn-bar");
    const editHeader = document.querySelector(".crt-edit-header");

    if (btnBar.style.bottom == "-15%" && editHeader.style.top == "-10%") {
        btnBar.style.bottom = "0";
        editHeader.style.top = "0";
    }
};

//Onload Functions parsing test
setMyCourts();
function loadMyCourts() {

    //Sample query size of all active reservations 
    //(Change this to test courts added)
    let queryLength = 0;

    //Array to store each dictionary object
    var myCourts = [];

    //Loading courtData object to myCourts array
    for (let i = 0; i < queryLength; i++) {

        //load court data format here, dictionary sample per object
        var courtData = {
            crtId: 0,
            userId: 0,
            courtName: "",
            crtProvince: "",
            crtCity: "",
            crtCap: 0,
            crtAdd: "",
            crtDesc: "",
            crtSched: {
                schedId: "",
                schedTime: "",
                schedDays: []
            },
        }
        //Test Values
        courtData.crtId = i;
        courtData.userId = i;
        courtData.courtName = "Hilly's - Public Court No." + i;
        courtData.crtProvince = "NCR";
        courtData.crtCity = "Makati",
            courtData.crtCap = 8,
            courtData.crtAdd = "Lorem Ipsum",
            courtData.crtDesc = "Lorem Ipsum",

            courtData.crtSched.schedId = i;
        courtData.crtSched.schedTime = "10PM -11PM";
        courtData.crtSched.schedDays = ["TUE", "THU"];
        myCourts[i] = courtData;
    }

    return myCourts;
}

function setMyCourts() {

    var myCourts = loadMyCourts();
    //My Courts Pane
    const myCourtParent = document.querySelector(".crt-lst");


    if (myCourts.length != 0) {
        //Clear Contents
        myCourtParent.innerHTML = "";
        var status = "OK";
        for (let i = 0; i < myCourts.length; i++) {
            if (myCourts[i].crtCap == 8)
                status = "FULL";
            else
                status = "OK";
            myCourtParent.innerHTML += "<div tabindex =\"" +
                i + "\"class=\"crt-lst-cnt\"><div class=\"crt-lst-cnt-wrp\"><span class = \"crt-lst-num\">" +
                i + "</span><div class=\"crt-det\"><h3>" +
                myCourts[i].courtName + "</h3><h4>Status:<span>&nbsp;" +
                status + "</span></h4></div><span class = \"edit-btn slide-menu\"><i class=\"fas fa-tools\"></i></span><span class = \"trash-btn slide-menu\"><i class=\"fas fa-trash-alt\"></i></span></div></div>";
        }
    }
}

//Dynamic Element Helpers

//My Court Edit Button
const editCourtBtn = document.querySelectorAll(".edit-btn");
for (let i = 0; i < editCourtBtn.length; i++) {
    editCourtBtn[i].addEventListener("click", function () {
        const topMenu = document.querySelector(".btn-container");
        topMenu.style.top = "1%";
        topMenuPanes[0].style.left = "-100%";
        topMenuPanes[1].style.left = "0";
    });
}

//Add Court Sched Button
var addSched = document.getElementById("schedAdd");
addSched.onclick = function () {
    courtEditMenuPanes[0].style.left = "-200%";
    courtEditMenuPanes[1].style.left = "-100%";
    courtEditMenuPanes[2].style.left = "0";

    const btnBar = document.querySelector(".change-btn-bar");
    btnBar.style.bottom = "-15%";
    const editHeader = document.querySelector(".crt-edit-header");
    editHeader.style.top = "-10%";
}
