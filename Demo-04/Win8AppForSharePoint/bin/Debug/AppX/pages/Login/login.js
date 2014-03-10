

(function () {

    // Track if the log in was successful
    var loggedIn;
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var containerName = "exampleContainer1";
    var nav = WinJS.Navigation;
    var container;
    var FedAuth;
    var RtFa;
    var address;
    var username;
    var password;
    "use strict";
    var page = WinJS.UI.Pages.define("/pages/Login/login.html", {
        ready: function (element, options) {
            element.querySelector('.submitButton').addEventListener('click', submitLogin, false);
          
        }
    });

 

    //account logon
    // onClick() handler for the 'submitButton'
    function submitLogin() {
        try {
            if (address|| username|| password)
            {
                //Creating message dialog box 
                var messagedialogpopup = new Windows.UI.Popups.MessageDialog("Input can not be empty!", "Error");
                messagedialogpopup.showAsync();
                return;
            }
            document.querySelector(".progress").style.display = "block";
             address = document.getElementById("address").value;
             username = document.getElementById("username").value;
             password = document.getElementById("password").value;
            var json = JSON.stringify({ "url": address, "userName": username, "password": password });
        }
        catch (err) {         
        }
        WinJS.xhr({
            type: "POST",
            url: "http://localhost:2738/Office365ClaimsService.svc/Authentication",
            headers: { "content-type": "application/json; charset=utf-8" },
            data: json,
        }).done(loginSuccess, loginFaliure, loginProgress);
    };
    function loginSuccess(request)
    {
        var obtainedData = window.JSON.parse(request.responseText);
        var container = localSettings.createContainer(containerName, Windows.Storage.ApplicationDataCreateDisposition.always);
        if (localSettings.containers.hasKey(containerName)) {
            localSettings.containers.lookup(containerName).values["FedAuth"] = obtainedData.FedAuth;
            localSettings.containers.lookup(containerName).values["RtFa"] = obtainedData.RtFa;
            localSettings.containers.lookup(containerName).values["Url"] = address;
        }
        WinJS.Navigation.navigate('/pages/Search/search.html');
    }
    function loginFaliure(request)
    {
        document.querySelector(".progress").style.display = "none";
        //Creating message dialog box 
        var messagedialogpopup = new Windows.UI.Popups.MessageDialog("An error occurred!", "Error");
        messagedialogpopup.showAsync();
        return false;
    }
    function loginProgress(request) {
        
    }
})();
