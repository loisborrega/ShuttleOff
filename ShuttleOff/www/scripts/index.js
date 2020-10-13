// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in cordova-simulate or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.


//var urlData = 'http://192.168.254.127/ShuttleOffServiceAjax/Service1.svc';
//var urlData = 'http://localhost:54458/Service1.svc';
var urlData = 'http://localhost/ShuttleOffService/Service1.svc';

function LoginClicked() {
    var userInfoJSON = {
        "userLog": {
            EmailAdd: $("#emailadd").val(),
            UserPW: $("#password").val(),
        }
    };

    $.ajax({
        type: 'POST',
        url: urlData + '/UserLogin',
        data: JSON.stringify(userInfoJSON),        
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processdata: true,
        success: function (data) {

            //console.log(data.d.FName);
            //if (data.d.UserID != 0)
            var value = JSON.parse(data.UserLoginResult);
            if(value.FName)
            {
                localStorage.setItem("UserId", value.UserID);
                localStorage.setItem("EmailAddress", value.EmailAdd);
                localStorage.setItem("UserPassword", value.UserPW);
                localStorage.setItem("FirstName", value.FName);
                localStorage.setItem("MiddleName", value.MName);
                localStorage.setItem("LastName", value.LName);
                localStorage.setItem("UserProvince", value.Province);
                localStorage.setItem("UserCity", value.City);
                localStorage.setItem("UserCreated", value.DateCreated);
                

                window.close = "index.html";
                window.location = "dashboard.html";                
            }
            else
            {
                alert("Incorrect email address or password.");
            }
        },
        error: function (result) {
           alert(result);
        }
    });

}

(function () {
    "use strict";

    var $body = document.querySelector('body');

    // Methods/polyfills.

    // classList | (c) @remy | github.com/remy/polyfills | rem.mit-license.org
    !function () { function t(t) { this.el = t; for (var n = t.className.replace(/^\s+|\s+$/g, "").split(/\s+/), i = 0; i < n.length; i++)e.call(this, n[i]) } function n(t, n, i) { Object.defineProperty ? Object.defineProperty(t, n, { get: i }) : t.__defineGetter__(n, i) } if (!("undefined" == typeof window.Element || "classList" in document.documentElement)) { var i = Array.prototype, e = i.push, s = i.splice, o = i.join; t.prototype = { add: function (t) { this.contains(t) || (e.call(this, t), this.el.className = this.toString()) }, contains: function (t) { return -1 != this.el.className.indexOf(t) }, item: function (t) { return this[t] || null }, remove: function (t) { if (this.contains(t)) { for (var n = 0; n < this.length && this[n] != t; n++); s.call(this, n, 1), this.el.className = this.toString() } }, toString: function () { return o.call(this, " ") }, toggle: function (t) { return this.contains(t) ? this.remove(t) : this.add(t), this.contains(t) } }, window.DOMTokenList = t, n(Element.prototype, "classList", function () { return new t(this) }) } }();

    // canUse
    window.canUse = function (p) { if (!window._canUse) window._canUse = document.createElement("div"); var e = window._canUse.style, up = p.charAt(0).toUpperCase() + p.slice(1); return p in e || "Moz" + up in e || "Webkit" + up in e || "O" + up in e || "ms" + up in e };

    // window.addEventListener
    (function () { if ("addEventListener" in window) return; window.addEventListener = function (type, f) { window.attachEvent("on" + type, f) } })();

    // Play initial animations on page load.
    window.addEventListener('load', function () {
        window.setTimeout(function () {
            $body.classList.remove('is-preload');
            $body.classList.remove('signup-page');
        }, 100);
    });

    // Slideshow Background.
    (function () {

        // Settings.
        var settings = {

            // Images (in the format of 'url': 'alignment').
            images: {
                'images/bc1.jpg': 'center',
                'images/bc2.jpg': 'center',
                'images/bc3.jpg': 'center'
            },

            // Delay.
            delay: 6000

        };

        // Vars.
        var pos = 0, lastPos = 0,
            $wrapper, $bgs = [], $bg,
            k, v;

        // Create BG wrapper, BGs.
        $wrapper = document.createElement('div');
        $wrapper.id = 'bg';
        $body.appendChild($wrapper);

        for (k in settings.images) {

            // Create BG.
            $bg = document.createElement('div');
            $bg.style.backgroundImage = 'url("' + k + '")';
            $bg.style.backgroundPosition = settings.images[k];
            $wrapper.appendChild($bg);

            // Add it to array.
            $bgs.push($bg);

        }

        // Main loop.
        $bgs[pos].classList.add('visible');
        $bgs[pos].classList.add('top');

        // Bail if we only have a single BG or the client doesn't support transitions.
        if ($bgs.length == 1
            || !canUse('transition'))
            return;

        window.setInterval(function () {

            lastPos = pos;
            pos++;

            // Wrap to beginning if necessary.
            if (pos >= $bgs.length)
                pos = 0;

            // Swap top images.
            $bgs[lastPos].classList.remove('top');
            $bgs[pos].classList.add('visible');
            $bgs[pos].classList.add('top');

            // Hide last image after a short delay.
            window.setTimeout(function () {
                $bgs[lastPos].classList.remove('visible');
            }, settings.delay / 2);

        }, settings.delay);

    })();




    // Login Form.
    (function () {

        // Vars.
        var $form = document.querySelectorAll('#login-form')[0],
            $submit = document.querySelectorAll('#login-form button[type="submit"]')[0],
            $message;

        // Bail if addEventListener isn't supported.
        if (!('addEventListener' in $form))
            return;

        // Message.
        $message = document.createElement('span');
        $message.classList.add('message');
        $form.appendChild($message);

        $message._show = function (type, text) {

            $message.innerHTML = text;
            $message.classList.add(type);
            $message.classList.add('visible');

            window.setTimeout(function () {
                $message._hide();
            }, 3000);

        };

        $message._hide = function () {
            $message.classList.remove('visible');
        };

        // Events.
        // Note: If you're *not* using AJAX, get rid of this event listener.
        $form.addEventListener('submit', function (event) {

            event.stopPropagation();
            event.preventDefault();

            // Hide message.
            $message._hide();

            // Disable submit.
            $submit.disabled = true;

            // Process form.
            // Note: Doesn't actually do anything yet (other than report back with a "thank you"),
            // but there's enough here to piece together a working AJAX submission call that does.
            window.setTimeout(function () {

                // Reset form.
                $form.reset();

                // Enable submit.
                $submit.disabled = false;

                // Show message.
                //$message._show('success', 'Thank you!');
                //$message._show('failure', 'Something went wrong. Please try again.');
                //window.open("./dashboard.html");
                //this.close();
            }, 750);

        });

    })

        ();
} )();