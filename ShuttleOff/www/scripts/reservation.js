//var urlData = 'http://192.168.254.127/ShuttleOffServiceAjax/Service1.svc';
var urlData = 'http://localhost:54458/Service1.svc';
//var urlData = 'http://localhost/ShuttleOffService/Service1.svc';
//var urlData_Almer = 'http://localhost:50869/Service1.svc'; // Almer Service

//Business Logic Objects
var tempResData;
var ReservationData = {
    resId : 0,
    userId : 0,
    courtId: 0,
    schedId: 0,
    courtName: "",
    province: "",
    city: "",
    time: "",
    address: "",
    isReviewed: false //added this bad body to prevent second reviews, only applies to history
}
var feedback = {
    userId : "",
    courtId : "",
    starCount: "",
    review: ""
}
//Temporary Data Storers (ctrl + f)
var activeRes;
var historyRes;
var feedbackTemp;
var reviews = [];
//Functions onload
//setHistoryReservations();

function openNav(){
    var x = document.getElementById("mySidenav");
    x.style.width = "260px";
}

function closeNav(){
    var x = document.getElementById("mySidenav");
    x.style.width = "0";
}

function showActivePane(){
    var p1 = document.getElementById("pane1");
    var p2 = document.getElementById("pane2");
    var indicator = document.getElementById("btn-selection-indicator");

    p1.style.transform = "translate(0,0)";
    p2.style.transform = "translate(100%,0)";
    indicator.style.transform = "translate(0,0)" 
    
}

function showHistoryPane(){
    var p1 = document.getElementById("pane1");
    var p2 = document.getElementById("pane2");
    var indicator = document.getElementById("btn-selection-indicator");

    p1.style.transform = "translate(-100%,0)";
    p2.style.transform = "translate(0,0)";
    indicator.style.transform = "translate(100%,0)" 
}

$(document).ready(function () {
    activeRes = loadActiveReservations();

    historyRes = loadHistory();
    //List All Courts
});


function loadActiveReservations(){


    //Array to store each dictionary object
    reservations = [];

    var c = localStorage.getItem("UserId");

    $.ajax({
        type: 'POST',
        url: urlData + '/DisplayReservation',
        data: JSON.stringify({ user_id: c }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {
            var value = JSON.parse(data.DisplayReservationResult);

            //Loading reservationData object to reservation array
            for (let i = 0; i < value.length; i++) {

                //load reservation data format here, dictionary sample per object
                var reservationData = JSON.parse(JSON.stringify(ReservationData));
                reservationData.schedId = value[i].schedId;
                reservationData.resId = value[i].reserveId;
                reservationData.userId = c;
                reservationData.courtId = value[i].courtId;
                reservationData.courtName = value[i].Name;
                reservationData.province = value[i].Province;
                reservationData.city = value[i].City;
                reservationData.time = value[i].StartTime + "-" + value[i].EndTime + " (" + value[i].DateEffective + ")" ;
                reservationData.address = value[i].Address;
                reservations[i] = reservationData;
            }

            setActiveReservations();

        },
        error: function (result) {
            alert(result.responseText);
        }
    });
 
    return reservations;
}

function setActiveReservations(){
    var parent = document.querySelector(".pane1-wrap");

    if (activeRes.length != 0){
        parent.innerHTML = "";
        for(let i = 0; i < activeRes.length; i++){
            parent.innerHTML += "<div tabindex = " + i + " class=\"pane1-content\"><div class=\"pane1-content-wrap\"><h3>" + activeRes[i].courtName + "</h3><i class=\"fas fa-map-marker-alt\">&nbsp;"
                + activeRes[i].province + "," + activeRes[i].city +"</i><i class=\"far fa-clock\">&nbsp;" 
                + activeRes[i].time + "</i><span onclick = \"deleteActiveReservations()\"><i class=\"far fa-trash-alt\"></i></span></div></div><div class=\"court-address\"><h4>Address</h4><p>"
            + activeRes[i].address+"</p></div>";
        }
    }
}


function deleteActiveReservations() {
    var resPos = document.activeElement.tabIndex;
    var c = activeRes[resPos].resId;

    var r = confirm("Are you sure you want to SAVE changes?");

    if (r == true) {

        $.ajax({
            type: 'POST',
            url: urlData + '/DeleteReserveDetails',
            data: JSON.stringify({ reserve_id: c }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processdata: true,
            success: function (data) {


                location.reload();

            },
            error: function (result) {
                alert(result.responseText);
            }
        });
    }


   
}

function loadHistory() {

    //Array to store each dictionary object
    var reservations = [];

    var c = localStorage.getItem("UserId");

    $.ajax({
        type: 'POST',
        url: urlData + '/DisplayPastReservation',
        data: JSON.stringify({ user_id: c }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {
            var value = JSON.parse(data.DisplayPastReservationResult);

            //Loading reservationData object to reservation array
            for (let i = 0; i < value.length; i++) {

                //load reservation data format here, dictionary sample per object
                var reservationData = JSON.parse(JSON.stringify(ReservationData));
                reservationData.schedId = value[i].schedId;
                reservationData.resId = value[i].reserveId;
                reservationData.userId = c;
                reservationData.courtId = value[i].courtId;
                reservationData.courtName = value[i].Name;
                reservationData.province = value[i].Province;
                reservationData.city = value[i].City;
                reservationData.time = value[i].StartTime + "-" + value[i].EndTime + " (" + value[i].DateEffective + ")";
                reservationData.address = value[i].Address;
                reservations[i] = reservationData;
            }

            setHistoryReservations();

        },
        error: function (result) {
            alert(result.responseText);
        }
    });



    /*Test Case for if there are reviews (1:1 match, in reality, its not) 
    (feel free to comment everything out below)*/
    /*for (let i = 0; i< queryLength; i++ ){
        //load history reservation data format here, dictionary sample per object
        var reservationData = JSON.parse(JSON.stringify(ReservationData));
        reservationData.resId = i;
        reservationData.userId = i;
        reservationData.courtId = i;
        reservationData.courtName = "Yesterday" + i;
        reservationData.time = "10PM - 11PM";
        reservationData.address = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi facilis quidem amet excepturi sint. Dicta, repudiandae! Quaerat a dicta cum iusto libero nam. Placeat, nihil dignissimos laborum temporibus rerum pariatur?";
        reservationData.isReviewed = true;
        reservations[i] = reservationData;

        var reviewData = JSON.parse(JSON.stringify(feedback));
        reviewData.userId = i;
        reviewData.courtId = i;
        reviewData.starCount = parseInt((Math.random() * 5) + 1); //produce numbers from 1 - 5
        reviewData.review = "Your mom is gay times " + i;
        reviews[i] = reviewData;
    }
    */
    return reservations;
}

function setHistoryReservations(){
    var parent = document.querySelector(".pane2-wrap");

    if (historyRes.length != 0){
        parent.innerHTML = "";
        for (let i = 0; i < historyRes.length; i++){
            parent.innerHTML += "<div tabindex = " + i + " class=\"pane2-content\"><div class=\"pane2-content-wrap\"><h3>" + historyRes[i].courtName + "</h3><i class=\"fas fa-map-marker-alt\">&nbsp;"
                + historyRes[i].province + "," + historyRes[i].city + "</i><span onclick = \"openReviewModal()\" ><i class=\"fas fa-comment-alt\"></i></span></div></div><div class=\"court-address\"><h4>Address</h4><p>"
            + historyRes[i].address+"</p></div>";
        }
    }
}

/*Review Modal Helpers*/
function openReviewModal(){

    //Set display from none to flex 
    var rv_modal = document.querySelector(".review-mod");
    rv_modal.style.display = "flex";

    //Get position of clicked element
    var pos = parseInt(document.activeElement.tabIndex);

    //load current content to tempResData
    tempResData = JSON.parse(JSON.stringify(historyRes[pos]));


    /*Setting details for feedbackTemp according to tempResData** 
    (Note this is for uniform reviews array only 1:1)
    NEED TO MERGE THIS
    */

    //Uncomment Below if using reviews[]
    //feedbackTemp = JSON.parse(JSON.stringify(reviews[pos]));
    
    //Uncomment below if using w/o reviews[]
    feedbackTemp = JSON.parse(JSON.stringify(feedback));

    //Assignments below are allowed to deep copied objects, dont try this at home... pls
    feedbackTemp.userId = tempResData.userId; 
    feedbackTemp.courtId = tempResData.courtId;
    
    //Set the court name of the Modal
    var modal_courtName = document.querySelector(".rv-mod-bod-wr > h3");
    modal_courtName.textContent = tempResData.courtName;

    //If already reviewed
    if(tempResData.isReviewed == true)
    {
        //Searches below are based on reviews array only**. 

        //Disable submit button
        rv_btns[1].disabled = true;

        //Disable text area and show review made
        document.querySelector(".rv-mod-bod-wr > textarea").disabled = true;
        
        //Set Review Made
        document.querySelector(".rv-mod-bod-wr > textarea").value = feedbackTemp.review;

        //Fill Star container
        var stars = document.querySelectorAll(".star-wrap > i");

        for(let i = 0; i < feedbackTemp.starCount; i++)
            stars[i].className = "fas fa-star";
        
        //IDK how to disable the stars but there is no effect anyway
        //(Disable stars BOOM...)

    }
    if(tempResData.isReviewed == false)
    {
      
        //Enable Text Area
        document.querySelector(".rv-mod-bod-wr > textarea").disabled = false;
        
        //Enable submit button
        rv_btns[1].disabled = false;

        //Set Enable Star container
        //(still dont know how to... enable stars BOOOOOOM)

    }
}

function closeReviewModal(){
    var rv_modal = document.querySelector(".review-mod");
    rv_modal.style.display = "none";

    //Star Removal
    var stars = document.querySelectorAll(".star-wrap > i");
    for(star of stars){
        star.className = "far fa-star";
        while((star = star.previousSibling) != null)
            star.className = "far fa-star";
    }

    //Test area clear
    document.querySelector(".rv-mod-bod-wr > textarea").value = "";
}

function submitReview(){
    review = document.querySelector(".rv-mod-bod-wr > textarea")


    //feedbacktemp Details
    feedbackTemp.review = review.value;
    feedbackTemp.starCount = (feedbackTemp.starCount + 1) / 2;

   

    var courtInfos_json = {
        user_id: feedbackTemp.userId,
        court_id: feedbackTemp.courtId,
        reserve_id: tempResData.resId,
        sched_id: tempResData.schedId,
        ratings: feedbackTemp.starCount,
        comments: feedbackTemp.review,
        
    };
    

    $.ajax({
        type: 'POST',
        url: urlData + '/AddFeedbackDetails',
        data: JSON.stringify(courtInfos_json),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {

            alert("Thank You For your Response!");
            closeReviewModal();

            //Memory Clean up
            delete tempResData;
            delete feedbackTemp;
            location.reload();

        },
        error: function (result) {
            alert(result.responseText);
        }
    });

    //Update review here

    /*Don't ask why because I dont know either but it works because 
    it outputs numbers in this order of stars highlighted (1 3 5 7 9). 
    If you dont know how, good. Me neither.*/
    
    //Send this data to history table
    tempResData.isReviewed = true;
    //Update history data here

    //console.log(feedbackTemp.starCount)
    //console.log(feedbackTemp.review)
    
}

/*Review Modal Buttons*/
var rv_btns = document.getElementsByClassName("rv-btn");
//Cancel Button
rv_btns[0].addEventListener("click", closeReviewModal); 
//Submit Button
rv_btns[1].addEventListener("click", submitReview);
//Star Logic onclick
var stars = document.querySelectorAll(".star-wrap > i");
for(star of stars){
    star.addEventListener("click",function(){
        feedbackTemp.starCount = 0;
        //node head
        first_star_child = this;
        //node tail
        star_child = this;

        //highlight preceding stars
        while(star_child.previousSibling != null){
            star_child.className = "fas fa-star";
            star_child = star_child.previousSibling;
            feedbackTemp.starCount++;
        }

        star_child = first_star_child;
        //unhighlight following stars
        while(star_child.nextSibling != null && star_child.nextSibling.className == "fas fa-star"){
            star_child = star_child.nextSibling;
            star_child.className = "far fa-star";
        }
    });
}
