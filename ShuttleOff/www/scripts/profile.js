//Display profile on load
var FirstNameH5 = localStorage.getItem("FirstName");
var UserLocation = localStorage.getItem("UserCity") + ", " + localStorage.getItem("UserProvince");
var FullName = localStorage.getItem("FirstName") + " " + localStorage.getItem("MiddleName") + " " + localStorage.getItem("LastName");
var EmailAddress = localStorage.getItem("EmailAddress");
var UserPassword = localStorage.getItem("UserPassword");

function profileOnLoad() {
    //document.getElementById("UserJoinedH6").innerHTML = "Joined " + UserCreated;
    document.getElementById("UserLocH6").innerHTML = UserLocation;
    document.getElementById("FirstNameH5").innerText = FirstNameH5;
    document.getElementById("FullName").placeholder = FullName;
    document.getElementById("email").placeholder = EmailAddress;    
}

//Onload functions
setProvinces();

function openNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "260px";
}

function closeNav() {
    var x = document.getElementById("mySidenav");
    x.style.width = "0";
}

function closeEditProfile() {
    var x = document.getElementById("edit-profile");
    x.style.width = "0";
}

function openEditProfile() {
    var x = document.getElementById("edit-profile");
    x.style.width = "100%";

}

function openChangePassPanel() {
    var x = document.getElementById("changepass-panel")
    x.style.width = "100%";
}

function closeChangePassPanel() {
    var x = document.getElementById("changepass-panel")
    x.style.width = "0%";
}

function loadProvinces() {
    //parsed json data here
    var provinces = ["Bulacan", "Cavite", "Laguna", "NCR", "Pampanga", "Rizal", "Tarlac"]

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

    information[3].innerHTML = ""; //Clear Contents
    new_option.value = "";
    new_option.textContent = "None";
    information[3].appendChild(new_option);
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
        information[3].appendChild(new_option);
        delete new_option;
    }
}

function setCities() {

    var CitiesByProvince = loadCitiesByProvince();
    var information = document.getElementsByName("information");
    var provinceSel = information[3].value;
    if (provinceSel == 0) {
        information[4].innerHTML = "<option>None</option>"
    }
    else {
        var citiesOptions = "";
        for (cityId in CitiesByProvince[provinceSel]) {
            citiesOptions += "<option>" + CitiesByProvince[provinceSel][cityId] + "</option>";
        }
        information[4].innerHTML = citiesOptions;
    }
}

//UPDATE USER DETAILS
function UpdateUserDetailsClicked() {
    var userInfoJSON = {
        "userInfo": {
            UserID: localStorage["UserId"],
            FName: $("#first_name").val(),
            MName: $("#mid_name").val(),
            LName: $("#last_name").val(),
            Province: $("#province").val(),
            City: $("#city").val()
        }
    };

    $.ajax({
        type: 'POST',
        url: 'http://localhost:54458/Service1.svc/UpdateUserDetails',
        data: JSON.stringify(userInfoJSON),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {
            var value = data;
            alert("Updated!");

            if (value.UpdateUserDetailsResult == "UPDATED")
            {
                localStorage.setItem("FirstName", $("#first_name").val());
                localStorage.setItem("MiddleName", $("#mid_name").val());
                localStorage.setItem("LastName", $("#last_name").val());
                localStorage.setItem("UserProvince", $("#province").val());
                localStorage.setItem("UserCity", $("#city").val());
                window.location = 'profile.html'
            }
            else if (value.UpdateUserDetailsResult == "ERROR")
            {
                alert("Please complete all the information.")

                $("#edit-profile").show();
            }
        },
        error: function (result) {
            alert(result.responseText);
        }
    });
}

//UPDATE USER PASSWORD
function ValidateCurrentPW() {
    var currentPassword = document.getElementById("current_pw").value;
    var newPassword = document.getElementById("new_pw").value;
    var confirmPassword = document.getElementById("confirm_pw").value;
    if (currentPassword != UserPassword)
    {
        alert("Incorrect Password. Please type the correct current password.")
    }
    else
    {
        if (newPassword != confirmPassword) {
            alert("New and confirm passwords do not match.");
            return false;
        }
        else {
            UpdateUserPasswordClicked();
            return true;
        }
    }
}

function UpdateUserPasswordClicked() {
    var userInfoJSON = {
        "userInfo": {
            UserID: localStorage["UserId"],
            UserPW: $("#new_pw").val()
        }
    };

    $.ajax({
        type: 'POST',
        url: 'http://localhost:54458/Service1.svc/UpdateUserPassword',
        data: JSON.stringify(userInfoJSON),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {
            var value = data;
            alert("Updated!");

            if (value.UpdateUserPasswordResult == "UPDATED") {
                localStorage.setItem("UserPassword", $("#new_pw").val());                
                window.location = 'profile.html'
            }
            else if (value.UpdateUserPasswordResult == "ERROR") {
                alert("Please complete all the information.")

                $("#changepass-panel").show();
            }
        },
        error: function (result) {
            alert(result.responseText);
        }
    });
}
